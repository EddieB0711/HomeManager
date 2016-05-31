using FluentAssertions;
using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.ResolveDependencies;
using Ninject;
using NUnit.Framework;

namespace HomeManager.Desktop.Infrastructure.Tests
{
    [TestFixture]
    public class DefaultRegistryTests
    {
        [Test]
        public void When_resolving_Dispatcher_should_not_be_null()
        {
            var container = DefaultRegistry.Initialize();
            var dispatcher = container.Get<IDispatcher>();

            dispatcher.Should().NotBeNull();
        }
    }
}