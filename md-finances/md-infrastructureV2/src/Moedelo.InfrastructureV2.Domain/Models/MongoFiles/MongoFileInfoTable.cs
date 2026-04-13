using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Models.MongoFiles;

public class MongoFileInfoTable
{
    public long TotalCount { get; set; }

    public List<MongoFileInfo> FileInfos { get; set; }
}