namespace Moedelo.Common.Http.Generic.Abstractions;

public readonly struct HttpFileWithMetadataModel<TMetadata> 
{
    private HttpFileWithMetadataModel(string fileName, string contentType, Stream stream, TMetadata? metadata, bool isEmpty)
    {
        FileName = fileName;
        ContentType = contentType;
        Stream = stream;
        Metadata = metadata;
        IsEmpty = isEmpty;
    }

    public HttpFileWithMetadataModel(string fileName, string contentType, Stream stream, TMetadata metadata)
    : this(fileName, contentType, stream, metadata, isEmpty: false)
    {
    }

    public static HttpFileWithMetadataModel<TMetadata> Empty() =>
        new ("", "", Stream.Null, default, isEmpty: true);

    public string FileName { get; }

    public string ContentType { get; }

    public Stream Stream { get; }
    
    public TMetadata? Metadata { get; }

    public bool IsEmpty { get; }
}
