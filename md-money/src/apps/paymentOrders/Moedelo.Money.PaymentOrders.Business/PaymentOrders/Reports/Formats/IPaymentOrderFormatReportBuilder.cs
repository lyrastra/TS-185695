using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats
{
    [MultipleImplementationsPossible]
    internal interface IPaymentOrderFormatReportBuilder
    {
        ReportFile Render(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot);
    }
}
