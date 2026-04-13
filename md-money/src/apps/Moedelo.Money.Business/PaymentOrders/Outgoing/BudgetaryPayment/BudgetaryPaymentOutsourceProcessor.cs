using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment;

[InjectAsSingleton(typeof(IBudgetaryPaymentOutsourceProcessor))]
internal sealed class BudgetaryPaymentOutsourceProcessor : PaymentOrderOutsourceProcessor<BudgetaryPaymentSaveRequest>, IBudgetaryPaymentOutsourceProcessor
{
    private readonly IBudgetaryPaymentValidator validator;
    private readonly IBudgetaryPaymentReader reader;
    private readonly IBudgetaryPaymentUpdater updater;
    private readonly BudgetaryRecipientRequisitesReader recipientRequisitesReader;

    public BudgetaryPaymentOutsourceProcessor(
        IBudgetaryPaymentValidator validator,
        IBudgetaryPaymentReader reader,
        IBudgetaryPaymentUpdater updater,
        ILogger<BudgetaryPaymentOutsourceProcessor> logger,
        BudgetaryRecipientRequisitesReader recipientRequisitesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.recipientRequisitesReader = recipientRequisitesReader;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(BudgetaryPaymentSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<BudgetaryPaymentSaveRequest> MapToExistentAsync(BudgetaryPaymentSaveRequest request)
    {
        // Обязательно указываем получателя бюджетного платежа. Без него в ЛК нельзя сохранить бюджетный платёж и перевыбрать получателя нельзя.
        // Делаем это здесь, потому что при смене из любого типа на бюджетный это поле взять негде.
        var defaultBudgetaryRecipient = await GetBudgetaryRecipientAsync();
        request.Recipient = defaultBudgetaryRecipient;

        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);

        return MapToExistent(existent, request, defaultBudgetaryRecipient);
    }

    private async Task<BudgetaryRecipient> GetBudgetaryRecipientAsync()
    {
        var recipient = await recipientRequisitesReader.GetUnifiedBudgetaryPaymentFnsDepartmentRequisitesAsync();
        return recipient != null
            ? new BudgetaryRecipient
            {
                Name = recipient?.Name,
                Inn = recipient?.Inn,
                Kpp = recipient?.Kpp,
                SettlementAccount = recipient?.SettlementAccount,
                BankBik = recipient?.BankBik,
                BankName = recipient?.BankName,
                UnifiedSettlementAccount = recipient?.UnifiedSettlementAccount,
                Oktmo = "0"
            }
            : new BudgetaryRecipient();
    }

    private static BudgetaryPaymentSaveRequest MapToExistent(
        BudgetaryPaymentResponse existent,
        BudgetaryPaymentSaveRequest newValues,
        BudgetaryRecipient budgetaryRecipient)
    {
        var result = BudgetaryPaymentMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        result.Recipient ??= budgetaryRecipient;
        result.TaxPostings ??= new TaxPostingsData();
        result.Period ??= new BudgetaryPeriod
        {
            Type = BudgetaryPeriodType.Month,
            Date = existent.Date,
            Number = existent.Date.Month,
            Year = existent.Date.Year,
        };

        return result;
    }

    protected override Task UpdateAsync(BudgetaryPaymentSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(BudgetaryPaymentSaveRequest request) => validator.ValidateAsync(request);
}