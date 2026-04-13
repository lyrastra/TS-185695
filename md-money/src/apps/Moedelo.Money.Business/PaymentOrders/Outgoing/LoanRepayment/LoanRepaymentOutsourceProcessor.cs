using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment;

[InjectAsSingleton(typeof(ILoanRepaymentOutsourceProcessor))]
internal sealed class LoanRepaymentOutsourceProcessor : PaymentOrderOutsourceProcessor<LoanRepaymentSaveRequest>, ILoanRepaymentOutsourceProcessor
{
    private readonly ILoanRepaymentValidator validator;
    private readonly ILoanRepaymentReader reader;
    private readonly ILoanRepaymentUpdater updater;

    public LoanRepaymentOutsourceProcessor(
        ILoanRepaymentValidator validator,
        ILoanRepaymentReader reader,
        ILoanRepaymentUpdater updater,
        ILogger<LoanRepaymentOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(LoanRepaymentSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<LoanRepaymentSaveRequest> MapToExistentAsync(LoanRepaymentSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static LoanRepaymentSaveRequest MapToExistent(
        LoanRepaymentResponse existent,
        LoanRepaymentSaveRequest newValues)
    {
        var result = LoanRepaymentMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Kontragent = newValues.Kontragent;
        result.LoanInterestSum = newValues.LoanInterestSum;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0; // Падаем в красную таблицу
        }

        return result;
    }

    protected override Task UpdateAsync(LoanRepaymentSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(LoanRepaymentSaveRequest request) => validator.ValidateAsync(request);
}