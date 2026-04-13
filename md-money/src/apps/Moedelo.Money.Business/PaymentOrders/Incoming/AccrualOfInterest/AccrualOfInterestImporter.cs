using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestImporter))]
    internal sealed class AccrualOfInterestImporter : IAccrualOfInterestImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly AccrualOfInterestImportValidator validator;
        private readonly IAccrualOfInterestCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public AccrualOfInterestImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            AccrualOfInterestImportValidator validator,
            IAccrualOfInterestCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(AccrualOfInterestImportRequest request)
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
                var saveRequest = AccrualOfInterestMapper.MapToSaveRequest(request);
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