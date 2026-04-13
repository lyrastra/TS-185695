using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Moedelo.Infrastructure.Aspose.Cells
{
    public class ReportBuilder
    {
        private readonly Workbook _workbook;
        private readonly object _model;
        private readonly Worksheet _currentWorksheet;

        public ReportBuilder(Workbook document, object model)
        {
            _workbook = document;
            _currentWorksheet = document.Worksheets[0];
            _model = model;
        }
        
        public ReportBuilder(Workbook document, object model, int worksheetIndex)
        {
            _workbook = document;
            _currentWorksheet = document.Worksheets[worksheetIndex];
            _model = model;
        }

        public Workbook Build()
        {
            Put(_model, null);
            return _workbook;
        }

        private void Put(object value, string propertyName)
        {
            if (IsValueType(value))
            {
                PutValueInSingleCell(value, propertyName);
            }
            else
            {
                var list = value as IList;
                if (list != null)
                {
                    PutList(list, propertyName);
                }
                else
                {
                    PutObject(value, propertyName);
                }
            }
        }

        private static bool IsValueType(object value)
        {
            return value == null || value is ValueType || value is string || value is byte[];
        }

        private void PutObject(object value, string propertyName)
        {
            foreach (var property in value.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(value, null);
                var name = GetFullPropertyName(propertyName, property.Name);

                Put(propertyValue, name);
            }
        }

        private string GetFullPropertyName(string parentName, string name)
        {
            return parentName != null ? $"{parentName}.{name}" : name;
        }

        private void PutValueInSingleCell(object value, string propertyName)
        {
            foreach (var cell in FindCells($"%{propertyName}%"))
            {
                cell.PutValue(value?.ToString() ?? string.Empty);
            }
        }

        private void PutList(IList value, string propertyName)
        {
            var writer = new TableWriter(_currentWorksheet, value, propertyName);
            writer.PutTable();
        }

        private IEnumerable<Cell> FindCells(string cellValue, bool useRegex = false)
        {
            Cell cell = null;

            foreach (Worksheet worksheet in _workbook.Worksheets)
            {
                if (_currentWorksheet != null && _currentWorksheet != worksheet)
                {
                    continue;
                }

                while (true)
                {
                    cell = worksheet.Cells.Find(cellValue, cell,
                        new FindOptions { RegexKey = useRegex, LookAtType = LookAtType.EntireContent });

                    if (cell != null)
                    {
                        yield return cell;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}