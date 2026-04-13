using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Reflection;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats
{
    [ReportFormat(ReportFormat.Excel)]
    [InjectAsSingleton(typeof(IPaymentOrderFormatReportBuilder))]
    internal class PaymentOrderXlsReportBuilder : IPaymentOrderFormatReportBuilder
    {
        public ReportFile Render(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            var workbook = WorkbookFactory.Create(
                Assembly.GetAssembly(typeof(PaymentOrderPdfReportBuilder)),
                Constants.TemplatePath);

            var reportModel = XlsReportModelGenerator.Generate(paymentOrder, snapshot);
            workbook.ApplyModel(reportModel);

            var options = new AutoFitterOptions
            {
                AutoFitMergedCellsType = AutoFitMergedCellsType.EachLine
            };
            workbook.Worksheets[0].AutoFitRows(22,22,options);

            return new ReportFile
            {
                Content = workbook.ToBytes(SaveFormat.Xlsx),
                FileName = string.Format(Constants.FileNameFormat, paymentOrder.Number, "xlsx"),
                ContentType = "application/octet-stream"
            };
        }
    }
}