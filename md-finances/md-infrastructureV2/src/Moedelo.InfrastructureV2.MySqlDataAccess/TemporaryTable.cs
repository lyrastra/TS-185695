using System;

namespace Moedelo.InfrastructureV2.MySqlDataAccess;

public sealed class TemporaryTable
{
    public TemporaryTable(string name, string createTableSql, string data)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (!name.StartsWith("Temp"))
            throw new ArgumentException("Name of temporary table should start with 'Temp'");
            
        if (string.IsNullOrEmpty(createTableSql))
            throw new ArgumentNullException(nameof(createTableSql));
            
        if (string.IsNullOrEmpty(data))
            throw new ArgumentNullException(nameof(data));

        Name = name;
        CreateSql = createTableSql;
        Data = data;
    }

    public string Name { get; }
        
    public string CreateSql { get; }

    public string DropSql => $"drop table {Name}";
        
    public string Data { get; }
}