using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingImporter))]
    internal sealed class OtherOutgoingImporter : IOtherOutgoingImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;
        private readonly OtherOutgoingImportValidator validator;
        private readonly IOtherOutgoingCreator creator;
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public OtherOutgoingImporter(ImportAvailabilityChecker importAvailabilityChecker,
            PaymentOrderImportEventNotifier importEventNotifier,
            OtherOutgoingImportValidator validator,
            IOtherOutgoingCreator creator,
            ISettlementAccountsReader settlementAccountsReader)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.importEventNotifier = importEventNotifier;
            this.validator = validator;
            this.creator = creator;
            this.settlementAccountsReader = settlementAccountsReader;
        }

        public async Task ImportAsync(OtherOutgoingImportRequest request)
        {
            // Базовейшая валидация (на предмет закрытого периода)
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

                // Проставляем параметры проводки
                await EnrichIncompleteAccPostingAsync(request);

                var saveRequest = OtherOutgoingMapper.MapToSaveRequest(request);
                var response = await creator.CreateAsync(saveRequest);

                await importEventNotifier.WriteCompletedAsync(request.ImportId, request.SourceFileId, response.DocumentBaseId, request.ImportRuleIds, request.ImportLogId);
            }
            catch (BusinessValidationException vex)
            {
                await importEventNotifier.WriteFailedAsync(request.ImportId, request.SourceFileId, request.ImportLogId, vex, request);
            }
        }

        private async Task EnrichIncompleteAccPostingAsync(OtherOutgoingImportRequest request)
        {
            var accPosting = request.AccPosting;
            if (accPosting == null)
            {
                return;
            }

            if (accPosting.CreditSubconto == 0)
            {
                var settlementAccount = await settlementAccountsReader.GetByIdAsync(request.SettlementAccountId);
                accPosting.CreditSubconto = settlementAccount.SubcontoId!.Value;
            }
        }
    }
}
