using System.Data;

namespace Moedelo.InfrastructureV2.DataAccess.Internals;

internal readonly struct TemporaryTable
{
    public TemporaryTable(DataTable dataTable, string creationSql)
    {
        DataTable = dataTable;
        CreationSql = creationSql;
    }
    
    public DataTable DataTable { get; }
    public string SqlTableName => $"#{DataTable.TableName}";
    public string CreationSql { get; }
    public string DropSql => $"drop table {SqlTableName}";
}
