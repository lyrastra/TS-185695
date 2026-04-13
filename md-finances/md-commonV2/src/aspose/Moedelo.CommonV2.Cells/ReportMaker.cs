using Aspose.Cells;
using Aspose.Cells.Drawing;
using Moedelo.Common.Enums.Enums;
using Moedelo.CommonV2.Cells.Business;
using Moedelo.CommonV2.Cells.Enums;
using Moedelo.CommonV2.Cells.Extensions;
using Moedelo.CommonV2.Cells.Helpers;
using Moedelo.CommonV2.Cells.Models.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Moedelo.CommonV2.Cells
{
    public class ReportMaker
    {
        private const string TemplatePagePrefix = "Template#";

        private readonly byte[] template;
        private readonly object model;
        private readonly ReportSettings setting;
        private readonly DocumentFormat format;
        private Workbook workbook;
        private Worksheet currentWorksheet;
        private readonly Action<Picture, string> imageFieldProcessor;

        /// <summary>
        /// Заполняет шаблон Excel-книги в соответствии с моделью и выдает в виде массива байт
        /// </summary>
        /// <param name="template">Шаблон исходного Excel-книги в виде массива байтов</param>
        /// <param name="model">Модель</param>
        /// <param name="settings">Настройки формата ячеек</param>
        /// <param name="format">Формат, в которой будет сохранена электронная книга</param>
        /// <param name="groupSettings">Групповые настройки</param>
        /// <param name="reportColorSettings">Настройки цветов</param>
        /// <returns></returns>
        public static byte[] GetReport(
            byte[] template,
            object model,
            ReportSettings settings = null,
            DocumentFormat format = DocumentFormat.Xlsx,
            ReportGroupSettings groupSettings = null,
            ReportColorSettings reportColorSettings = null,
            ReportAutoHeightSettings reportAutoHeightSettings = null)
        {
            AsposeCellLicenseHelper.SetLicense();
            var report = new ReportMaker(template, model, settings ?? ReportSettings.Default, format);
            report.Build();
            report.GroupBySettings(groupSettings ?? new ReportGroupSettings());
            report.ColorBySettings(reportColorSettings ?? new ReportColorSettings());
            report.ColorBySettings(reportColorSettings ?? new ReportColorSettings());
            report.SetAutoHeightBySettings(reportAutoHeightSettings);
            return report.GetResult();
        }

        private void SetAutoHeightBySettings(ReportAutoHeightSettings reportAutoHeightSettings)
        {
            if (reportAutoHeightSettings is null)
                return;

            workbook.Worksheets[0].AutoFitRows(reportAutoHeightSettings.StartRowIndex, reportAutoHeightSettings.EndRowIndex);
        }

        private void ColorBySettings(ReportColorSettings reportColorSettings)
        {
            if (workbook.Worksheets.Count == 0)
            {
                return;
            }
            var worksheet = workbook.Worksheets[0];
            foreach (var color in reportColorSettings.Colors)
            {
                if (color.Value.Rows.Count == 0)
                {
                    continue;
                }
                foreach (var row in color.Value.Rows)
                {
                    for (var i = color.Value.FirstColumnIndex; i <= color.Value.LastColumnIndex; i++)
                    {
                        var style = worksheet.Cells[row, i].GetStyle();
                        style.ForegroundColor = color.Key;
                        worksheet.Cells[row, i].SetStyle(style);
                    }
                }
            }
        }

        public ReportMaker(byte[] template, object model, ReportSettings setting, DocumentFormat format)
        {
            this.template = template;
            this.model = model;
            this.setting = setting;
            this.format = format;
        }

        public ReportMaker(Workbook workbook, object model, ReportSettings settings)
        {
            this.workbook = workbook;
            this.model = model;
            setting = settings;
        }

        public ReportMaker(Workbook workbook, object model, Action<Picture, string> imageFieldProcessor, ReportSettings settings)
        {
            this.workbook = workbook;
            this.model = model;
            setting = settings;
            this.imageFieldProcessor = imageFieldProcessor;
        }

        public void Build()
        {
            LoadTemplate();

            Put(model, null);

            CleanUnusedTemplateCells();

            UpdateRangesBySettings();

            CleanTemplateWorksheets();
        }

        private void GroupBySettings(ReportGroupSettings groupSettings)
        {
            if (workbook.Worksheets.Count == 0)
            {
                return;
            }
            var cells = workbook.Worksheets[0].Cells;
            foreach (var reportGroup in groupSettings.Groups)
            {
                cells.GroupRows(reportGroup.FirstIndex, reportGroup.LastIndex, reportGroup.IsHidden);
            }
        }

        private void CleanTemplateWorksheets()
        {
            var names = workbook.Worksheets
                .Select(worksheet => worksheet.Name)
                .Where(name => name.StartsWith(TemplatePagePrefix))
                .ToList();

            foreach (var name in names)
            {
                workbook.Worksheets.RemoveAt(name);
            }
        }

        private void CleanUnusedTemplateCells()
        {
            foreach (var cell in FindCells("^%.*%$", true))
            {
                cell.PutValue(string.Empty);
            }
        }

        // нехорошо засорять warning-ами чужие проекты
#pragma warning disable CS0618

        private void UpdateRangesBySettings()
        {
            if (setting == null)
            {
                return;
            }

            foreach (RangeSettings rangeSettings in setting.RangesSettings.Where(r => r.Hide))
            {
                Range range = workbook.Worksheets.GetRangeByName(rangeSettings.Name);
                if (range != null)
                {
                    switch (rangeSettings.DeletionType)
                    {
                        case DeletionType.Range:
                            range.Worksheet.Cells.DeleteRange(range.FirstRow, range.FirstColumn,
                                range.FirstRow + range.RowCount - 1, range.FirstColumn + range.ColumnCount, ShiftType.Left);
                            break;

                        case DeletionType.Row:
                            range.Worksheet.Cells.DeleteRows(range.FirstRow, range.RowCount);
                            break;

                        case DeletionType.Column:
                            range.Worksheet.Cells.DeleteColumns(range.FirstColumn, range.ColumnCount, true);
                            break;
                    }
                }
            }

            foreach (RangeSettings rangeSettings in setting.RangesSettings.Where(r => r.IsAutoFit))
            {
                Range range = workbook.Worksheets.GetRangeByName(rangeSettings.Name);
                if (range == null)
                {
                    continue;
                }

                for (int rowIndex = range.FirstRow; rowIndex < range.FirstRow + range.RowCount; rowIndex++)
                {
                    range.Worksheet.AutoFitRow(rowIndex, 0, byte.MaxValue, new AutoFitterOptions { AutoFitMergedCells = true });

                    Row row = range.Worksheet.Cells.Rows[rowIndex];
                    if (row.Height < rangeSettings.MinHeight)
                    {
                        row.Height = rangeSettings.MinHeight;
                    }
                }
            }
        }

#pragma warning restore CS0618

        private void Put(object value, string propertyName)
        {
            if (IsValueType(value))
            {
                PutValue(value, propertyName);
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

        private void PutList(IList value, string propertyName)
        {
            PutListItemsByIndex(value, propertyName);
            var writer = new TableWriter(workbook.Worksheets[0], value, propertyName, setting);
            writer.PutTable();

            if (value.Count > 0 && value[0].GetType().GetProperty("PageNumber") != null)
            {
                PutInPage(value, propertyName);
            }
        }

        private void PutInPage(IEnumerable value, string propertyName)
        {
            foreach (var val in value)
            {
                var type = val.GetType();

                var templateIndex = (int)type.GetProperty("TemplateIndex").GetValue(val);
                int templateWorksheetIndex = -1;

                for (var w = 0; w < workbook.Worksheets.Count; w++)
                {
                    if (workbook.Worksheets[w].Name == TemplatePagePrefix + templateIndex)
                    {
                        templateWorksheetIndex = workbook.Worksheets[w].Index;
                        break;
                    }
                }

                if (templateWorksheetIndex < 0)
                {
                    continue;
                }

                var worksheetIndex = workbook.Worksheets.AddCopy(templateWorksheetIndex);

                currentWorksheet = workbook.Worksheets[worksheetIndex];
                currentWorksheet.Name = type.GetProperty("PageName").GetValue(val).ToString();
                PutObject(val, propertyName);
                currentWorksheet = null;
            }
        }

        private void PutListItemsByIndex(IList list, string propertyName)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Put(list[i], $"{propertyName}[{i}]");
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

        private void PutValue(object value, string propertyName)
        {
            PutValueInSingleCell(value, propertyName);
            PutValueInMultipleCells(value, propertyName);
            PutValueInRange(value, propertyName);
        }

        private void PutValueInSingleCell(object value, string propertyName)
        {
            foreach (var cell in FindCells($"%{propertyName}%"))
            {
                if (value is byte[] bytes)
                {
                    using (var imageStream = new MemoryStream(bytes))
                    {
                        var index = workbook.Worksheets[0].Pictures.Add(cell.Row, cell.Column, imageStream);
                        var picture = workbook.Worksheets[0].Pictures[index];

                        imageFieldProcessor?.Invoke(picture, propertyName);
                    }
                }
                else
                {
                    CellValueHelper.PutValue(cell, value, setting);
                }
            }
        }

        private void PutValueInMultipleCells(object value, string propertyName)
        {
            if (value == null)
            {
                return;
            }

            var i = 1;
            var strValue = value.ToString();

            while (true)
            {
                var stop = true;

                foreach (var cell in FindCells($"%{propertyName}#{i}%"))
                {
                    stop = false;

                    var currentChar = i <= strValue.Length
                        ? strValue[i - 1].ToString()
                        : string.Empty;
                    CellValueHelper.PutValue(cell, currentChar, setting);
                }

                if (stop)
                {
                    break;
                }

                i += 1;
            }
        }

        private void PutValueInRange(object value, string propertyName)
        {
            var ranges = GetRangesForProperty(propertyName);
            if (!ranges.Any())
            {
                return;
            }

            string preparedValue = value.ToString();

            int valueCharIndex = 0;
            string lastMergeRange = string.Empty;
            foreach (Range range in ranges)
            {
                foreach (Cell cell in range)
                {
                    Range merge = cell.GetMergedRange();
                    if (merge.RefersTo != lastMergeRange)
                    {
                        lastMergeRange = merge.RefersTo;
                        if (valueCharIndex < preparedValue.Length)
                        {
                            merge.PutValue(preparedValue[valueCharIndex].ToString(), false, false);
                            valueCharIndex++;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        private List<Range> GetRangesForProperty(string propertyName)
        {
            var ranges = new List<Range>();

            Range range = workbook.Worksheets.GetRangeByName(propertyName);
            if (range != null)
            {
                ranges.Add(range);
            }
            else
            {
                int rangeIndex = 1;
                while (true)
                {
                    Range rangeIndexed = workbook.Worksheets.GetRangeByName(propertyName + "." + rangeIndex);
                    if (rangeIndexed != null)
                    {
                        ranges.Add(rangeIndexed);
                        rangeIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return ranges;
        }

        private IEnumerable<Cell> FindCells(string cellValue, bool useRegex = false)
        {
            Cell cell = null;

            foreach (Worksheet worksheet in workbook.Worksheets)
            {
                if (currentWorksheet != null && currentWorksheet != worksheet)
                {
                    continue;
                }

                while (true)
                {
                    cell = worksheet.Cells.Find(cellValue, cell, new FindOptions { RegexKey = useRegex, LookAtType = LookAtType.EntireContent });

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

        private void LoadTemplate()
        {
            if (workbook != null)
            {
                return;
            }

            using (var stream = new MemoryStream(template))
            {
                workbook = new Workbook(stream);
            }
        }

        public byte[] GetResult()
        {
            using (var stream = new MemoryStream())
            {
                workbook.Save(stream, format.ToSaveFormat());

                return stream.ToArray();
            }
        }

        public void SetPagesVisibility(Dictionary<int, bool> pagesVisibility)
        {
            var names = new List<string>();
            for (var i = 0; i < workbook.Worksheets.Count; i++)
            {
                var name = workbook.Worksheets[i].Name;
                if (pagesVisibility.ContainsKey(i) && !pagesVisibility[i])
                {
                    names.Add(name);
                }
            }

            foreach (var name in names)
            {
                workbook.Worksheets.RemoveAt(name);
            }
        }
    }
}