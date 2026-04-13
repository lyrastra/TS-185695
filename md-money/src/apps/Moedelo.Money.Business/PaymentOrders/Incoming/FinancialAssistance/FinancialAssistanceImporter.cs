using System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(IFinancialAssistanceImporter))]
    internal sealed class FinancialAssistanceImporter : IFinancialAssistanceImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly FinancialAssistanceImportValidator validator;
        private readonly IFinancialAssistanceCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;

        public FinancialAssistanceImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            FinancialAssistanceImportValidator validator,
            IFinancialAssistanceCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
        }

        public async Task ImportAsync(FinancialAssistanceImportRequest request)
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
                var saveRequest = FinancialAssistanceMapper.MapToSaveRequest(request);
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