using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonImporter))]
    internal sealed class RefundFromAccountablePersonImporter : IRefundFromAccountablePersonImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly IRefundFromAccountablePersonValidator validator;
        private readonly IRefundFromAccountablePersonCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public RefundFromAccountablePersonImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            IRefundFromAccountablePersonValidator validator,
            IRefundFromAccountablePersonCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(RefundFromAccountablePersonImportRequest request)
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
                var saveRequest = RefundFromAccountablePersonMapper.MapToSaveRequest(request);
                await validator.ValidateAsync(saveRequest);
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