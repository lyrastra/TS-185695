using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount;

[InjectAsSingleton(typeof(IRefundToSettlementAccountOutsourceProcessor))]
internal sealed class RefundToSettlementAccountOutsourceProcessor : PaymentOrderOutsourceProcessor<RefundToSettlementAccountSaveRequest>, IRefundToSettlementAccountOutsourceProcessor
{
    private readonly IRefundToSettlementAccountValidator validator;
    private readonly IRefundToSettlementAccountReader reader;
    private readonly IRefundToSettlementAccountUpdater updater;
    private readonly RefundToSettlementAccountAccPostingGetter accountAccPostingGetter;

    public RefundToSettlementAccountOutsourceProcessor(
        IRefundToSettlementAccountValidator validator,
        IRefundToSettlementAccountReader reader,
        IRefundToSettlementAccountUpdater updater,
        ILogger<RefundToSettlementAccountOutsourceProcessor> logger,
        RefundToSettlementAccountAccPostingGetter accountAccPostingGetter)
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.accountAccPostingGetter = accountAccPostingGetter;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(RefundToSettlementAccountSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<RefundToSettlementAccountSaveRequest> MapToExistentAsync(RefundToSettlementAccountSaveRequest request)
    {
        // всегда подставляем проводку до загрузки операции (если была смена типа, вернется исходная модель)
        request.AccPosting = await accountAccPostingGetter.GetDefaultAsync(request);
        
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        var result = MapToExistent(existent, request);

        var contractorChanged = existent?.Contractor?.Id != request.Contractor?.Id;
        if (contractorChanged)
        {
            // поменялся контрагент: установить связанные поля в значения по умолчанию
            result.ContractBaseId = null;
            result.AccPosting = await accountAccPostingGetter.GetDefaultAsync(result);
        } 

        return result;
    }

    protected override Task UpdateAsync(RefundToSettlementAccountSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(RefundToSettlementAccountSaveRequest request) => validator.ValidateAsync(request);

    private static RefundToSettlementAccountSaveRequest MapToExistent(
        RefundToSettlementAccountResponse existent,
        RefundToSettlementAccountSaveRequest newValues)
    {
        var result = RefundToSettlementAccountMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings ?? new TaxPostingsData();
        // в таком случае НУ не должен обрабатываться => останется старая проводка, если была
        result.TaxPostings.ProvidePostingType = ProvidePostingType.Auto;
        result.Contractor = newValues.Contractor;
        result.TaxationSystemType = newValues.TaxationSystemType;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.AccPosting = newValues.AccPosting;

        return result;
    }
}