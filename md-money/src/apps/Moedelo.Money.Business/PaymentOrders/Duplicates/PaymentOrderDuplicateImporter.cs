using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Duplicates;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Duplicates
{
    [InjectAsSingleton(typeof(IPaymentOrderDuplicateImporter))]
    internal sealed class PaymentOrderDuplicateImporter : IPaymentOrderDuplicateImporter
    {
        private readonly IPaymentOrderGetter getter;
        private readonly Dictionary<OperationType, IConcreteDuplicateImporter> importers;

        public PaymentOrderDuplicateImporter(
            IPaymentOrderGetter getter,
            IEnumerable<IConcreteDuplicateImporter> importers)
        {
            this.getter = getter;
            this.importers = importers.Select(x => new
                {
                    x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                    Importer = x
                })
                .ToDictionary(x => x.OperationType, x => x.Importer);
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var duplicateData = await getter.GetDuplicateDataAsync(documentBaseId).ConfigureAwait(false);
            if (duplicateData == null)
            {
                return;
            }
            if (duplicateData.OperationState != OperationState.Duplicate)
            {
                return;
            }
            var operationType = duplicateData.OperationType;
            if (importers.TryGetValue(operationType, out var importer) == false)
            {
                throw new NotImplementedException($"Implementation of IConcreteDuplicateImporter for opration type {operationType} ({(int)operationType}) is not found");
            }
            await importer.ImportAsync(documentBaseId).ConfigureAwait(false);
        }
    }
}
