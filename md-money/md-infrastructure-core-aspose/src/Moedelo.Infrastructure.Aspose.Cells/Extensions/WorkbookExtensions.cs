using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Abstraction;
using Moedelo.Infrastructure.Aspose.Cells.Helpers;

namespace Moedelo.Infrastructure.Aspose.Cells.Extensions
{
    /// <summary>
    /// Функционал заимствован из commonV2 (переработан под .net core)
    /// source: https://github.com/moedelo/md-commonV2/blob/a5a3fb39a9bbc3249ea19c02ede36fc5eef4470c/src/aspose/Moedelo.CommonV2.Cells/AsposeCellsExtensions.cs
    /// </summary>
    public static class WorkbookExtensions
    {
        static WorkbookExtensions()
        {
            AsposeCellsActivator.ApplyLicense();
        }

        public static Workbook ApplyModel(this Workbook document, object model)
        {
            var builder = new ReportBuilder(document, model);

            return builder.Build();
        }

        public static Workbook ApplyModel(this Workbook document, object model, int worksheetIndex)
        {
            var builder = new ReportBuilder(document, model, worksheetIndex);

            return builder.Build();
        }

        public static byte[] ToBytes(this Workbook workbook)
        {
            using (var ms = new MemoryStream())
            {
                workbook.Save(ms, SaveFormat.Xlsx);
                return ms.ToArray();
            }
        }
        
        public static byte[] ToBytes(this Workbook workbook, SaveFormat saveFormat)
        {
            using (var ms = new MemoryStream())
            {
                workbook.Save(ms, saveFormat);
                return ms.ToArray();
            }
        }
        
        public static void MergeModel(this Workbook workbook, object model)
        {
            workbook.MergeFields(model);
        }
        
        public static void MergeListModel(this Workbook workbook, IEnumerable<IWorksheet> sheets)
        {
            workbook.MergeListFields(sheets.ToList());
        }
    }
}