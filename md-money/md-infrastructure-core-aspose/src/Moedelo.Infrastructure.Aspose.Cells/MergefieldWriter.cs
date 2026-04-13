using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Abstraction;
using Moedelo.Infrastructure.Aspose.Cells.Enums;
using Moedelo.Infrastructure.Aspose.Cells.Extensions;
using Moedelo.Infrastructure.Aspose.Cells.Models;
using Moedelo.Infrastructure.Aspose.Cells.Models.Mergefield;
using Color = System.Drawing.Color;

namespace Moedelo.Infrastructure.Aspose.Cells
{
    internal static class MergefieldWriter
    {
        public static void MergeFields(this Workbook workbook, object model)
        {
            foreach (var worksheet in workbook.Worksheets)
            {
                worksheet.Cells.InsertData(model, null);
                worksheet.Cells.RemoveMarkers();
            }
        }

        public static void MergeListFields(this Workbook workbook, List<IWorksheet> sheets)
        {
            for (var i = 0; i < sheets.Count; i++)
            {
                workbook.Worksheets.AddCopy(0);

                workbook.Worksheets[i + 1].Name = sheets[i].WorksheetName;
                workbook.Worksheets[i + 1].Cells.InsertData(sheets[i].Model, null);
                workbook.Worksheets[i + 1].InsertPagebreaks();
                workbook.Worksheets[i + 1].Cells.RemoveMarkers();
            }

            if (workbook.Worksheets.Count > 1)
            {
                workbook.Worksheets.RemoveAt(0);
            }
        }

        private static int InsertData(this global::Aspose.Cells.Cells cells, object value, string parentName, CellArea? cellArea = null)
        {
            var insertData = value.GetType().GetProperties().Select(p => new InsertDataItem(p, value)).ToList();
            int insertedRows = 0;

            foreach (var insertDataItem in insertData.Where(i => i.DataType == InsertDataType.Simple || i.DataType == InsertDataType.None))
            {
                var modifiedRows = cells.InsertSimple(insertDataItem, parentName, cellArea);
                insertedRows += modifiedRows;

                if (modifiedRows < 0 && cellArea.HasValue)
                {
                    var modifiedArea = CellArea.CreateCellArea(cellArea.Value.StartRow, cellArea.Value.StartColumn, cellArea.Value.EndRow, cellArea.Value.EndColumn);
                    modifiedArea.EndRow += modifiedRows;

                    cellArea = modifiedArea;
                }
            }

            foreach (var insertDataItem in insertData.Where(i => i.DataType == InsertDataType.ObjectList || i.DataType == InsertDataType.SimpleList))
            {
                var modifiedRows = cells.InsertTable(insertDataItem, parentName, cellArea);
                insertedRows += modifiedRows;

                if (modifiedRows != 0 && cellArea.HasValue)
                {
                    var modifiedArea = CellArea.CreateCellArea(cellArea.Value.StartRow, cellArea.Value.StartColumn, cellArea.Value.EndRow, cellArea.Value.EndColumn);
                    modifiedArea.EndRow += modifiedRows;

                    cellArea = modifiedArea;
                }
            }

            foreach (var insertDataItem in insertData.Where(i => i.DataType == InsertDataType.CompoundObject))
            {
                var modifiedRows = cells.InsertCompound(insertDataItem, parentName, cellArea);
                insertedRows += modifiedRows;
            
                if (modifiedRows != 0 && cellArea.HasValue)
                {
                    var modifiedArea = CellArea.CreateCellArea(cellArea.Value.StartRow, cellArea.Value.StartColumn,
                        cellArea.Value.EndRow, cellArea.Value.EndColumn);
                    modifiedArea.EndRow += modifiedRows;
            
                    cellArea = modifiedArea;
                }
            }

            return insertedRows;
        }

        private static int InsertSimple(this global::Aspose.Cells.Cells cells, InsertDataItem property, string parentName, CellArea? cellArea = null)
        {
            var findOptons = new FindOptions();
            var removedRowsCount = cells.ProcessRemovedList(property.Name, parentName, property.Value, cellArea);

            if (cellArea.HasValue)
            {
                var modifiedArea = CellArea.CreateCellArea(cellArea.Value.StartRow, cellArea.Value.StartColumn, cellArea.Value.EndRow, cellArea.Value.EndColumn);
                modifiedArea.EndRow -= removedRowsCount;
                
                findOptons.SetRange(modifiedArea);
            }

            var patern = $"{{mergefield {GetPropertyFullPath(parentName, property.Name)}(?<options> .+?)?}}";
            var regex = new Regex(patern, RegexOptions.IgnoreCase);
            Cell fundedCell = null;

            while (true)
            {
                fundedCell = cells.Find($"{{mergefield {GetPropertyFullPath(parentName, property.Name)}", fundedCell, findOptons);

                if (fundedCell == null)
                {
                    break;
                }

                var match = regex.Match(fundedCell.StringValue);
                var matchValue = match.Value;

                if (String.IsNullOrEmpty(matchValue))
                {
                    break;
                }
                var optionsString = match?.Groups["options"]?.Value;
                var options = GetFieldOptions(optionsString);

                ApplyFieldOptions(fundedCell, matchValue, property.Value, options);
            }

            return -removedRowsCount;
        }

