using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

public static class HttpFileModelExtensions
{
    public static HttpFileStream ToHttpFileStream(this HttpFileModel file, bool disposeStream)
    {
        return new HttpFileStream(file.FileName, file.ContentType, file.Stream, disposeStream);
    }
}
