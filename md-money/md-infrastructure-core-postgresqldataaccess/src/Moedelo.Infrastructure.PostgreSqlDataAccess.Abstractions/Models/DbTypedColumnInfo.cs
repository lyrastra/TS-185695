#nullable enable
namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

public readonly record struct DbTypedColumnInfo(
    string ColumnName, string DbTypeName);
