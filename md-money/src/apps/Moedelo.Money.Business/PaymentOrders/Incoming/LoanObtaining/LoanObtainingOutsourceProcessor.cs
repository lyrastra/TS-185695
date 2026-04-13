using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining;

[InjectAsSingleton(typeof(ILoanObtainingOutsourceProcessor))]
internal sealed class LoanObtainingOutsourceProcessor : PaymentOrderOutsourceProcessor<LoanObtainingSaveRequest>, ILoanObtainingOutsourceProcessor
{
    private readonly ILoanObtainingValidator validator;
    private readonly ILoanObtainingReader reader;
    private readonly ILoanObtainingUpdater updater;

    public LoanObtainingOutsourceProcessor(
        ILoanObtainingValidator validator,
        ILoanObtainingReader reader,
        ILoanObtainingUpdater updater,
        ILogger<LoanObtainingOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(LoanObtainingSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<LoanObtainingSaveRequest> MapToExistentAsync(LoanObtainingSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static LoanObtainingSaveRequest MapToExistent(
        LoanObtainingResponse existent,
        LoanObtainingSaveRequest newValues)
    {
        var result = LoanObtainingMapper.MapToSaveRequest(existent);

        result.Kontragent = newValues.Kontragent;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0L;
        }

        return result;
    }

    protected override Task UpdateAsync(LoanObtainingSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(LoanObtainingSaveRequest request) => validator.ValidateAsync(request);
}