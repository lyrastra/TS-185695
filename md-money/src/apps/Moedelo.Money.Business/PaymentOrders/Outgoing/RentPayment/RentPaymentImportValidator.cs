using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment;

[InjectAsSingleton(typeof(RentPaymentImportValidator))]
internal sealed class RentPaymentImportValidator
{
    private readonly NumberValidator numberValidator;
    private readonly ISettlementAccountsValidator settlementAccountsValidator;
    private readonly IKontragentsValidator kontragentsValidator;
    private readonly IContractsValidator contractsValidator;

    public RentPaymentImportValidator(
        NumberValidator numberValidator,
        ISettlementAccountsValidator settlementAccountsValidator,
        IKontragentsValidator kontragentsValidator,
        IContractsValidator contractsValidator)
    {
        this.numberValidator = numberValidator;
        this.settlementAccountsValidator = settlementAccountsValidator;
        this.kontragentsValidator = kontragentsValidator;
        this.contractsValidator = contractsValidator;
    }

    public async Task ValidateAsync(RentPaymentImportRequest request)
    {
        await numberValidator.ValidateAsync(request.Number);
        await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        if (request.OperationState != OperationState.MissingKontragent)
        {
            await kontragentsValidator.ValidateAsync(request.Kontragent.Id);
            if (request.ContractBaseId.HasValue)
            {
                await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
            }
        }
    }
}