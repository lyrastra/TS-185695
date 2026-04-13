using System.Reflection;

namespace Moedelo.Common.Consul.Internals;

internal static class Env
{
    private static readonly Lazy<string> LazyEntryAssemblyName = new Lazy<string>(GetEntryAssemblyName);
    private static readonly Lazy<string> LazyMachineName = new Lazy<string>(static () => Environment.MachineName);
    
    internal static string EntryAssemblyName => LazyEntryAssemblyName.Value;
    internal static string MachineName => LazyMachineName.Value;
    
    private static string GetEntryAssemblyName()
    {
        const string fallback = "Unknown";
        
        try
        {
            // Пытаемся получить основную assembly приложения
            return Assembly.GetEntryAssembly()?.GetName().Name
                   ?? Assembly.GetExecutingAssembly()?.GetName().Name
                   ?? fallback;
        }
        catch
        {
            // В случае ошибки возвращаем fallback значение
            return fallback;
        }
    }
}
