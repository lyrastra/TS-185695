using System.Collections.Generic;
// using Microsoft.Data.SqlClient;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public class BulkCopySetting
{
    public BulkCopySetting() : this(SqlBulkCopyOptions.Default, timeout: null)
    {
    }

    public BulkCopySetting(SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default, int? timeout = null)
    {
        CopyOptions = copyOptions;
        Timeout = timeout;
    }

    public SqlBulkCopyOptions CopyOptions { get; set; }

    /// <summary>
    /// Если названия полей в таблице не совпадают с названием полей класса, 
    /// нужно переопределить маппинг для всех колонок в таблице в формате: { название_поля_в_классе, название_колонки_в_бд}
    /// </summary>
    public IReadOnlyCollection<SqlBulkCopyColumnMapping> Mappings { get; set; }

    public int? Timeout { get; set; }
}