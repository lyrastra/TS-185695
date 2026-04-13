namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class ConsulKeyPathExtensions
{
    private const char PathDelimiterChar = '/';
    private const string PathDelimiter = "/";
    /// <summary>
    /// Разделитель частей пути в consul, используемый в программном коде 
    /// </summary>
    private const char CodePathDelimiter = ':';

    internal static string NormalizeConsulKeyDirectoryPath(this string keyPath)
    {
        if (!keyPath.EndsWith(PathDelimiter))
        {
            keyPath += PathDelimiter;
        }

        if (keyPath.StartsWith(PathDelimiter))
        {
            keyPath = keyPath.TrimStart(PathDelimiterChar);
        }

        return keyPath;
    }

    internal static string NormalizeConsulKeyPath(this string keyPath)
    {
        return keyPath.Trim(PathDelimiterChar);
    }

    internal static string ConvertConsulPathToCodeKeyPath(this string consulPath)
    {
        return consulPath.Replace(PathDelimiterChar, CodePathDelimiter);
    }
}
