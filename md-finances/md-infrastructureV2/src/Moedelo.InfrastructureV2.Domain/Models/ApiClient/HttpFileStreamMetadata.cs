using System.IO;

namespace Moedelo.InfrastructureV2.Domain.Models.ApiClient;

public sealed class HttpFileStreamMetadata<TMetadata> : Stream
{
    private Stream stream;

    public HttpFileStreamMetadata(
        string fileName,
        Stream stream,
        string contentType,
        TMetadata metadata)
    {
        this.stream = stream;
        FileName = fileName;
        ContentType = contentType;
        Metadata = metadata;
    }
    
    public string FileName { get; }
    public string ContentType { get; }
    public TMetadata Metadata { get; }

    protected override void Dispose(bool disposing)
    {
        stream.Dispose();
        this.stream = Stream.Null;

        base.Dispose(disposing);
    }

    public Stream ReleaseStream()
    {
        var streamRef = this.stream;
        this.stream = Stream.Null;

        return streamRef;
    }

    public override void Flush()
    {
        stream.Flush();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return stream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        stream.SetLength(value);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return stream.Read(buffer, offset, count);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        stream.Write(buffer, offset, count);
    }

    public override bool CanRead => stream.CanRead;

    public override bool CanSeek => stream.CanSeek;

    public override bool CanWrite => stream.CanWrite;

    public override long Length => stream.Length;

    public override long Position
    {
        get => stream.Position;
        set => stream.Position = value;
    }
}
