using System.Collections;
using System.Linq;
using Aspose.Cells;
using Moedelo.CommonV2.Cells.Models.Settings;

namespace Moedelo.CommonV2.Cells.Extensions
{
    /// <summary>
    /// Вспомогательные класс для работы с наборром ячеек Aspose.Cells
    /// </summary>
    public static class CellsExtension
    {
        /// <summary>
        /// Вставить простой тип данных в набор ячеек
        /// </summary>
        /// <param name="cells">Набор ячеек</param>
        /// <param name="propertyName">Название аттрибута</param>
        /// <param name="propertyValue">Значение аттрибута</param>
        public static void InsertSimple(this Aspose.Cells.Cells cells, string propertyName, object propertyValue)
        {
            Cell fundedCell = null;

            while (true)
            {
                fundedCell = cells.Find($"%{propertyName}%", fundedCell, new FindOptions { RegexKey = false });
                if (fundedCell == null)
                {
                    break;
                }

                fundedCell.PutValue(propertyValue);
            }
        }

        /// <summary>
        /// Вставить простой список в набор ячеек
        /// </summary>
        /// <param name="cells">Набор ячеек</param>
        /// <param name="propertyName">Название аттрибута</param>
        /// <param name="propertyValue">Значение аттрибута</param>
        public static void InsertSimpleList(this Aspose.Cells.Cells cells, string propertyName, object propertyValue)
        {
            var valueAsList = ((IEnumerable) propertyValue).Cast<object>().ToList();

            for (var i = 0; i < valueAsList.Count; i++)
            {
                InsertSimple(cells, $"{propertyName}[{i}]", valueAsList[i]);
            }
        }

        /// <summary>
        /// Вставить список объектов в набор ячеек
        /// </summary>
        /// <param name="cells">Набор ячеек</param>
        /// <param name="propertyName">Название аттрибута</param>
        /// <param name="propertyValue">Значение аттрибута</param>
        /// <param name="bigDataSettings">Настройки для импорта больших данных</param>
        public static void InsertObjectList(this Aspose.Cells.Cells cells, string propertyName, object propertyValue, BigDataSettings bigDataSettings)
        {
            var valueAsList = ((IEnumerable)propertyValue).Cast<object>().ToArray();

            var first = valueAsList.FirstOrDefault();
            if (first == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(bigDataSettings?.CollectionName) && propertyName == bigDataSettings.CollectionName &&
                bigDataSettings.StartRow >= 0 && bigDataSettings.StartColumn >= 0)
            {
                cells.ImportCustomObjects(valueAsList, bigDataSettings.StartRow, bigDataSettings.StartColumn,
                    new ImportTableOptions
                    {
                        InsertRows = true,
                        IsFieldNameShown = false
                    });
                cells.HideRow(bigDataSettings.StartRow + valueAsList.Length);
            }
            else
            {
                foreach (var propertyInfo in first.GetType().GetProperties())
                {
                    var fundedCell = cells.Find($"%{propertyName}.{propertyInfo.Name}%", null, new FindOptions {RegexKey = false});
                    if (fundedCell == null)
                    {
                        continue;
                    }

                    var collection = valueAsList.Select(t => new {Value = propertyInfo.GetValue(t)}).ToList();

                    var area = new CellArea
                    {
                        StartRow = fundedCell.Row + 1,
                        EndRow = fundedCell.Row + collection.Count,
                        StartColumn = fundedCell.Column,
                        EndColumn = fundedCell.Column
                    };

                    cells.MoveRange(area, fundedCell.Row + collection.Count, fundedCell.Column);
                    cells.ImportCustomObjects(collection, fundedCell.Row, fundedCell.Column, new ImportTableOptions
                        {
                            InsertRows = false,
                            IsFieldNameShown = false
                        });
                    cells.CreateRange(fundedCell.Row, fundedCell.Column, collection.Count, 1)
                        .ApplyStyle(fundedCell.GetStyle(), new StyleFlag {All = true});
                }
            }
        }

        /// <summary>
        /// Вставить пустое значение в неиспользуемые ячейки с шаблонами
        /// </summary>
        /// <param name="cells"></param>
        public static void InsertEmptyToUnusedCells(this Aspose.Cells.Cells cells)
        {
            Cell fundedCell = null;

            var findOptionsWithUseRegex = new FindOptions
            {
                RegexKey = true,
                LookAtType = LookAtType.EntireContent
            };

            while (true)
            {
                fundedCell = cells.Find("%.*?%", fundedCell, findOptionsWithUseRegex);
                if (fundedCell == null)
                {
                    break;
                }

                fundedCell.PutValue(string.Empty);
            }
        }
    }
}