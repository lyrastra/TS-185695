using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.PaymentOrders.Import;
using System;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Linq;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentImporter))]
    internal sealed class UnifiedBudgetaryPaymentImporter : IUnifiedBudgetaryPaymentImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly UnifiedBudgetaryPaymentImportValidator validator;
        private readonly IUnifiedBudgetaryPaymentCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;
        private readonly IUnifiedBudgetaryPaymentDescriptor descriptor;

        public UnifiedBudgetaryPaymentImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            UnifiedBudgetaryPaymentImportValidator validator,
            IUnifiedBudgetaryPaymentCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier,
            IUnifiedBudgetaryPaymentDescriptor descriptor)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
            this.descriptor = descriptor;
        }

        public async Task ImportAsync(UnifiedBudgetaryPaymentImportRequest request)
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
                var subPayments = await descriptor.GetSubPayments(request.Description);
                if (subPayments.Count == 1)
                    subPayments.First().Sum = request.Sum;
                var saveRequest = UnifiedBudgetaryPaymentMapper.MapToSaveRequest(request, subPayments);
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