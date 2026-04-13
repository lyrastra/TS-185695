using System;

namespace Moedelo.InfrastructureV2.MySqlDataAccess;

public class MySqlBulkLoaderSettings
{
    public string FieldTerminator { get; set; } = ";";

    public string LineTerminator { get; set; } = Environment.NewLine;
        
    public int? Timeout { get; set; }
}