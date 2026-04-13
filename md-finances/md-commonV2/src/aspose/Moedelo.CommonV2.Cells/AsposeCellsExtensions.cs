using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Moedelo.Common.Enums.Enums;
using Moedelo.CommonV2.Cells.Business;
using Moedelo.CommonV2.Cells.Enums;
using Moedelo.CommonV2.Cells.Extensions;
using Moedelo.CommonV2.Cells.Helpers;
using Moedelo.CommonV2.Cells.Models;
using Moedelo.CommonV2.Cells.Models.Mergefield;
using Moedelo.CommonV2.Cells.Models.Settings;

namespace Moedelo.CommonV2.Cells
{
    public static class AsposeCellsExtensions
    {
        static AsposeCellsExtensions()
        {
            AsposeCellLicenseHelper.SetLicense();
        }

        public static Workbook CreateFromTemplate(Assembly assembly, string templateName)
        {
            using (var template = GetTemplateStreamFromResource(assembly, templateName))
            {
                return CreateFromTemplate(template);
            }
        }

        public static Workbook CreateFromTemplate(byte[] template)
        {
            using (var stream = new MemoryStream(template))
            {
                return CreateFromTemplate(stream);
            }
        }

        public static Workbook CreateFromTemplate(Stream template)
        {
            var workbook = new Workbook(template);
            return workbook;
        }

        public static void ApplyModel(this Workbook workbook, object model, ReportSettings settings = null)
        {
            settings = settings ?? ReportSettings.Default;
            var reportMaker = new ReportMaker(workbook, model, settings);
            reportMaker.Build();
        }

        public static void ApplyModel(this Workbook workbook, object model, Action<Picture, string> imageFieldProcessor, ReportSettings settings = null)
        {
            settings = settings ?? ReportSettings.Default;
            var reportMaker = new ReportMaker(workbook, model, imageFieldProcessor, settings);
            reportMaker.Build();
        }

        public static void ApplyPagesVisibility(this Workbook workbook, PagesVisibilitySettings settings)
        {
            var names = new List<string>();
            for (var i = 0; i < workbook.Worksheets.Count; i++)
            {
                var name = workbook.Worksheets[i].Name;
                if (settings.ContainsKey(i) && !settings[i])
                {
                    names.Add(name);
                }
            }

            foreach (var name in names)
            {
                workbook.Worksheets.RemoveAt(name);
            }
        }

        public static void ApplyBigDataModel(this Workbook workbook, object model, BigDataSettings bigDataSettings = null)
        {
            foreach (var worksheet in workbook.Worksheets)
            {
                ApplyBigDataModel(worksheet, model, bigDataSettings);
            }
        }

        public static void ApplyBigDataModel(this Worksheet worksheet, object model, BigDataSettings bigDataSettings = null)
        {
            var insertData = model.GetType().GetProperties().Select(p => new InsertDataItem(p, model)).ToList();

            foreach (var insertDataItem in insertData.Where(i => i.DataType == InsertDataType.Simple))
            {
                worksheet.Cells.InsertSimple(insertDataItem.Name, insertDataItem.Value);
            }

            foreach (var insertDataItem in insertData.Where(i => i.DataType == InsertDataType.SimpleList))
            {
                worksheet.Cells.InsertSimpleList(insertDataItem.Name, insertDataItem.Value);
            }

            foreach (var insertDataItem in insertData.Where(i => i.DataType == InsertDataType.ObjectList))
            {
                worksheet.Cells.InsertObjectList(insertDataItem.Name, insertDataItem.Value, bigDataSettings);
            }

            worksheet.Cells.InsertEmptyToUnusedCells();
        }

        public static void MergeModel(this Workbook workbook, object model)
        {
            workbook.MergeFields(model);
        }

        public static void MergeListModel(this Workbook workbook, IEnumerable<IWorksheet> sheets)
        {
            workbook.MergeListFields(sheets.ToList());
        }

        public static byte[] ToBytes(this Workbook workbook, DocumentFormat format)
        {
            using (var stream = ToStream(workbook, format))
            {
                return stream.ToArray();
            }
        }

        public static MemoryStream ToStream(this Workbook workbook, DocumentFormat format)
        {
            var stream = new MemoryStream();
            workbook.Save(stream, format.ToSaveFormat());
            return stream;
        }

        private static Stream GetTemplateStreamFromResource(Assembly assembly, string templateName)
        {
            var stream = assembly.GetManifestResourceStream(templateName);

            if (stream == null)
            {
                throw new ArgumentException($"Error loading template '{templateName}'");
            }

            return stream;
        }
    }
}
