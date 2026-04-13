using System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashImporter))]
    internal sealed class TransferFromCashImporter : ITransferFromCashImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly TransferFromCashImportValidator validator;
        private readonly ITransferFromCashCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public TransferFromCashImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            TransferFromCashImportValidator validator,
            ITransferFromCashCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(TransferFromCashImportRequest request)
        {
            var isAvailableForImport = await importAvailabilityChecker.IsAvailableAsync(request.Date);
            if (isAvailableForImport == false)
            {
                await importEventNotifier.WriteSkippedAsync(request.ImportId, 
                    request.SourceFileId,
                    request,
                    new SkippedImportReason { IsInClosedPeriod = isAvailableForImport, ImportLogId = request.ImportLogId });
                return;
            }
            try
            {
                await validator.ValidateAsync(request);
                var saveRequest = TransferFromCashMapper.MapToSaveRequest(request);
                var response = await creator.CreateAsync(saveRequest);

                var importRuleIds = request.ImportRuleId.HasValue
                    ? new[] { (int)request.ImportRuleId }
                    : Array.Empty<int>();
                await importEventNotifier.WriteCompletedAsync(request.ImportId, request.SourceFileId, response.DocumentBaseId, importRuleIds, request.ImportLogId);
            }
            catch (BusinessValidationException vex)
            {
                await importEventNotifier.WriteFailedAsync(request.ImportId, request.SourceFileId, request.ImportLogId, vex, request);
            }
        }
    }
}