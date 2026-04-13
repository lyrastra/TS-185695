namespace Moedelo.Common.Http.Generic.Abstractions;

public readonly struct HttpFileModel
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