        private static int InsertTable(this global::Aspose.Cells.Cells cells, InsertDataItem property, string parentName, CellArea? findRange = null)
        {
            var removedRowsCount = cells.ProcessRemovedList(property.Name, parentName, property.Value, findRange);

            if (findRange.HasValue)
            {
                var modifiedArea = CellArea.CreateCellArea(findRange.Value.StartRow, findRange.Value.StartColumn, findRange.Value.EndRow, findRange.Value.EndColumn); 
                modifiedArea.EndRow -= removedRowsCount;
                
                findRange = modifiedArea;
            }

            int totalInsertedRows = -removedRowsCount;

            if (property.Value == null)
            {
                return 0;
            }

            var valueList = ((IEnumerable)property.Value).Cast<object>().ToArray();
            parentName = GetPropertyFullPath(parentName, property.Name);
            var tablesInfoList = cells.GetTableInfoList(property.Value.GetType(), parentName, findRange);

            foreach (var tableInfo in tablesInfoList)
            {
                var currentCellArea = tableInfo.CellArea;
                var templateRowCount = currentCellArea.EndRow - currentCellArea.StartRow + 1;
                currentCellArea.StartRow += totalInsertedRows;
                currentCellArea.EndRow += totalInsertedRows;

//                var insertedRows = 0;
                for (var i = 0; i < valueList.Length; i++)
                {
                    var insertedRows = 0;
                    var nextCellArea = i >= valueList.Length - 1 ? currentCellArea : cells.CopyTableRowPattern(currentCellArea);
                    insertedRows += cells.InsertData(valueList[i], parentName, currentCellArea);
                    insertedRows -= removedRowsCount;

                    currentCellArea.EndRow += insertedRows;
                    tableInfo.AddRowInfo(new RowInfo(currentCellArea));

                    nextCellArea.StartRow += insertedRows;
                    nextCellArea.EndRow += insertedRows;

                    currentCellArea = nextCellArea;
                    totalInsertedRows += i > 0
                        ? insertedRows == 0 ? templateRowCount : templateRowCount + insertedRows
                        : insertedRows;
                }

                cells.ApplyTablieOptions(tableInfo);
                cells.RemoveMarkers(tableInfo.CellArea);
            }

            return totalInsertedRows;
        }
        
        private static int InsertCompound(this global::Aspose.Cells.Cells cells, InsertDataItem property, string parentName, CellArea? findRange = null)
        {
            if (property.Value == null)
            {
                return 0;
            }

            var valueList = property.Value;
            var propertyName = GetPropertyFullPath(parentName, property.Name);

            return cells.InsertData(valueList, propertyName, findRange);
        }

        private static List<TableInfo> GetTableInfoList(this global::Aspose.Cells.Cells cells, Type propertyType, string propertyName, CellArea? findRange = null)
        {
            var result = new List<TableInfo>();
            var findOptons = new FindOptions();

            if (findRange.HasValue)
            {
                findOptons.SetRange(findRange.Value);
            }

            var patern = $"{{TableStart:{propertyName}(?<options> .+?)?}}";
            var regex = new Regex(patern, RegexOptions.IgnoreCase);
            Cell firstTableCell = null;
            Cell lastTableCell = null;

            while (true)
            {
                firstTableCell = cells.Find($"{{TableStart:{propertyName}", firstTableCell, findOptons);
                lastTableCell = cells.Find($"{{TableEnd:{propertyName}}}", lastTableCell, findOptons);

                if (firstTableCell == null || lastTableCell == null)
                {
                    break;
                }

                var cellArea = CellArea.CreateCellArea(firstTableCell.Row, firstTableCell.Column, lastTableCell.Row, lastTableCell.Column);
                var optionsString = regex.Match(firstTableCell.StringValue)?.Groups["options"]?.Value;

                result.Add(new TableInfo(cellArea)
                {
                    Columns = GetColumnInfo(cells, propertyType, propertyName, cellArea),
                    Options = GetTableOptions(optionsString)
                });
            }

            return result;
        }

