using System.Collections.Generic;

namespace Moedelo.Infrastructure.System.Extensions.Assemblies
{
    internal sealed class AssemblyResourcePathEqualityComparer : IEqualityComparer<AssemblyResourcePath>
    {
        public bool Equals(AssemblyResourcePath x, AssemblyResourcePath y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }
            
            var xAssemblyName = x.Assembly.GetName().Name;
            var xResourcePath = x.ResourcePath;
            
            var yAssemblyName = y.Assembly.GetName().Name;
            var yResourcePath = y.ResourcePath;

            return xAssemblyName == yAssemblyName && xResourcePath == yResourcePath;
        }

        public int GetHashCode(AssemblyResourcePath obj)
        {
            var assemblyName = obj.Assembly.GetName().Name;
            var resourcePath = obj.ResourcePath;

            return $"{assemblyName}::{resourcePath}".GetHashCode();
        }
    }
}