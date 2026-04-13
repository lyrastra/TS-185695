using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.SqlDataAccess.Abstractions.Models;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Extensions
{
    internal static class TemporaryTableExtensions
    {
        internal static IReadOnlyCollection<TemporaryTableDump> DumpTemporaryTables(
            this IReadOnlyCollection<TemporaryTable> temporaryTables)
        {
            if (temporaryTables == null || temporaryTables.Count == 0)
            {
                return Array.Empty<TemporaryTableDump>();
            }

            return temporaryTables.Select(DumpTemporaryTable).ToArray();
        }

        private static TemporaryTableDump DumpTemporaryTable(TemporaryTable temporaryTable)
        {
            return new TemporaryTableDump
            {
                Name = temporaryTable.Name,
                CreateSql = temporaryTable.CreateSql,
                TotalCount = temporaryTable.DataTable.Rows.Count,
                Data = temporaryTable.DataTable.DumpDataTableRows(),
            };
        }
    }
}
