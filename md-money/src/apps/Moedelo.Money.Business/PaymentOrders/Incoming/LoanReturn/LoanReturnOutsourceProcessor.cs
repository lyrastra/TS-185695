using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn;

[InjectAsSingleton(typeof(ILoanReturnOutsourceProcessor))]
internal sealed class LoanReturnOutsourceProcessor : PaymentOrderOutsourceProcessor<LoanReturnSaveRequest>, ILoanReturnOutsourceProcessor
{
    private readonly ILoanReturnValidator validator;
    private readonly ILoanReturnReader reader;
    private readonly ILoanReturnUpdater updater;

    public LoanReturnOutsourceProcessor(
        ILoanReturnValidator validator,
        ILoanReturnReader reader,
        ILoanReturnUpdater updater,
        ILogger<LoanReturnOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(LoanReturnSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<LoanReturnSaveRequest> MapToExistentAsync(LoanReturnSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static LoanReturnSaveRequest MapToExistent(
        LoanReturnResponse existent,
        LoanReturnSaveRequest newValues)
    {
        var result = LoanReturnMapper.MapToSaveRequest(existent);

        result.LoanInterestSum = newValues.LoanInterestSum;
        result.Kontragent = newValues.Kontragent;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0L;
        }

        return result;
    }

    protected override Task UpdateAsync(LoanReturnSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(LoanReturnSaveRequest request) => validator.ValidateAsync(request);
}