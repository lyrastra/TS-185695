using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Extensions;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outsource
{
    [InjectAsSingleton(typeof(IOutsourceApproveService))]
    internal class OutsourceApproveService : IOutsourceApproveService
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IOutsourceApproveDao outsourceApproveDao;
        private readonly IPaymentOrderDao paymentOrderDao;

        public OutsourceApproveService(
            IExecutionInfoContextAccessor executionInfoContext,
            IOutsourceApproveDao outsourceApproveDao,
            IPaymentOrderDao paymentOrderDao)
        {
            this.executionInfoContext = executionInfoContext;
            this.outsourceApproveDao = outsourceApproveDao;
            this.paymentOrderDao = paymentOrderDao;
        }

        public async Task<bool> GetIsApprovedAsync(long documentBaseId, DateTime initialDate)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var paymentOrder = await paymentOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            paymentOrder.CheckExistence(documentBaseId);

            return await outsourceApproveDao.GetIsApprovedAsync(
                (int)context.FirmId, paymentOrder.DocumentBaseId, initialDate);
        }

        public Task<IReadOnlyList<IsApprovedResponse>> GetIsApprovedAsync(
            IReadOnlyCollection<long> documentBaseIds, DateTime initialDate)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return outsourceApproveDao.GetIsApprovedAsync((int)context.FirmId, documentBaseIds, initialDate);
        }

        public Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(AllOperationsApprovedRequest request, CancellationToken ct)
        {
            return outsourceApproveDao.GetIsAllOperationsApprovedAsync(request, ct);
        }

        public async Task SetIsApprovedAsync(long documentBaseId, bool isApproved, DateTime initialDate)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var paymentOrder = await paymentOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            paymentOrder.CheckExistence(documentBaseId);

            if (isApproved)
            {
                await outsourceApproveDao.TrySetIsApprovedAsync(
                    new[] { documentBaseId });
                return;
            }

            await outsourceApproveDao.TryUnsetIsApprovedAsync((int)context.FirmId, documentBaseId, initialDate);
        }

        public async Task SetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var paymentOrders = await paymentOrderDao.GetOperationTypeByBaseIdsAsync((int)context.FirmId, documentBaseIds);
            var existedPaymentOrdersBaseIds = paymentOrders.Select(x => x.DocumentBaseId).ToArray();

            await outsourceApproveDao.TrySetIsApprovedAsync(
                existedPaymentOrdersBaseIds);
        }
    }
}
