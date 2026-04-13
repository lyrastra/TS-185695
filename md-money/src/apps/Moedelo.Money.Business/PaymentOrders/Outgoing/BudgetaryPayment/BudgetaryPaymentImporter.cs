using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentImporter))]
    internal sealed class BudgetaryPaymentImporter : IBudgetaryPaymentImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly BudgetaryPaymentImportValidator validator;
        private readonly IBudgetaryPaymentCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public BudgetaryPaymentImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            BudgetaryPaymentImportValidator validator,
            IBudgetaryPaymentCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(BudgetaryPaymentImportRequest request)
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
                var saveRequest = BudgetaryPaymentMapper.MapToSaveRequest(request);
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