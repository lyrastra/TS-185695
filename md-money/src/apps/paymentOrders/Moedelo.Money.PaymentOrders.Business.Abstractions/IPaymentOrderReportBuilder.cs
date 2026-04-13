using Moedelo.Money.Common.Domain.Models.Reports;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrderReportBuilder
    {
        Task<ReportFile> BuildAsync(long documentBaseId, ReportFormat format);
    }
}