using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Generators;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Generators;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots
{
    [InjectAsSingleton(typeof(PaymentSnapshotBuilder))]
    internal class PaymentSnapshotBuilder
    {
        private readonly ILogger<PaymentSnapshotBuilder> logger;
        private readonly WorkerPaymentSnapshotGenerator workerPaymentSnapshotGenerator;
        private readonly MovementPaymentSnapshotGenerator movementPaymentSnapshotGenerator;
        private readonly BudgetaryPaymentSnapshotGenerator budgetaryPaymentSnapshotGenerator;
        private readonly DefaultPaymentSnapshotGenerator defaultPaymentSnapshotGenerator;
        private readonly DeductionPaymentSnapshotGenerator deductionPaymentSnapshotGenerator;

        public PaymentSnapshotBuilder(
            ILogger<PaymentSnapshotBuilder> logger,
            WorkerPaymentSnapshotGenerator workerPaymentSnapshotGenerator,
            MovementPaymentSnapshotGenerator movementPaymentSnapshotGenerator,
            BudgetaryPaymentSnapshotGenerator budgetaryPaymentSnapshotGenerator,
            DefaultPaymentSnapshotGenerator defaultPaymentSnapshotGenerator,
            DeductionPaymentSnapshotGenerator deductionPaymentSnapshotGenerator)
        {
            this.logger = logger;
            this.defaultPaymentSnapshotGenerator = defaultPaymentSnapshotGenerator;
            this.deductionPaymentSnapshotGenerator = deductionPaymentSnapshotGenerator;
            this.workerPaymentSnapshotGenerator = workerPaymentSnapshotGenerator;
            this.movementPaymentSnapshotGenerator = movementPaymentSnapshotGenerator;
            this.budgetaryPaymentSnapshotGenerator = budgetaryPaymentSnapshotGenerator;
        }

        public async Task<string> BuildAsync(PaymentOrderSaveRequest request)
        {
            PaymentOrderSnapshot snapshot;
            if (request.PaymentOrder.OperationType == OperationType.PaymentOrderOutgoingDeduction)
            {
                snapshot = await deductionPaymentSnapshotGenerator.GenerateAsync(request);
            }
            else if (request.PaymentOrder.OperationType.IsWorkerOperation())
            {
                snapshot = await workerPaymentSnapshotGenerator.GenerateAsync(request);
            }
            else if (request.PaymentOrder.OperationType.IsOperationBetweenSettlementAccounts())
            {
                snapshot = await movementPaymentSnapshotGenerator.GenerateAsync(request);
            }
            else if (request.PaymentOrder.OperationType.IsBudgetaryOperation())
            {
                snapshot = await budgetaryPaymentSnapshotGenerator.GenerateAsync(request);
            }
            else
            {
                snapshot = await defaultPaymentSnapshotGenerator.GenerateAsync(request);
            }

            try
            {
                return XmlHelper.Serialize(snapshot);
            }
            catch
            {
                logger.LogInformation($"Serialization error. snapshot : {snapshot.ToJsonString()}");
                throw;
            }
        }
    }
}
