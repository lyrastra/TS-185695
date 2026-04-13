using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites;
using Moedelo.Money.PaymentOrders.Domain.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Generators
{
    [InjectAsSingleton(typeof(WorkerPaymentSnapshotGenerator))]
    internal class WorkerPaymentSnapshotGenerator
    {
        private readonly FirmOrderDetailsGetter firmOrderDetailsGetter;
        private readonly WorkerOrderDetailsGetter workerOrderDetailsGetter;
        private readonly SalaryProjectOrderDetailsGetter salaryProjectOrderDetailsGetter;
        private readonly IExecutionInfoContextAccessor userContextAccessor;
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;

        public WorkerPaymentSnapshotGenerator(
            FirmOrderDetailsGetter firmOrderDetailsGetter,
            WorkerOrderDetailsGetter workerOrderDetailsGetter,
            SalaryProjectOrderDetailsGetter salaryProjectOrderDetailsGetter,
            IExecutionInfoContextAccessor userContextAccessor,
            IFirmRequisitesApiClient firmRequisitesApiClient)
        {
            this.firmOrderDetailsGetter = firmOrderDetailsGetter;
            this.workerOrderDetailsGetter = workerOrderDetailsGetter;
            this.salaryProjectOrderDetailsGetter = salaryProjectOrderDetailsGetter;
            this.userContextAccessor = userContextAccessor;
            this.firmRequisitesApiClient = firmRequisitesApiClient;
        }

        public async Task<PaymentOrderSnapshot> GenerateAsync(PaymentOrderSaveRequest request)
        {
            var snapshot = new PaymentOrderSnapshot
            {
                PaymentNumber = request.PaymentOrder.Number,
                OrderDate = request.PaymentOrder.Date.Date,
                BankDocType = request.PaymentOrder.OrderType,
                Sum = request.PaymentOrder.Sum,
                Direction = request.PaymentOrder.Direction,
                Purpose = request.PaymentOrder.Description,
                PaymentPriority = request.PaymentOrder.UnderContract == PaymentToNaturalPersonsType.WorkContract
                    ? PaymentPriority.Third
                    : PaymentPriority.Fifth
            };

            // списания
            if (request.PaymentOrder.Direction == MoneyDirection.Outgoing)
            {
                snapshot.Payer = await firmOrderDetailsGetter.GetAsync(request.PaymentOrder.SettlementAccountId);

                // зарплатный проект
                if (request.PaymentOrder.UnderContract.IsSalaryProject())
                {
                    snapshot.Recipient = await salaryProjectOrderDetailsGetter.GetAsync(request.PaymentOrder.DocumentBaseId);
                    snapshot.PaymentPriority = PaymentPriority.Third;
                    return snapshot;
                }

                snapshot.Recipient = await GetWorkerRequisitesAsync(request);
                snapshot.Recipient.Name = request.PaymentOrder.KontragentName ?? snapshot.Recipient.Name;
                return snapshot;
            }

            // поступления
            snapshot.Payer = await GetWorkerRequisitesAsync(request);
            snapshot.Payer.Name = request.PaymentOrder.KontragentName ?? snapshot.Payer.Name;
            snapshot.Recipient = await firmOrderDetailsGetter.GetAsync(request.PaymentOrder.SettlementAccountId);
            return snapshot;
        }

        private async Task<OrderDetails> GetWorkerRequisitesAsync(PaymentOrderSaveRequest request)
        {
            var workerId = request.PaymentOrder.SalaryWorkerId;

            if (workerId == null)
            {
                workerId = request.ChargePayments?.FirstOrDefault()?.WorkerId;
            }

            if (workerId == -1)
            {
                var userContext = userContextAccessor.ExecutionInfoContext;
                var firmRequisites = await firmRequisitesApiClient.GetRegistrationDataAsync(userContext.FirmId, userContext.UserId).ConfigureAwait(false);

                // Специальный случай - ИП выбрал сам себя
                return new OrderDetails
                {
                    Name = request.PaymentOrder.KontragentName,
                    Inn = firmRequisites.Inn,
                    SettlementNumber = string.Empty,
                    IsOoo = false,
                    KontragentType = KontragentTypes.Worker
                };
            }

            return workerId.HasValue
                ? await workerOrderDetailsGetter.GetAsync(workerId.Value)
                : new OrderDetails
                {
                    Name = request.PaymentOrder.KontragentName,
                    Inn = request.KontragentRequisites?.Inn,
                    SettlementNumber = request.KontragentRequisites?.SettlementAccount,
                    IsOoo = false,
                    KontragentType = KontragentTypes.Worker
                };
        }
    }
}
