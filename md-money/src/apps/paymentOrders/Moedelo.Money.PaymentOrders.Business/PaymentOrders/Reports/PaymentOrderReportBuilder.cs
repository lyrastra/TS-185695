using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Extensions;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports
{
    [InjectAsSingleton(typeof(IPaymentOrderReportBuilder))]
    internal class PaymentOrderReportBuilder : IPaymentOrderReportBuilder
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPaymentOrderDao paymentOrderDao;
        private readonly IReadOnlyDictionary<ReportFormat, IPaymentOrderFormatReportBuilder> reportBuilders;

        public PaymentOrderReportBuilder(
            IExecutionInfoContextAccessor executionInfoContext,
            IPaymentOrderDao paymentOrderDao,
            IEnumerable<IPaymentOrderFormatReportBuilder> reportBuilders)
        {
            this.executionInfoContext = executionInfoContext;
            this.paymentOrderDao = paymentOrderDao;
            this.reportBuilders = reportBuilders.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<ReportFormatAttribute>().Format,
                     Builder = x
                 })
                .ToDictionary(x => x.Format, x => x.Builder);
        }

        public async Task<ReportFile> BuildAsync(long documentBaseId, ReportFormat format)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await paymentOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            paymentOrder.CheckExistence(documentBaseId);
            var snapshot = paymentOrder.ToSnapshot();

            var reportBuider = GetReportBuilder(format);
            return reportBuider.Render(paymentOrder, snapshot);
        }

        private IPaymentOrderFormatReportBuilder GetReportBuilder(ReportFormat format)
        {
            if (reportBuilders.TryGetValue(format, out var builder))
            {
                return builder;
            }
            throw new NotImplementedException($"Implementation of IPaymentOrderFormatReportBuilder for format {format} ({(int)format}) is not found");
        }
    }
}