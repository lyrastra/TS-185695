using System;
using System.IO;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.UploadedFiles.Client.Extensions;

internal static class HttpFileModelExtensions
{
    internal static void EnsureFileStreamAtStartPosition(this HttpFileModel model)
    {
        if (model.Stream.Position != 0 && model.Stream.CanSeek)
        {
            model.Stream.Seek(0, SeekOrigin.Begin);
        }

        if (model.Stream.Position != 0)
        {
            throw new ArgumentException("Поток должен быть в начальной позиции чтения", nameof(model.Stream));
        }
    }

    internal static void EnsureFileStreamAtStartPosition(this HttpFileStream model)
    {
        if (model.Stream.Position != 0 && model.Stream.CanSeek)
        {
            model.Stream.Seek(0, SeekOrigin.Begin);
        }

        if (model.Stream.Position != 0)
        {
            throw new ArgumentException("Поток должен быть в начальной позиции чтения", nameof(model.Stream));
        }
    }
}
