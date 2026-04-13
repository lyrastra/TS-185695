using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Duplicates
{
    [InjectAsSingleton(typeof(IPaymentOrderDuplicateMerger))]
    internal sealed class PaymentOrderDuplicateMerger : IPaymentOrderDuplicateMerger
    {
        private readonly IPaymentOrderGetter getter;
        private readonly IPaymentOrderRemover remover;
        private readonly Dictionary<OperationType, IConcreteDuplicateMerger> mergers;

        public PaymentOrderDuplicateMerger(
            IPaymentOrderGetter getter,
            IPaymentOrderRemover remover,
            IEnumerable<IConcreteDuplicateMerger> mergers)
        {
            this.mergers = mergers.Select(x =>
                new
                {
                    x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                    Merger = x
                })
                .ToDictionary(x => x.OperationType, x => x.Merger);
            this.getter = getter;
            this.remover = remover;
        }

        public async Task MergeAsync(long documentBaseId)
        {
            var duplicateData = await getter.GetDuplicateDataAsync(documentBaseId).ConfigureAwait(false);
            if (duplicateData == null)
            {
                return;
            }
            if (duplicateData.OperationState != OperationState.Duplicate || duplicateData.DuplicateId == null)
            {
                return;
            }
            var sourceBaseId = await getter.GetOperationBaseIdAsync(duplicateData.DuplicateId.Value).ConfigureAwait(false);
            var sourceOperationType = await getter.GetOperationTypeAsync(sourceBaseId).ConfigureAwait(false);
            if (mergers.TryGetValue(sourceOperationType, out var merger) == false)
            {
                throw new NotImplementedException($"Implementation of IConcreteDuplicateMerger for opration type {sourceOperationType} ({(int)sourceOperationType}) is not found");
            }
            var request = new PaymentOrderDuplicateMergeRequest
            {
                DocumentBaseId = sourceBaseId,
                Date = duplicateData.Date
            };
            await merger.MergeAsync(request).ConfigureAwait(false);
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
        }
    }
}
