using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.MimeTypes;
using Moedelo.Common.Enums.Extensions;
using Moedelo.Common.Enums.Extensions.Mime;
using Moedelo.CommonV2.Cells;
using Moedelo.Finances.Business.Resources;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Reports;

namespace Moedelo.Finances.Business.Services.Reports.PatentIncome
{
    internal static class PatentIncomeReportMaker
    {
        public static Report CreateReportFile(string organizationName, string patentName, int year,
            IReadOnlyCollection<MoneyOperation> operations)
        {
            var fileName = GetFileName(organizationName, patentName, year);
            var reportModel = CreateReportModel(operations, fileName);
            var report = CreateXlsReport(reportModel);

            return new Report
            {
                FileName = $"{fileName}.xls",
                MimeType = MimeFileTypes.Excel.ToMimeString(),
                Content = report
            };
        }

        private static ReportData CreateReportModel(IReadOnlyCollection<MoneyOperation> operations, string fileName)
        {
            return new ReportData
            {
                FileName = fileName,
                SumForYear =
                    FormatTotalSum(operations.Sum(x => x.RubSum)),
                Items = operations.Select(MapItem)
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.DocumentId)
                    .Select((x, i) =>
                    {
                        x.Number = i + 1;
                        return x;
                    }).ToList()
            };
        }

        private static ReportItemData MapItem(MoneyOperation operation)
        {
            return new ReportItemData
            {
                Date = operation.Date,
                DocumentId = operation.DocumentBaseId,
                DateAndNumberOfDocument = $"{operation.Date.ToShortDateString()}   №{operation.Number}",
                OperationType = operation.OperationType.GetDescription() ?? string.Empty,
                Destination = operation.Description,
                Sum = FormatItemSum(operation.Direction == MoneyDirection.Incoming ? operation.RubSum : 0)
            };
        }

        private static byte[] CreateXlsReport(ReportData reportModel)
        {
            var workbook = AsposeCellsExtensions.CreateFromTemplate(Templates.IncomePatentReportTemplate);
            workbook.ApplyModel(reportModel);
            return workbook.ToBytes(DocumentFormat.Xls);
        }

        private static string GetFileName(string organizationName, string patentName, int year)
        {
            var periodText = GetPeriodText(year);

            return $"{organizationName} патент {patentName} {periodText}";
        }

        private static string GetPeriodText(int year)
        {
            return $"за {year} год";
        }

        private static string FormatItemSum(decimal value)
        {
            return value.ToString("0.00;-0.00;''");
        }

        private static string FormatTotalSum(decimal value)
        {
            return value.ToString("0.00;-0.00;'0'");
        }

        private class ReportItemData
        {
            public long DocumentId { get; set; } // для сортировки
            public DateTime Date { get; set; } // для сортировки
            public int Number { get; set; }
            public string OperationType { get; set; }
            public string DateAndNumberOfDocument { get; set; }
            public string Destination { get; set; }
            public string Sum { get; set; }
        }

        private class ReportData
        {
            public string FileName { get; set; }
            public string SumForYear { get; set; }
            public List<ReportItemData> Items { get; set; }
        }
    }
}
