using System.IO;

namespace Moedelo.Infrastructure.Http.Abstractions.Models;

public sealed class HttpFileModel
{
    public HttpFileModel(string fileName, string contentType, Stream stream)
    {
        FileName = fileName;
        ContentType = contentType;
        Stream = stream;
    }

    public string FileName { get; }

    public string ContentType { get; }

    public Stream Stream { get; }
}