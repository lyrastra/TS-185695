using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(IRefundToSettlementAccountImporter))]
    internal sealed class RefundToSettlementAccountImporter : IRefundToSettlementAccountImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;
        private readonly RefundToSettlementAccountImportValidator validator;
        private readonly IRefundToSettlementAccountCreator creator;
        private readonly RefundToSettlementAccountAccPostingGetter accPostingGetter;

        public RefundToSettlementAccountImporter(ImportAvailabilityChecker importAvailabilityChecker,
            PaymentOrderImportEventNotifier importEventNotifier,
            RefundToSettlementAccountImportValidator validator,
            IRefundToSettlementAccountCreator creator,
            RefundToSettlementAccountAccPostingGetter accPostingGetter)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.importEventNotifier = importEventNotifier;
            this.validator = validator;
            this.creator = creator;
            this.accPostingGetter = accPostingGetter;
        }

        public async Task ImportAsync(ImportRefundToSettlementAccountRequest request)
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

                var saveRequest = RefundToSettlementAccountMapper.MapToSaveRequest(request);
                saveRequest.AccPosting = await accPostingGetter.GetDefaultAsync(saveRequest);

                var response = await creator.CreateAsync(saveRequest);

                await importEventNotifier.WriteCompletedAsync(request.ImportId, request.SourceFileId, response.DocumentBaseId, Array.Empty<int>(), request.ImportLogId);
            }
            catch (BusinessValidationException vex)
            {
                await importEventNotifier.WriteFailedAsync(request.ImportId, request.SourceFileId, request.ImportLogId, vex, request);
            }
        }
    }
}
