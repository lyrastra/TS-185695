using System;
using System.IO;

namespace Moedelo.Infrastructure.Http.Abstractions.Models;

public class HttpFileStream : IDisposable
{
    private readonly bool disposeStream;

    public HttpFileStream(string fileName, string contentType, Stream stream, bool disposeStream = true)
    {
        FileName = fileName;
        ContentType = contentType;
        Stream = stream;
        this.disposeStream = disposeStream;
    }

    public void Dispose()
    {
        if (disposeStream)
        {
            Stream?.Dispose();
        }
        Stream = null;
    }

    /// <summary>
    /// "Освободить" поток данных и заменить хранимое значение на null.
    /// </summary>
    /// <returns>поток данных с содержимым файла</returns>
    public Stream ReleaseStream()
    {
        var stream = Stream;
        Stream = null;

        return stream;
    }

    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Тип содержимого файла
    /// </summary>
    public string ContentType { get; }

    /// <summary>
    /// Потом данных с содержимым файла
    /// </summary>
    public Stream Stream { get; private set; }
}
