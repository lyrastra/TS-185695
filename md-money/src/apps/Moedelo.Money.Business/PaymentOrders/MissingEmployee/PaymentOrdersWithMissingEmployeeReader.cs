using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.MissingEmployee;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.MissingEmployee;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.MissingEmployee
{
    [InjectAsSingleton(typeof(IPaymentOrdersWithMissingEmployeeReader))]
    internal class PaymentOrdersWithMissingEmployeeReader : IPaymentOrdersWithMissingEmployeeReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IEmployeesApiClient employeesApiClient;
        private readonly IWorkerApiClient workerApiClient;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;

        public PaymentOrdersWithMissingEmployeeReader(IExecutionInfoContextAccessor contextAccessor,
            IEmployeesApiClient employeesApiClient,
            IWorkerApiClient workerApiClient,
            IPaymentOrderApiClient paymentOrderApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.employeesApiClient = employeesApiClient;
            this.workerApiClient = workerApiClient;
            this.paymentOrderApiClient = paymentOrderApiClient;
        }

        public async Task<List<MissingEmployeePayment>> GetByEmployeeIdAsync(int workerId)
        {
            var unrecognizedByWorkerQuery = await GetUnrecognizedByWorkerQuery(workerId);

            if (unrecognizedByWorkerQuery == null)
            {
                return new List<MissingEmployeePayment>();
            }

            if (await IsDuplicateWorker(unrecognizedByWorkerQuery))
            {
                return new List<MissingEmployeePayment>();
            }

            var paymentOrders = await paymentOrderApiClient.GetPaymentOrdersWithMissingEmployee();
            paymentOrders = paymentOrders
                .Where(x =>
                    (!string.IsNullOrEmpty(x.WorkerInn) && x.WorkerInn == unrecognizedByWorkerQuery.Inn)
                    || (!string.IsNullOrEmpty(x.WorkerName) && x.WorkerName == unrecognizedByWorkerQuery.Name)
                    ||  (!string.IsNullOrEmpty(x.WorkerSettlementAccount) && x.WorkerSettlementAccount == unrecognizedByWorkerQuery.Account))
                .ToArray();

            return paymentOrders.Select(paymentOrder => new MissingEmployeePayment
            {
                DocumentBaseId = paymentOrder.DocumentBaseId,
                OperationType = paymentOrder.OperationType
            }).ToList();
        }

        private async Task<bool> IsDuplicateWorker(UnrecognizedByWorkerQuery unrecognizedByWorkerQuery)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var workerList = await workerApiClient.GetForFinControlAsync(context.FirmId, context.UserId);

            var filteredWorkers = workerList.Where(item =>
                (!string.IsNullOrEmpty(unrecognizedByWorkerQuery.Inn) && item.Inn == unrecognizedByWorkerQuery.Inn)
                || GetFullName(item.Surname, item.Name, item.Patronymic) == unrecognizedByWorkerQuery.Name).ToList();

            if (filteredWorkers.Count > 1)
            {
                return true;
            }

            var filteredByAccount = await employeesApiClient.GetWorkersCardAccountAsync(
                context.FirmId, context.UserId, workerList.Select(item => item.Id));

            var accounts = filteredByAccount.Where(item => item.Number == unrecognizedByWorkerQuery.Account).ToList();

            if (accounts.Count > 1)
            {
                return true;
            }

            return false;
        }

        private async Task<UnrecognizedByWorkerQuery> GetUnrecognizedByWorkerQuery(int workerId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var worker = await employeesApiClient.GetByIdAsync(context.FirmId, context.UserId, workerId);
            var workerAccount =
                await employeesApiClient.GetWorkerCardAccountAsync(context.FirmId, context.UserId, workerId);

            if (worker == null || workerAccount == null)
            {
                return null;
            }

            var unrecognizedByWorkerQuery = new UnrecognizedByWorkerQuery
            {
                Inn = worker.Inn,
                Name = GetFullName(worker.Surname, worker.Name, worker.Patronymic),
                Account = workerAccount?.Number
            };

            return unrecognizedByWorkerQuery;
        }

        private static string GetFullName(string surname, string name, string patronymic) =>
            $"{surname} {name} {patronymic}";

        private class UnrecognizedByWorkerQuery
        {
            public string Name { get; set; }
            public string Inn { get; set; }
            public string Account { get; set; }
        }
    }
}