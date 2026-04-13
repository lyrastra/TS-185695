using System;

namespace Moedelo.InfrastructureV2.Domain.Models.MongoFiles;

public class MongoFileInfo
{
    public string ObjectId { get; set; }

    public string Name { get; set; }

    public DateTime DateUpload { get; set; }

    /// <summary>
    /// размер файла в байтах
    /// </summary>
    public long ContentSize { get; set; }
}