using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.Commands.CashOrders;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders;
using Moedelo.Money.Business.Abstractions.Commands.PurseOperations;
using Moedelo.Money.Business.Abstractions.Commands;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.Operations.TaxationSystemChangingSync;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(IChangeTaxationSystemSender))]
    internal class ChangeTaxationSystemSender : IChangeTaxationSystemSender
    {
        private readonly IChangeTaxationSystemCommandWriter writer;
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly IPaymentOrderChangeTaxationSystemCommandWriter paymentOrderWriter;
        private readonly ICashOrderChangeTaxationSystemCommandWriter cashOrderWriter;
        private readonly IPurseOperationChangeTaxationSystemCommandWriter purseOperationWriter;
        private readonly TaxationSystemChangingSyncObjectInitializer syncObjectInitializer;
        private readonly ChangeTaxationSystemNotifier changeTaxationSystemNotifier;

        public ChangeTaxationSystemSender(
            IChangeTaxationSystemCommandWriter writer,
            IBaseDocumentReader baseDocumentReader,
            IPaymentOrderChangeTaxationSystemCommandWriter paymentOrderWriter,
            ICashOrderChangeTaxationSystemCommandWriter cashOrderWriter,
            IPurseOperationChangeTaxationSystemCommandWriter purseOperationWriter,
            TaxationSystemChangingSyncObjectInitializer syncObjectInitializer,
            ChangeTaxationSystemNotifier changeTaxationSystemNotifier)
        {
            this.writer = writer;
            this.baseDocumentReader = baseDocumentReader;
            this.paymentOrderWriter = paymentOrderWriter;
            this.cashOrderWriter = cashOrderWriter;
            this.purseOperationWriter = purseOperationWriter;
            this.syncObjectInitializer = syncObjectInitializer;
            this.changeTaxationSystemNotifier = changeTaxationSystemNotifier;
        }

        public Task SendCommandAsync(IReadOnlyCollection<long> documentBaseIds, TaxationSystemType taxationSystemType)
        {
            return writer.WriteAsync(new ChangeTaxationSystemCommand
            {
                DocumentBaseIds = documentBaseIds,
                TaxationSystemType = taxationSystemType,
                Guid = Guid.NewGuid()
            });
        }

        public async Task DistributeCommandsAsync(
            IReadOnlyCollection<long> documentBaseIds,
            TaxationSystemType taxationSystemType, 
            Guid guid)
        {
            var documents = await baseDocumentReader.GetByIdsAsync(documentBaseIds);

            var affectedDocuments = GetAffectedDocuments(documents);

            if (!affectedDocuments.HasAnyDocuments())
            {
                await changeTaxationSystemNotifier.NotifyAsync(guid);
                return;
            }

            // этот вызов должен быть до или после отправки команд?
            await syncObjectInitializer.InitializeAsync(guid, documentBaseIds);

            // блок отправки команд
            await Task.WhenAll(
                affectedDocuments.PaymentOrders
                    .RunParallelAsync(baseDocument => paymentOrderWriter.WriteAsync(
                            CommandMapper.MapPaymentOrderCommand(baseDocument.Id, taxationSystemType, guid))),
                affectedDocuments.IncomingCashOrders
                    .RunParallelAsync(baseDocument => cashOrderWriter.WriteAsync(
                            CommandMapper.MapCashOrderCommand(baseDocument.Id, taxationSystemType, guid))),
                affectedDocuments.PurseOperations
                    .RunParallelAsync(baseDocument => purseOperationWriter.WriteAsync(
                            CommandMapper.MapPurseOperationCommand(baseDocument.Id, taxationSystemType, guid))));
        }

        private struct AffectedDocuments
        {
            public IReadOnlyCollection<BaseDocument> PaymentOrders { get; set; }
            public IReadOnlyCollection<BaseDocument> IncomingCashOrders { get; set; }
            public IReadOnlyCollection<BaseDocument> PurseOperations { get; set; }

            public bool HasAnyDocuments()
            {
                return PaymentOrders.Any()
                       || IncomingCashOrders.Any()
                       || PurseOperations.Any();
            }
        }

        private AffectedDocuments GetAffectedDocuments(IEnumerable<BaseDocument> documents)
        {
            var documentByType = documents
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.ToArray());

            return new AffectedDocuments
            {
                PaymentOrders = documentByType.GetValueOrDefault(LinkedDocumentType.PaymentOrder) ?? Array.Empty<BaseDocument>(),
                IncomingCashOrders = documentByType.GetValueOrDefault(LinkedDocumentType.IncomingCashOrder) ?? Array.Empty<BaseDocument>(),
                PurseOperations = documentByType.GetValueOrDefault(LinkedDocumentType.PurseOperation) ?? Array.Empty<BaseDocument>()
            };
        }
    }
}
