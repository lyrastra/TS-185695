using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsReader))]

    class PaymentToNaturalPersonsReader : IPaymentToNaturalPersonsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderReader paymentOrderReader;
        private readonly IWorkerPaymentDao workerPaymentDao;

        public PaymentToNaturalPersonsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderReader paymentOrderReader,
            IWorkerPaymentDao workerPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderReader = paymentOrderReader;
            this.workerPaymentDao = workerPaymentDao;
        }

        public async Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await paymentOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderOutgoingPaymentToNaturalPersons).ConfigureAwait(false);
            response.ChargePayments =
                response.PaymentOrder.OperationState == OperationState.MissingWorker
                    ? ChargePaymentsForMissingEmployee(response.PaymentOrder.Sum)
                    : await workerPaymentDao.GetByBaseIdAsync((int)context.FirmId, documentBaseId);

            return response;
        }

        public async Task<IReadOnlyCollection<PaymentOrderResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var paymentOrderResponses = await paymentOrderReader.GetByBaseIdsAsync(documentBaseIds, OperationType.PaymentOrderOutgoingPaymentToNaturalPersons);

            var baseIds = paymentOrderResponses
                .Where(x => x.PaymentOrder.OperationState != OperationState.MissingWorker)
                .Select(x => x.PaymentOrder.DocumentBaseId)
                .ToArray();
            var chargePaymentsMap = await workerPaymentDao.GetByBaseIdsAsync((int)context.FirmId, baseIds);

            foreach (var paymentOrderResponse in paymentOrderResponses)
            {
                paymentOrderResponse.ChargePayments = paymentOrderResponse.PaymentOrder.OperationState == OperationState.MissingWorker
                    ? ChargePaymentsForMissingEmployee(paymentOrderResponse.PaymentOrder.Sum)
                    : chargePaymentsMap.GetValueOrDefault(paymentOrderResponse.PaymentOrder.DocumentBaseId) ?? Array.Empty<WorkerPayment>();
            }

            return paymentOrderResponses;
        }

        private static IReadOnlyCollection<WorkerPayment> ChargePaymentsForMissingEmployee(decimal paymentOrderSum)
        {
            return new[]
            {
                new WorkerPayment
                {
                    Sum = paymentOrderSum
                }
            };
        }
    }
}
