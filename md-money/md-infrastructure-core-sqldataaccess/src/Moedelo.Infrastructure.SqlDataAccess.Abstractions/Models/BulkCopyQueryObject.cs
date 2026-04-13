using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

public sealed class BulkCopyQueryObject : IBulkCopyQueryObject
{
    public BulkCopyQueryObject(string name, DataTable dataTable)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
        DataTable = dataTable ?? throw new ArgumentNullException(nameof(dataTable));
    }

    public static BulkCopyQueryObject FromCollection<T>(string name, IEnumerable<T> collection)
        where T : class
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        var tmp = collection.ToTemporaryTable("bulk");

        return new BulkCopyQueryObject(name, tmp.DataTable);
    }

    public string Name { get; }

    public DataTable DataTable { get; }
}
