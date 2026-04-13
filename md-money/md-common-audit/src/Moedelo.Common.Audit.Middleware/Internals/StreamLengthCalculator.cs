using System;
using System.IO;

namespace Moedelo.Common.Audit.Middleware.Internals;

/// <summary>
/// Класс для подсчёта длины записываемых данных
/// </summary>
internal sealed class StreamLengthCalculator : Stream
{
    private long length;

    public override void Flush()
    {
        // do nothing
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException("это поток-заглушка, из него нельзя читать");
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new InvalidOperationException("это поток-заглушка, в нём нельзя менять позицию");
    }

    public override void SetLength(long value)
    {
        throw new InvalidOperationException("это поток-заглушка, у него нельзя выставить длину");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        length += count;
    }

    public override void WriteByte(byte value)
    {
        length++;
    }

    public override long Position
    {
        get => length;
        set => throw new InvalidOperationException("нельзя менять позицию у этого потока");
    }

    public override bool CanSeek => false;
    public override bool CanRead => false;
    public override bool CanWrite => true;
    public override long Length => length;
}