        private static int ProcessRemovedList(this global::Aspose.Cells.Cells cells, string propertyName, string parentName, object val, CellArea? findRange = null)
        {
            if (null != val)
            {
                return 0;
            }

            //            if ((val as bool?).Value != null)
            //            {
            //                
            //            }

            var result = new List<TableInfo>();
            var findOptons = new FindOptions();

            if (findRange.HasValue)
            {
                findOptons.SetRange(findRange.Value);
            }

            var patern = $"{{RemoveStart:{GetPropertyFullPath(parentName, propertyName)}}}";
            var regex = new Regex(patern, RegexOptions.IgnoreCase);


            Cell firstTableCell = null;
            Cell lastTableCell = null;
            if (findRange.HasValue)
            {
                firstTableCell = cells[findRange.Value.StartRow, findRange.Value.StartColumn];
                lastTableCell = firstTableCell;
            }

            int removedRows = 0;
            
            while (true)
            {
                firstTableCell = cells.Find($"{{RemoveStart:{GetPropertyFullPath(parentName, propertyName)}", firstTableCell, findOptons);
                lastTableCell = cells.Find($"{{RemoveEnd:{GetPropertyFullPath(parentName, propertyName)}}}", lastTableCell, findOptons);

                if (firstTableCell == null || lastTableCell == null)
                {
                    break;
                }

                var cellArea = CellArea.CreateCellArea(firstTableCell.Row, firstTableCell.Column, lastTableCell.Row, lastTableCell.Column);
                cells.DeleteRows(cellArea.StartRow, cellArea.EndRow - cellArea.StartRow);

                removedRows += cellArea.EndRow - cellArea.StartRow;
            }

            return removedRows;
        }

        private static List<ColumnInfo> GetColumnInfo(global::Aspose.Cells.Cells cells, Type propertyType, string parentName, CellArea cellArea)
        {
            var result = new List<ColumnInfo>();
            var columns = propertyType.GetGenericArguments().Single().GetProperties().ToList();

            foreach (var column in columns)
            {
                var findOptons = new FindOptions
                {
                    RegexKey = true,
                    LookInType = LookInType.Values,
                    LookAtType = LookAtType.EntireContent
                };

                findOptons.SetRange(cellArea);

                var propertyFullName = GetPropertyFullPath(parentName, column.Name);
                var patern = $"{{mergefield {propertyFullName}( ?.*?)}}";
                var regex = new Regex(patern, RegexOptions.IgnoreCase).ToString();
                var fundedCell = cells.Find(regex, null, findOptons);

                if (fundedCell == null)
                {
                    continue;
                }

                result.Add(new ColumnInfo
                {
                    Name = propertyFullName,
                    Index = fundedCell.Column,
                    DataType = column.GetInsertDataType()
                });
            }

            return result;
        }

        private static TableOptions GetTableOptions(string optionsString)
        {
            var options = new TableOptions();

            if (string.IsNullOrWhiteSpace(optionsString))
            {
                return options;
            }

            options.MergeRowsByColumns = Regex.IsMatch(optionsString, @"\\mergeRowsByColumns", RegexOptions.IgnoreCase);

            return options;
        }

        private static FieldOptions GetFieldOptions(string optionsString)
        {
            var options = new FieldOptions();

            if (string.IsNullOrWhiteSpace(optionsString))
            {
                return options;
            }

            options.DateFormat = Regex.Match(optionsString, @"\\@ (?<dateformat>.+?)(\\|$)", RegexOptions.IgnoreCase)?.Groups["dateformat"]?.Value.Trim();
            options.NumberFormat = Regex.Match(optionsString, @"\\# (?<numberformat>.+?)(\\|$)", RegexOptions.IgnoreCase)?.Groups["numberformat"]?.Value.Trim();
            options.AutoFitRow = Regex.IsMatch(optionsString, @"\\autoFitRow", RegexOptions.IgnoreCase);
            options.AutoCellBorder = Regex.IsMatch(optionsString, @"\\autoCellBorder", RegexOptions.IgnoreCase);
            var index = Regex.Match(optionsString, @"\\index (?<index>.+?)(\\|$)", RegexOptions.IgnoreCase)?.Groups["index"]?.Value.Trim();
            options.Index = string.IsNullOrEmpty(index) ? 0 : Convert.ToInt32(index);
            
            return options;
        }

        private static CellArea CopyTableRowPattern(this global::Aspose.Cells.Cells cells, CellArea cellArea)
        {
            var totalRows = cellArea.EndRow - cellArea.StartRow + 1;
            var startRow = cellArea.EndRow + 1;
            var endRow = cellArea.EndRow + totalRows;

            cells.InsertRows(startRow, totalRows);
            cells.CopyRows(cells, cellArea.StartRow, startRow, totalRows);

            return CellArea.CreateCellArea(startRow, cellArea.StartColumn, endRow, cellArea.EndColumn);
        }

