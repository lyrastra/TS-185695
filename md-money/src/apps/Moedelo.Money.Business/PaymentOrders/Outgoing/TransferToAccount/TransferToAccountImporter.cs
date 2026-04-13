using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountImporter))]
    internal sealed class TransferToAccountImporter : ITransferToAccountImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly TransferToAccountImportValidator validator;
        private readonly ITransferToAccountCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public TransferToAccountImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            TransferToAccountImportValidator validator,
            ITransferToAccountCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(TransferToAccountImportRequest request)
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
                var saveRequest = TransferToAccountMapper.MapToSaveRequest(request);
                var response = await creator.CreateAsync(saveRequest);

                await importEventNotifier.WriteCompletedAsync(request.ImportId, request.SourceFileId, response.DocumentBaseId, request.ImportRuleIds, request.ImportLogId);
            }
            catch (BusinessValidationException vex)
            {
                await importEventNotifier.WriteFailedAsync(request.ImportId, request.SourceFileId, request.ImportLogId, vex, request);
            }
        }
    }
}