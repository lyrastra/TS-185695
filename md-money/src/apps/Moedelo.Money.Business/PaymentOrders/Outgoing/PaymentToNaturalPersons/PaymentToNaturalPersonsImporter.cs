using System;
using System.Collections.Generic;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsImporter))]
    internal sealed class PaymentToNaturalPersonsImporter : IPaymentToNaturalPersonsImporter
    {
        private readonly ImportAvailabilityChecker importAvailabilityChecker;
        private readonly PaymentToNaturalPersonsImportValidator validator;
        private readonly IPaymentToNaturalPersonsCreator creator;
        private readonly PaymentOrderImportEventNotifier importEventNotifier;
        private readonly IChargePaymentDetector chargePaymentDetector;

        public PaymentToNaturalPersonsImporter(
            ImportAvailabilityChecker importAvailabilityChecker,
            PaymentToNaturalPersonsImportValidator validator,
            IPaymentToNaturalPersonsCreator creator,
            PaymentOrderImportEventNotifier importEventNotifier,
            IChargePaymentDetector chargePaymentDetector)
        {
            this.importAvailabilityChecker = importAvailabilityChecker;
            this.validator = validator;
            this.creator = creator;
            this.importEventNotifier = importEventNotifier;
            this.chargePaymentDetector = chargePaymentDetector;
        }

        public async Task ImportAsync(PaymentToNaturalPersonsImportRequest request)
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

                var chargePayments = request.OperationState == OperationState.MissingWorker
                    ? new List<ChargePayment> { new ChargePayment { Sum = request.PaymentSum, Description = request.Description } }
                    : await chargePaymentDetector.DetectAsync(request.Description, request.PaymentSum, request.EmployeeId);
                
                var saveRequest = PaymentToNaturalPersonsMapper.MapToSaveRequest(request, chargePayments);
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