        private static void RemoveMarkers(this global::Aspose.Cells.Cells cells, CellArea? cellArea = null)
        {
            var findOptons = new FindOptions()
            {
                RegexKey = true,
                LookInType = LookInType.Values,
                LookAtType = LookAtType.EntireContent
            };

            if (cellArea.HasValue)
            {
                findOptons.SetRange(cellArea.Value);
            }

            var pattern = "{mergefield .+}|{TableStart:.+}|{TableEnd:.+}|{RemoveStart:.+}|{RemoveEnd:.+}";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Cell cell = null;

            while (true)
            {
                cell = cells.Find(pattern, cell, findOptons);

                if (cell == null)
                {
                    break;
                }

                cell.PutValue(regex.Replace(cell.StringValue, ""));
            }
        }

        private static string GetPropertyFullPath(string parentName, string propertyName)
        {
            return $"{(string.IsNullOrWhiteSpace(parentName) ? "" : $"{parentName}.")}{propertyName}";
        }

        private static void ApplyFieldOptions(this Cell cell, string matchValue, object value, FieldOptions options)
        {
            if (value == null)
            {
                cell.PutValue("");

                return;
            }

            if (!string.IsNullOrWhiteSpace(options.DateFormat))
            {
                value = Convert.ToDateTime(value).ToString(options.DateFormat);
            }

            if ((value is decimal || value is double || value is float) && !string.IsNullOrWhiteSpace(options.NumberFormat))
            {
                value = Convert.ToDecimal(value).ToString(options.NumberFormat);
            }

            if ((value is byte || value is short || value is int || value is long) &&
                !string.IsNullOrWhiteSpace(options.NumberFormat))
            {
                value = Convert.ToInt64(value).ToString(options.NumberFormat);
            }

            if (options.Index > 0)
            {
                var i = options.Index;
                var str = value.ToString();
                value = i <= str.Length ? str[i - 1].ToString() : string.Empty;
            }

            var result = cell.StringValue.Replace(matchValue, value.ToString());

            cell.PutValue(result);

            if (options.AutoFitRow)
            {
                cell.Worksheet.AutoFitRows(cell.Row, cell.Row, new AutoFitterOptions { IgnoreHidden = true });
            }
            
            if (options.AutoCellBorder)
            {
                var cellBorderType = CellBorderType.Thin;
                var color = Color.Black;

                var cellRange = cell.GetMergedRange() ?? cell.Worksheet.Cells.CreateRange(cell.Row, cell.Column, 1, 1);
                cellRange.SetOutlineBorder(BorderType.TopBorder, cellBorderType, color);
                cellRange.SetOutlineBorder(BorderType.BottomBorder, cellBorderType, color);
                cellRange.SetOutlineBorder(BorderType.LeftBorder, cellBorderType, color);
                cellRange.SetOutlineBorder(BorderType.RightBorder, cellBorderType, color);
            }
        }

        private static void ApplyTablieOptions(this global::Aspose.Cells.Cells cells, TableInfo tableInfo)
        {
            if (tableInfo.Options.MergeRowsByColumns)
            {
                cells.MergeRowsByColumns(tableInfo);
            }
        }

        private static void MergeRowsByColumns(this global::Aspose.Cells.Cells cells, TableInfo tableInfo)
        {
            foreach (var row in tableInfo.Rows)
            {
                var totalRows = row.CellArea.EndRow - row.CellArea.StartRow + 1;

                foreach (var column in tableInfo.Columns.Where(x => x.DataType == InsertDataType.Simple || x.DataType == InsertDataType.CompoundObject))
                {
                    try
                    {
                        cells.Merge(row.CellArea.StartRow, column.Index, totalRows, 1);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Rows {row.CellArea.StartRow} - {row.CellArea.EndRow}", ex);
                    }
                }
            }
        }

        private static void InsertPagebreaks(this Worksheet worksheet, CellArea? cellArea = null)
        {
            var findOptons = new FindOptions
            {
                RegexKey = true,
                LookInType = LookInType.Values,
                LookAtType = LookAtType.EntireContent
            };

            if (cellArea.HasValue)
            {
                findOptons.SetRange(cellArea.Value);
            }

            var patern = "{pagebreak}";
            var regex = new Regex(patern, RegexOptions.IgnoreCase).ToString();
            Cell cell = null;

            while (true)
            {
                cell = worksheet.Cells.Find(regex, cell, findOptons);

                if (cell == null)
                {
                    break;
                }

                worksheet.HorizontalPageBreaks.Add(cell.Row, cell.Column);
                cell.PutValue("");
            }
        }
    }
}