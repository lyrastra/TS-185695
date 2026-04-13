using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Banks;
using Moedelo.Money.PaymentOrders.Domain.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites
{
    [InjectAsSingleton(typeof(SalaryProjectOrderDetailsGetter))]
    internal class SalaryProjectOrderDetailsGetter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBankReader bankReader;
        private readonly ISalaryProjectApiClient salaryProjectApiClient;

        public SalaryProjectOrderDetailsGetter(
            IExecutionInfoContextAccessor contextAccessor,
            IBankReader bankReader,
            ISalaryProjectApiClient salaryProjectApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.bankReader = bankReader;
            this.salaryProjectApiClient = salaryProjectApiClient;
        }

        public async Task<OrderDetails> GetAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var salaryProject = await salaryProjectApiClient.GetSalaryProject(context.FirmId, context.UserId).ConfigureAwait(false) ??
                                await salaryProjectApiClient.GetSalaryProjectByDocumentBaseId(context.FirmId, context.UserId, documentBaseId).ConfigureAwait(false);

            if (salaryProject == null)
            {
                throw new Exception($"Not found salary project by firm with Id = {context.FirmId}");
            }

            var salaryBank = salaryProject.BankId.HasValue
                ? await bankReader.GetByIdAsync(salaryProject.BankId.Value).ConfigureAwait(false)
                : null;

            return new OrderDetails
            {
                IsOoo = false,
                Name = salaryBank?.FullName ?? string.Empty,
                Inn = salaryProject.BankInn,
                Kpp = salaryProject.BankKpp,
                SettlementNumber = salaryProject.SettlementAccountNumber,
                BankName = salaryBank?.FullName ?? string.Empty,
                BankBik = salaryBank?.Bik ?? string.Empty,
                BankCorrespondentAccount = salaryBank?.CorrespondentAccount ?? string.Empty,
                BankCity = salaryBank?.City ?? string.Empty,
                Okato = string.Empty,
                KontragentType = KontragentTypes.Worker
            };
        }
    }
}