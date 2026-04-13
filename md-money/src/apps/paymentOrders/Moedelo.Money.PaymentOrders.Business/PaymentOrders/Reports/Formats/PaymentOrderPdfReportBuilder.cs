using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Abstraction;
using Moedelo.Infrastructure.Aspose.Cells.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Reflection;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats
{
    [ReportFormat(ReportFormat.Pdf)]
    [InjectAsSingleton(typeof(IPaymentOrderFormatReportBuilder))]
    internal class PaymentOrderPdfReportBuilder : IPaymentOrderFormatReportBuilder
    {
        public ReportFile Render(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            var workbook = WorkbookFactory.Create(
                Assembly.GetAssembly(typeof(PaymentOrderPdfReportBuilder)),
                Constants.TemplatePath);

            var reportModel = XlsReportModelGenerator.Generate(paymentOrder, snapshot);
            workbook.ApplyModel(reportModel);

            workbook.Worksheets[0].PageSetup.FitToPagesWide = 1;
            workbook.Worksheets[0].AutoFitRow(22);

            return new ReportFile
            {
                Content = workbook.ToBytes(Aspose.Cells.SaveFormat.Pdf),
                FileName = string.Format(Constants.FileNameFormat, paymentOrder.Number, "pdf"),
                ContentType = "application/pdf"
            };
        }
    }
}