namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public sealed class SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
{
    public string SourceColumn { get; set; } = sourceColumn;
    public string DestinationColumn { get; set; } = destinationColumn;
}
