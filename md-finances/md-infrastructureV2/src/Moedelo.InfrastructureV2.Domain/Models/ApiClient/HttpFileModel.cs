using System.IO;

namespace Moedelo.InfrastructureV2.Domain.Models.ApiClient;

public class HttpFileModel
{
    public string FileName { get; set; }

    public string ContentType { get; set; }

    public MemoryStream Stream { get; set; }
}