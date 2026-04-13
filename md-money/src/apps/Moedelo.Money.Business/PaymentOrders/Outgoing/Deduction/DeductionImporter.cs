using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IDeductionImporter))]
    internal sealed class DeductionImporter : IDeductionImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;
        private readonly DeductionImportValidator validator;
        private readonly IDeductionCreator creator;
        private readonly DeductionAccPostingsGetter deductionAccPostingsGetter;

        public DeductionImporter(ImportAvailabilityChecker importAvailabilityChecker,
            PaymentOrderImportEventNotifier importEventNotifier,
            DeductionImportValidator validator,
            IDeductionCreator creator,
            DeductionAccPostingsGetter deductionAccPostingsGetter)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.importEventNotifier = importEventNotifier;
            this.validator = validator;
            this.creator = creator;
            this.deductionAccPostingsGetter = deductionAccPostingsGetter;
        }

        public async Task ImportAsync(DeductionImportRequest request)
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
                var saveRequest = DeductionMapper.MapToSaveRequest(request);
                saveRequest.AccountingPosting = await deductionAccPostingsGetter.GetAsync(DeductionMapper.MapToAccPostingRequest(request));
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
