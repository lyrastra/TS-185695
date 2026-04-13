using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierImporter))]
    internal sealed class PaymentToSupplierImporter : IPaymentToSupplierImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly PaymentToSupplierImportValidator validator;
        private readonly IPaymentToSupplierCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public PaymentToSupplierImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            PaymentToSupplierImportValidator validator,
            IPaymentToSupplierCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(PaymentToSupplierImportRequest request)
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
                var saveRequest = PaymentToSupplierMapper.MapToSaveRequest(request);
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