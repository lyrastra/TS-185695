using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public sealed class BulkCopySetting
{
    public BulkCopySetting(
        SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default,
        SqlTransaction? transaction = null,
        int? timeout = null,
        IReadOnlyCollection<SqlBulkCopyColumnMapping>? mappings = null)
    {
        CopyOptions = copyOptions;
        Transaction = transaction;
        Timeout = timeout;
        Mappings = mappings;
    }

    public SqlBulkCopyOptions CopyOptions { get; }

    public SqlTransaction? Transaction { get; }

    public int? Timeout { get; }

    /// <summary>
    /// Если названия полей в таблице не совпадают с названием полей класса, 
    /// нужно переопределить маппинг для всех колонок в таблице в формате: { название_поля_в_классе, название_колонки_в_бд}
    /// </summary>
    public IReadOnlyCollection<SqlBulkCopyColumnMapping>? Mappings { get; }
}