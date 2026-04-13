using System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierImporter))]
    internal sealed class CurrencyPaymentToSupplierImporter : ICurrencyPaymentToSupplierImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly CurrencyPaymentToSupplierImportValidator validator;
        private readonly ICurrencyPaymentToSupplierCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public CurrencyPaymentToSupplierImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            CurrencyPaymentToSupplierImportValidator validator,
            ICurrencyPaymentToSupplierCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(CurrencyPaymentToSupplierImportRequest request)
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
                var saveRequest = CurrencyPaymentToSupplierMapper.MapToSaveRequest(request);
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