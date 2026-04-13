using System.IO;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Extensions;

internal static class StreamExtensions
{
    internal static long? GetLengthOrNullSafely(this Stream stream)
    {
        if (stream is not {CanSeek: true})
        {
            return null;
        }

        try
        {
            return stream.Length;
        }
        catch
        {
            return null;
        }
    }
}
