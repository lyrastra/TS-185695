using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonImporter))]
    internal sealed class PaymentToAccountablePersonImporter : IPaymentToAccountablePersonImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly PaymentToAccountablePersonImportValidator validator;
        private readonly IPaymentToAccountablePersonCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public PaymentToAccountablePersonImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            PaymentToAccountablePersonImportValidator validator,
            IPaymentToAccountablePersonCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(PaymentToAccountablePersonImportRequest request)
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

                PaymentOrderSaveResponse response;
                
                if (request.OperationState == OperationState.MissingWorker)
                {
                    var saveRequest = PaymentToAccountablePersonMapper.MapToMissingSaveRequest(request);
                    response = await creator.CreateWithMissingEmployeeAsync(saveRequest);
                }
                else
                {
                    var saveRequest = PaymentToAccountablePersonMapper.MapToSaveRequest(request);
                    response = await creator.CreateAsync(saveRequest);
                }

                await importEventNotifier.WriteCompletedAsync(request.ImportId, request.SourceFileId, response.DocumentBaseId, request.ImportRuleIds, request.ImportLogId);
            }
            catch (BusinessValidationException vex)
            {
                await importEventNotifier.WriteFailedAsync(request.ImportId, request.SourceFileId, request.ImportLogId, vex, request);
            }
        }
    }
}