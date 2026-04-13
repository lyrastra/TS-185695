using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Cells;
using Moedelo.CommonV2.Cells.Helpers;
using Moedelo.CommonV2.Cells.Models.Settings;

namespace Moedelo.CommonV2.Cells.Business
{
    public class TableWriter
    {
        private readonly string propertyName;
        private readonly Worksheet worksheet;
        private readonly ReportSettings settings;
        private bool rowsInserted;
        private Cell topCell;

        /// <summary>
        /// Инициализирует объект, предназначенный для заполнения листа электронной книги коллекцией данных
        /// </summary>
        /// <param name="worksheet">Лист электронной книги</param>
        /// <param name="value">Коллекция данных</param>
        /// <param name="propertyName">Шаблон поля</param>
        /// <param name="settings">Настройки форматов значений в ячейках</param>
        public TableWriter(Worksheet worksheet, IList value, string propertyName, ReportSettings settings)
        {
            this.worksheet = worksheet;
            List = value;
            this.propertyName = propertyName;
            this.settings = settings;
            rowsInserted = false;
        }

        private IList List { get; }

        private Type ListItemType => List.GetType().GetGenericArguments()[0];

        private Range TopCellMergeRange => topCell.GetMergedRange();

        public void PutTable()
        {
            foreach (var itemProperty in ListItemType.GetProperties())
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

                    PutColumn(itemProperty);
                }
            }
        }

        private void RemoveTemplateRow()
        {
            worksheet.Cells.DeleteRow(topCell.Row);
        }

        private void PutColumn(PropertyInfo itemProperty)
        {
            for (int i = 0; i < List.Count; i++)
            {
                var curCell = worksheet.Cells[topCell.Row + i, topCell.Column];
                var curCellValue = itemProperty.GetValue(List[i], null);

                MergeColumnCells(curCell);

                CellValueHelper.PutValue(curCell, curCellValue, settings);
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