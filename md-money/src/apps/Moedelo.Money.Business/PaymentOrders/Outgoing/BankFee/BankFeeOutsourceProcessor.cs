using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee;

[InjectAsSingleton(typeof(IBankFeeOutsourceProcessor))]
internal class BankFeeOutsourceProcessor : PaymentOrderOutsourceProcessor<BankFeeSaveRequest>, IBankFeeOutsourceProcessor
{
    private readonly IBankFeeValidator validator;
    private readonly IBankFeeReader reader;
    private readonly IBankFeeUpdater updater;

    public BankFeeOutsourceProcessor(
        IBankFeeValidator validator,
        IBankFeeReader reader,
        IBankFeeUpdater updater,
        ILogger<BankFeeOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(BankFeeSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<BankFeeSaveRequest> MapToExistentAsync(BankFeeSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static BankFeeSaveRequest MapToExistent(
        BankFeeResponse existent,
        BankFeeSaveRequest newValues)
    {
        var result = BankFeeMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings;
        result.TaxationSystemType = newValues.TaxationSystemType;
        result.PatentId = newValues.PatentId;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(BankFeeSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(BankFeeSaveRequest request) => validator.ValidateAsync(request);
}