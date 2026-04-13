using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.ApiClient.Internals;

internal sealed class HttpResponseMessageStream : Stream
{
    private readonly Stream responseStream;
    private readonly HttpResponseMessage httpResponseMessage;
    private readonly bool disposeStream;

    private HttpResponseMessageStream(HttpResponseMessage httpResponseMessage, Stream responseStream, bool disposeStream)
    {
        this.httpResponseMessage = httpResponseMessage;
        this.responseStream = responseStream;
        this.disposeStream = disposeStream;
    }

    internal static async Task<HttpResponseMessageStream> CreateAsync(HttpResponseMessage httpResponseMessage)
    {
        var stream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

        return new HttpResponseMessageStream(httpResponseMessage, stream, disposeStream: false);
    }
    
    internal static HttpResponseMessageStream Create(HttpResponseMessage httpResponseMessage, Stream stream)
    {
        return new HttpResponseMessageStream(httpResponseMessage, stream, disposeStream: true);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposeStream)
        {
            responseStream.Dispose();
        }
        httpResponseMessage.Dispose();
    }

    public override void Flush()
    {
        responseStream.Flush();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return responseStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        responseStream.SetLength(value);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return responseStream.Read(buffer, offset, count);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        responseStream.Write(buffer, offset, count);
    }

    public override bool CanRead => responseStream.CanRead;

    public override bool CanSeek => responseStream.CanSeek;

    public override bool CanWrite => responseStream.CanWrite;

    public override long Length => responseStream.Length;

    public override long Position
    {
        get => responseStream.Position;
        set => responseStream.Position = value;
    }
}
