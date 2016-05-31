using System;
using System.Linq;

namespace HomeManager.Infrastructure.MVVM.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultContentForRegionAttribute : Attribute
    {
        public DefaultContentForRegionAttribute(string region, params string[] additionalRegions)
        {
            Regions = new[] { region }.Concat(additionalRegions ?? new string[0]).ToArray();
        }

        public string[] Regions { get; private set; }
    }
}