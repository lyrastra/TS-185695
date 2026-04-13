using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction;

[InjectAsSingleton(typeof(IDeductionOutsourceProcessor))]
internal sealed class DeductionOutsourceProcessor : PaymentOrderOutsourceProcessor<DeductionSaveRequest>, IDeductionOutsourceProcessor
{
    private readonly IDeductionValidator validator;
    private readonly IDeductionReader reader;
    private readonly IDeductionUpdater updater;
    private readonly DeductionAccPostingsGetter deductionAccPostingsGetter;

    public DeductionOutsourceProcessor(
        IDeductionValidator validator,
        IDeductionReader reader,
        IDeductionUpdater updater,
        ILogger<DeductionOutsourceProcessor> logger,
        DeductionAccPostingsGetter deductionAccPostingsGetter) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.deductionAccPostingsGetter = deductionAccPostingsGetter;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(DeductionSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<DeductionSaveRequest> MapToExistentAsync(DeductionSaveRequest request)
    {
        // проводка точно как в импорте (и обязательно в самом начале, т. к. при смене операции вернется исходная модель)
        request.AccountingPosting = await deductionAccPostingsGetter
            .GetAsync(DeductionMapper.MapToAccPostingRequest(request));
        
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        var result = MapToExistent(existent, request);

        return result;
    }

    private static DeductionSaveRequest MapToExistent(
        DeductionResponse existent,
        DeductionSaveRequest newValues)
    {
        var result = DeductionMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Contractor = newValues.Contractor;
        result.AccountingPosting = newValues.AccountingPosting;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Contractor?.Id != newValues.Contractor?.Id)
        {
            result.ContractBaseId = null;
        }

        return result;
    }

    protected override Task UpdateAsync(DeductionSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(DeductionSaveRequest request) => validator.ValidateAsync(request);
}