using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Moedelo.Common.Audit.Abstractions.Interfaces;

// unit-тесты запускаются в pack-сборке md-auditTrailNetCore
[assembly: InternalsVisibleTo("Moedelo.Common.Audit.Tests")]

namespace Moedelo.Common.Audit.Extensions
{
    internal static class AuditSpanExtensions
    {
        internal static string GetNormalizedName(this IAuditSpanData span)
        {
            return NormalizeName(span.Name);
        }

        private static readonly ReplacingRegex[] NormalizationRegexList = [
            new(@"(func .*? from )(?:.*?\\.*?\\.*?\\.*?)(\\.*?$)", "$1...$2"),
            new(@"^(?:(http|https):\/\/.*?)(?<path>\/.*?)$", static m => "." + m.Groups["path"].Value.ToLowerInvariant()), /* url protocols */
            new (@"\/", "//"), /* setup double slashes */
            new (@"\/(?:[^/]*?[0-9a-fA-F]{8}[-]?(?:[0-9a-fA-F]{4}[-]?){3}[0-9a-fA-F]{12}.*?)(\/|$)", "/...$1"), /* guid in url path or some path part with guid inside (real case)*/
            new(@"\/(?:[a-fA-F0-9]{24})(\/|$)", "/...$1"), /* mongo id in url path */
            new(@"\/(?:(?:-?\d+)+)(\/|$)", "/...$1"), /* digits in url path */
            new(@"\/(?:[^/]*?\%[^/]*?)(\/|$)", "/...$1"), /* some encoded stuff in url path */
            new(@"\/\/", @"/"), /* remove double slashes */
        ];

        private static string NormalizeName(string name)
        {
            try
            {
                return NormalizationRegexList.Aggregate(name, static (agg, regex) => regex.Replace(agg));
            }
            catch
            {
                return name;
            }
        }
    }
}
