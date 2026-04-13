using System;
using System.Collections.Generic;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Models
{
    internal struct TemporaryTableDump
    {
        public string Name { get; set; }
        public string CreateSql { get; set; }
        public int DumpedRowCount => Math.Min(TotalCount, Data?.Count ?? 0);
        public int TotalCount { get; set; }
        public IReadOnlyCollection<object> Data { get; set; }
    }
}
