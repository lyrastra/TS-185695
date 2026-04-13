using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Exceptions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess
{
    [InjectAsSingleton(typeof(ISqlScriptReader))]
    internal sealed class SqlScriptReader : ISqlScriptReader
    {
        private readonly ConcurrentDictionary<AssemblyScriptPath, string> scripts 
            = new ConcurrentDictionary<AssemblyScriptPath, string>(new AssemblyScriptPathEqualityComparer());

        public string Get<T>(T dao, string scriptPath)
        {
            return Get(typeof(T), scriptPath);
        }

        private string Get(Type type, string scriptPath)
        {
            var assembly = type.Assembly;
            var assemblyScriptPath = new AssemblyScriptPath(assembly, scriptPath);

            var script = scripts.GetOrAdd(assemblyScriptPath, ScriptFactory);

            return script;
        }

        private static string ScriptFactory(AssemblyScriptPath assemblyScriptPath)
        {
            var script = GetScriptResourceFromAssembly(assemblyScriptPath.Assembly, assemblyScriptPath.ScriptPath);

            return script;
        }

        private static string GetScriptResourceFromAssembly(Assembly assembly, string scriptPath)
        {
            var assemblyName = assembly.GetName().Name;
            
            using (var stream = assembly.GetManifestResourceStream($"{assemblyName}.{scriptPath}"))
            {
                if (stream == null)
                {
                    throw new ScriptNotFoundException(assemblyName, scriptPath);
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}