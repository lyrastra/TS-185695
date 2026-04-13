using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport;
using Moedelo.Money.Reports.Business.Extensions;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport
{
    [InjectAsSingleton(typeof(IBankAndServiceBalanceReportPrintService))]
    internal class BankAndServiceBalanceReportPrintService : IBankAndServiceBalanceReportPrintService
    {
        private const string TemplatePath = "Moedelo.Money.Reports.Business.BankAndServiceBalanceReport.Templates.BankAndServiceBalanceReport.xls";
        private const string ReportContentType = "application/octet-stream";
        private const string DownloadFileName = "Выборка по расхождению остатков на конец {0} {1} года.xlsx";

        public BankAndServiceBalanceReportPrintService()
        {
        }

        public ReportFile GetReport(IReadOnlyCollection<BankAndServiceBalanceReportRow> reportRows, DateTime date)
        {
            var rowForPrint = PrintRowMapper.Map(reportRows);

            var workbook = WorkbookFactory.Create(GetType().Assembly, TemplatePath);

            FillReport(workbook, rowForPrint);

            return new ReportFile
            {
                Content = workbook.ToBytes(),
                FileName = string.Format(DownloadFileName, date.GetMonthGenitiveName(), date.Year),
                ContentType = ReportContentType,
            };
        }

        private static void FillReport(Workbook workbook, IReadOnlyCollection<BankAndServiceBalanceReportPrintRow> rows)
        {
            var worksheet = workbook.Worksheets[0];

            new TableWriter(worksheet, (IList) rows, "Items").PutTable(rawCellValue: true);
        }
    }
}
