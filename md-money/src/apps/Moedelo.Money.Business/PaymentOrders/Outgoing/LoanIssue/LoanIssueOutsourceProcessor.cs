using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue;

[InjectAsSingleton(typeof(ILoanIssueOutsourceProcessor))]
internal sealed class LoanIssueOutsourceProcessor : PaymentOrderOutsourceProcessor<LoanIssueSaveRequest>, ILoanIssueOutsourceProcessor
{
    private readonly ILoanIssueValidator validator;
    private readonly ILoanIssueReader reader;
    private readonly ILoanIssueUpdater updater;

    public LoanIssueOutsourceProcessor(
        ILoanIssueValidator validator,
        ILoanIssueReader reader,
        ILoanIssueUpdater updater,
        ILogger<LoanIssueOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(LoanIssueSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<LoanIssueSaveRequest> MapToExistentAsync(LoanIssueSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static LoanIssueSaveRequest MapToExistent(
        LoanIssueResponse existent,
        LoanIssueSaveRequest newValues)
    {
        var result = LoanIssueMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Kontragent = newValues.Kontragent;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0; // Падаем в красную таблицу
        }

        return result;
    }

    protected override Task UpdateAsync(LoanIssueSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(LoanIssueSaveRequest request) => validator.ValidateAsync(request);
}