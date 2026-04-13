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
    [InjectAsSingleton(typeof(WorkerOrderDetailsGetter))]
    internal class WorkerOrderDetailsGetter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IEmployeesApiClient employeesApiClient;
        private readonly IBankReader bankReader;

        public WorkerOrderDetailsGetter(
            IExecutionInfoContextAccessor contextAccessor,
            IEmployeesApiClient employeesApiClient,
            IBankReader bankReader)
        {
            this.contextAccessor = contextAccessor;
            this.employeesApiClient = employeesApiClient;
            this.bankReader = bankReader;
        }

        /// <inheritdoc />
        public async Task<OrderDetails> GetAsync(int workerId)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var worker = await employeesApiClient.GetByIdAsync(context.FirmId, context.UserId, workerId).ConfigureAwait(false);
            if (worker == null)
            {
                throw new Exception($"Not found worker by Id = {workerId}");
            }

            var workerCardAccount = await employeesApiClient.GetWorkerCardAccountAsync(context.FirmId, context.UserId, workerId).ConfigureAwait(false);

            var bank = workerCardAccount.BankId.HasValue
                ? await bankReader.GetByIdAsync(workerCardAccount.BankId.Value).ConfigureAwait(false)
                : null;

            return new OrderDetails
            {
                IsOoo = false,
                Name = worker.FullName,
                Inn = GetWorkerInn(workerCardAccount.InnRecipient, worker.Inn),
                Kpp = string.Empty,
                SettlementNumber = workerCardAccount.Number ?? string.Empty,
                BankName = bank?.FullNameWithCity ?? string.Empty,
                BankBik = bank?.Bik ?? string.Empty,
                BankCorrespondentAccount = bank?.CorrespondentAccount ?? string.Empty,
                BankCity = bank?.City ?? string.Empty,
                Okato = string.Empty,
                KontragentType = KontragentTypes.Worker
            };
        }

        private static string GetWorkerInn(string workerCardAccountInn, string workerInn)
        {
            return string.IsNullOrEmpty(workerCardAccountInn)
                ? workerInn
                : workerCardAccountInn;
        }
    }
}