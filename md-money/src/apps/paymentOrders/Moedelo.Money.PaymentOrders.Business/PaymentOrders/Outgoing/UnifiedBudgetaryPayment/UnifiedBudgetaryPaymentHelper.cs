using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Kbks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetaryPaymentHelper))]
    internal class UnifiedBudgetaryPaymentHelper
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly KbkReader kbkReader;
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;

        public UnifiedBudgetaryPaymentHelper(
            IExecutionInfoContextAccessor contextAccessor,
            KbkReader kbkReader,
            IFirmRequisitesApiClient firmRequisitesApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.kbkReader = kbkReader;
            this.firmRequisitesApiClient = firmRequisitesApiClient;
        }

        public async Task FillRequestAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var kbkTask = kbkReader.GetByAccountCodeAsync((int)BudgetaryAccountCodes.UnifiedBudgetaryPayment);
            var firmRequisitesTask = firmRequisitesApiClient.GetRegistrationDataAsync(context.FirmId, context.UserId);

            await Task.WhenAll(kbkTask, firmRequisitesTask);

            var kbk = kbkTask.Result.Single();
            var IsOoo = firmRequisitesTask.Result.IsOoo;

            request.PaymentOrder.BudgetaryTaxesAndFees = kbk.AccountCode;
            request.PaymentOrder.KbkId = kbk.Id;
            request.PaymentOrder.KbkPaymentType = kbk.KbkPaymentType;
            request.PaymentOrder.BudgetaryPeriodType = BudgetaryPeriodType.NoPeriod;

            request.BudgetaryFields = new Domain.Models.Snapshot.BudgetaryFields
            {
                Period = new Domain.Models.Snapshot.BudgetaryPeriod { Type = request.PaymentOrder.BudgetaryPeriodType.Value },
                DocDate = "0",
                PaymentType = BudgetaryPaymentType.TaxPayment,
                CodeUin = request.BudgetaryFields.CodeUin,
                PayerKpp = "0",
                DocNumber = kbk.DocNumber,
                PaymentBase = kbk.PaymentBase,
                Kbk = kbk.Number,
                PayerStatus = BudgetaryPayerStatus.Company
            };
        }
    }
}