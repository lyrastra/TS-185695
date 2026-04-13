using System;
using System.Data;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Extensions
{
    internal static class DataTableExtensions
    {
        // ограничение количества строк, которое попадает в дамп
        private const int MaxRowsToDump = 100;

        internal static object[] DumpDataTableRows(this DataTable dataTable, uint maxRowsToDump = MaxRowsToDump)
        {
            if (dataTable == null)
            {
                return null;
            }

            var columns = dataTable.Columns;
            var colsCount = dataTable.Columns.Count;
            var dumpCapacity = Math.Min(maxRowsToDump, dataTable.Rows.Count);

            if (colsCount == 0 || dumpCapacity == 0)
            {
                // или нет ни одной колонки в таблице или нет ни одной строки
                return Array.Empty<object>();
            }

            var rowDumps = new object[dumpCapacity];
            var firstColumn = dataTable.Columns[0];

            for (var rowIndex = 0; rowIndex < dumpCapacity; ++rowIndex)
            {
                var row = dataTable.Rows[rowIndex];

                rowDumps[rowIndex] = colsCount == 1
                    ? row[firstColumn]
                    : DumpMultiColumnsRow(row, columns);
            }

            return rowDumps;
        }

        private static object DumpMultiColumnsRow(DataRow row, DataColumnCollection columns)
        {
            var columnsCount = columns.Count;

            var valueList = new object[columnsCount];
            for (var columnIndex = 0; columnIndex < columnsCount; ++columnIndex)
            {
                valueList[columnIndex] = row[columns[columnIndex]];
            }

            return valueList;
        }
    }
}
