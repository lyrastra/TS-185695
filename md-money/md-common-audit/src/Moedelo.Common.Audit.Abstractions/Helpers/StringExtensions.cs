using System.Text.RegularExpressions;

namespace Moedelo.Common.Audit.Abstractions.Helpers
{
    public static class StringExtensions
    {
        private static readonly Regex pathToMoedeloProjectDirectory = new Regex(@"^.*?\\(Moedelo\..*)$", RegexOptions.Compiled);
        private static readonly Regex fileName = new Regex(@"^.*\\(\w+).cs$", RegexOptions.Compiled);
        
        public static string NormalizeSourceFilePath(this string sourceFilePath)
        {
            return pathToMoedeloProjectDirectory
                .Replace(sourceFilePath, @"$1")
                .Replace("\\", "/");
        }

        public static string GetSourceFileName(this string sourceFilePath)
        {
            var matches = fileName.Match(sourceFilePath);

            if (matches.Success)
            {
                return matches.Groups[1].Value;
            }

            return null;
        }
    }
}
