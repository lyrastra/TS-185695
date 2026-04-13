using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment;

[InjectAsSingleton]
internal sealed class RentPaymentImporter : IRentPaymentImporter
{
    private readonly ImportAvailabilityChecker importAvailabilityChecker;
    private readonly RentPaymentImportValidator validator;
    private readonly IRentPaymentCreator creator;
    private readonly PaymentOrderImportEventNotifier importEventNotifier;

    public RentPaymentImporter(
        ImportAvailabilityChecker importAvailabilityChecker,
        RentPaymentImportValidator validator,
        IRentPaymentCreator creator,
        PaymentOrderImportEventNotifier importEventNotifier)
    {
        this.importAvailabilityChecker = importAvailabilityChecker;
        this.validator = validator;
        this.creator = creator;
        this.importEventNotifier = importEventNotifier;
    }

    public async Task ImportAsync(RentPaymentImportRequest request)
    {
        var isAvailableForImport = await importAvailabilityChecker.IsAvailableAsync(request.Date);
        if (isAvailableForImport == false)
        {
            await importEventNotifier.WriteSkippedAsync(request.ImportId,
                request.SourceFileId,
                request,
                new SkippedImportReason
                    { IsInClosedPeriod = isAvailableForImport, ImportLogId = request.ImportLogId });
            return;
        }

        try
        {
            await validator.ValidateAsync(request);
            var saveRequest = RentPaymentMapper.MapToSaveRequest(request);
            var response = await creator.CreateAsync(saveRequest);

            await importEventNotifier.WriteCompletedAsync(request.ImportId, request.SourceFileId,
                response.DocumentBaseId, request.ImportRuleIds, request.ImportLogId);
        }
        catch (BusinessValidationException vex)
        {
            await importEventNotifier.WriteFailedAsync(request.ImportId, request.SourceFileId, request.ImportLogId,
                vex, request);
        }
    }
}