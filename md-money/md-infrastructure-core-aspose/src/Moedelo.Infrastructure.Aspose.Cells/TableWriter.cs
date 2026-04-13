using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Moedelo.Infrastructure.Aspose.Cells
{
    public class TableWriter
    {
        private readonly string propertyName;
        private readonly Worksheet worksheet;
        private bool rowsInserted;
        private Cell topCell;

        /// <summary>
        /// Инициализирует объект, предназначенный для заполнения листа электронной книги коллекцией данных
        /// </summary>
        /// <param name="worksheet">Лист электронной книги</param>
        /// <param name="value">Коллекция данных</param>
        /// <param name="propertyName">Шаблон поля</param>
        public TableWriter(Worksheet worksheet, IList value, string propertyName)
        {
            this.worksheet = worksheet;
            List = value;
            this.propertyName = propertyName;
            rowsInserted = false;
        }

        private IList List { get; }

        private Range TopCellMergeRange => topCell.GetMergedRange();

        public void PutTable(bool rawCellValue = false)
        {
            foreach (var itemProperty in GetListItemType().GetProperties())
            {
                foreach (var cell in FindTopCellsForProperty(itemProperty))
                {
                    topCell = cell;

                    if (List.Count == 0)
                    {
                        RemoveTemplateRow();
                        return;
                    }

                    InsertRows();

                    PutColumn(itemProperty, rawCellValue);
                }
            }
        }

        private Type GetListItemType()
        {
            var type = List.GetType();
            return type.IsArray
                ? type.GetElementType()
                : type.GetGenericArguments()[0];
        }

        private void RemoveTemplateRow()
        {
            worksheet.Cells.DeleteRow(topCell.Row);
        }

        private void PutColumn(PropertyInfo itemProperty, bool rawCellValue)
        {
            for (var i = 0; i < List.Count; i++)
            {
                var curCell = worksheet.Cells[topCell.Row + i, topCell.Column];
                var curCellValue = itemProperty.GetValue(List[i], null);

                MergeColumnCells(curCell);

                var value = rawCellValue ? curCellValue : curCellValue?.ToString();
                curCell.PutValue(value ?? string.Empty);
            }
        }

        private void MergeColumnCells(Cell curCell)
        {
            if (TopCellMergeRange != null)
            {
                worksheet.Cells.Merge(curCell.Row, curCell.Column, 1, TopCellMergeRange.ColumnCount);
            }
        }

        private void InsertRows()
        {
            if (rowsInserted)
            {
                return;
            }

            worksheet.Cells.InsertRows(topCell.Row + 1, List.Count - 1);

            rowsInserted = true;
        }

        private IEnumerable<Cell> FindTopCellsForProperty(PropertyInfo itemProperty)
        {
            Cell cell = null;

            while (true)
            {
                cell = worksheet.Cells.Find($"%{propertyName}.{itemProperty.Name}%", cell, new FindOptions());

                if (cell != null)
                {
                    yield return cell;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}