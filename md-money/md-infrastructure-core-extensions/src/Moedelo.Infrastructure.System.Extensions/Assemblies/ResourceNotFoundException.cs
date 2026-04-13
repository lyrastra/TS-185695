using System;

namespace Moedelo.Infrastructure.System.Extensions.Assemblies
{
    public sealed class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string assemblyName, string resourcePath) : base(
            $"Resource {resourcePath} not found in assembly {assemblyName}")
        {
        }
    }
}