using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

[InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentOutsourceProcessor))]
internal sealed class UnifiedBudgetaryPaymentOutsourceProcessor : PaymentOrderOutsourceProcessor<UnifiedBudgetaryPaymentSaveRequest>, IUnifiedBudgetaryPaymentOutsourceProcessor
{
    private readonly IUnifiedBudgetaryPaymentValidator validator;
    private readonly IUnifiedBudgetaryPaymentReader reader;
    private readonly IUnifiedBudgetaryPaymentUpdater updater;
    private readonly BudgetaryRecipientRequisitesReader recipientRequisitesReader;
    private readonly IUnifiedBudgetaryPaymentDescriptor descriptor;

    public UnifiedBudgetaryPaymentOutsourceProcessor(
        IUnifiedBudgetaryPaymentValidator validator,
        IUnifiedBudgetaryPaymentReader reader,
        IUnifiedBudgetaryPaymentUpdater updater,
        ILogger<UnifiedBudgetaryPaymentOutsourceProcessor> logger,
        BudgetaryRecipientRequisitesReader recipientRequisitesReader,
        IUnifiedBudgetaryPaymentDescriptor descriptor) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.recipientRequisitesReader = recipientRequisitesReader;
        this.descriptor = descriptor;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(UnifiedBudgetaryPaymentSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<UnifiedBudgetaryPaymentSaveRequest> MapToExistentAsync(UnifiedBudgetaryPaymentSaveRequest request)
    {
        // дозаполняем обязательные поля ЕНП здесь,
        // т.к. при смене из другого типа ниже по стеку сгенерируется исключение и возвратится исходная модель
        await EnrichRequestAsync(request);

        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);

        return MapToExistent(existent, request);
    }

    private async Task EnrichRequestAsync(UnifiedBudgetaryPaymentSaveRequest request)
    {
        // получатель ЕНП
        request.Recipient = await GetBudgetaryRecipientAsync();
        // разбиение на подплатежи: точь-в-точь как в импорте
        var subPayments = await descriptor.GetSubPayments(request.Description);
        if (subPayments.Count == 1)
            subPayments.First().Sum = request.Sum;

        request.SubPayments = subPayments.MapToSaveRequests();
        if (request.SubPayments.Count == 0 || request.SubPayments.IsBadSubPaymentExists())
        {
            request.OperationState = OperationState.NoSubPayments;
        }
    }

    private async Task<UnifiedBudgetaryRecipient> GetBudgetaryRecipientAsync()
    {
        var recipient = await recipientRequisitesReader.GetUnifiedBudgetaryPaymentFnsDepartmentRequisitesAsync();
        
        return new UnifiedBudgetaryRecipient
        {
            Name = recipient?.Name,
            Inn = recipient?.Inn,
            Kpp = recipient?.Kpp,
            SettlementAccount = recipient?.SettlementAccount,
            BankBik = recipient?.BankBik,
            BankName = recipient?.BankName,
            UnifiedSettlementAccount = recipient?.UnifiedSettlementAccount,
            Oktmo = "0"
        };
    }

    private static UnifiedBudgetaryPaymentSaveRequest MapToExistent(
        UnifiedBudgetaryPaymentResponse existent,
        UnifiedBudgetaryPaymentSaveRequest newValues)
    {
        var result = UnifiedBudgetaryPaymentMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Recipient ??= newValues.Recipient;
        result.SubPayments ??= Array.Empty<UnifiedBudgetarySubPaymentSaveModel>();

        foreach (var subPayment in result.SubPayments)
        {
            subPayment.TaxPostings ??= new TaxPostingsData();
        }

        return result;
    }

    protected override Task UpdateAsync(UnifiedBudgetaryPaymentSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(UnifiedBudgetaryPaymentSaveRequest request) => validator.ValidateAsync(request);
}