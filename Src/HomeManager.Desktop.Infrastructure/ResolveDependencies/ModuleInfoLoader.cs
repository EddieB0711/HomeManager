using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Prism.Modularity;

namespace HomeManager.Desktop.Infrastructure.ResolveDependencies
{
    internal class ModuleInfoLoader
    {
        internal ModuleInfo[] GetModules(string path)
        {
            var directory = new DirectoryInfo(path);

            ResolveEventHandler resolveEventHandler =
                delegate (object sender, ResolveEventArgs args) { return OnReflectionOnlyResolve(args, directory); };

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;
            ModuleInfo[] moduleCollection;

            try
            {
                var moduleReflectionOnlyAssembly =
                    AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
                        .First(asm => asm.FullName == typeof(IModule).Assembly.FullName);
                var moduleType = moduleReflectionOnlyAssembly.GetType(typeof(IModule).FullName);

                var modules = GetAssembliesImplementingIModule(directory, moduleType);
                moduleCollection = modules.ToArray();
            }
            finally
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;
            }

            return moduleCollection;
        }

        internal void LoadAssemblies(IEnumerable<string> assemblies)
        {
            foreach (var assemblyPath in assemblies)
            {
                try
                {
                    Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                }
                catch (FileNotFoundException exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception.Message);
                }
            }
        }

        private static ModuleInfo CreateModuleInfo(Type type)
        {
            var moduleName = type.FullName;
            var onDemand = false;

            ModuleAttributes(type, ref onDemand, ref moduleName);

            var moduleInfo = new ModuleInfo(moduleName, type.AssemblyQualifiedName)
            {
                InitializationMode = onDemand ? InitializationMode.OnDemand : InitializationMode.WhenAvailable,
                Ref = type.Assembly.CodeBase,
            };

            moduleInfo.DependsOn.AddRange(
                CustomAttributeData.GetCustomAttributes(type)
                    .Where(
                        cad => cad.Constructor.DeclaringType != null && cad.Constructor.DeclaringType.FullName == typeof(ModuleDependencyAttribute).FullName)
                    .Select(cad => (string)cad.ConstructorArguments[0].Value)
                    .ToList());
            return moduleInfo;
        }

        private static void ModuleAttributes(Type type, ref bool onDemand, ref string moduleName)
        {
            var moduleAttribute =
                CustomAttributeData.GetCustomAttributes(type)
                    .FirstOrDefault(cad => cad.Constructor.DeclaringType != null && cad.Constructor.DeclaringType.FullName == typeof(ModuleAttribute).FullName);

            if (moduleAttribute != null && moduleAttribute.NamedArguments != null)
            {
                foreach (var argument in moduleAttribute.NamedArguments)
                {
                    var argumentName = argument.MemberInfo.Name;

                    switch (argumentName)
                    {
                        case "ModuleName":
                            moduleName = (string)argument.TypedValue.Value;
                            break;

                        case "OnDemand":
                            onDemand = (bool)argument.TypedValue.Value;
                            break;

                        case "StartupLoaded":
                            onDemand = !((bool)argument.TypedValue.Value);
                            break;
                    }
                }
            }
        }

        private IEnumerable<ModuleInfo> GetAssembliesImplementingIModule(DirectoryInfo directory, Type moduleType)
        {
            return
                directory.GetFiles("*Module.dll")
                    .SelectMany(file =>
                        Assembly.ReflectionOnlyLoadFrom(file.FullName)
                            .GetExportedTypes()
                            .Where(t => ExportTypeIsValid(t, moduleType))
                            .Select(CreateModuleInfo));
        }

        private Assembly OnReflectionOnlyResolve(ResolveEventArgs args, DirectoryInfo directory)
        {
            var loadedAssembly =
                AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
                    .FirstOrDefault(
                        asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase));

            if (loadedAssembly != null) return loadedAssembly;

            var assemblyName = new AssemblyName(args.Name);
            var dependentAssemblyFilename = Path.Combine(directory.FullName, assemblyName.Name + ".dll");

            if (File.Exists(dependentAssemblyFilename)) return Assembly.ReflectionOnlyLoadFrom(dependentAssemblyFilename);

            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        private static bool ExportTypeIsValid(Type exportType, Type moduleType)
        {
            return moduleType.IsAssignableFrom(exportType)
                && exportType != moduleType
                && !exportType.IsAbstract;
        }
    }
}