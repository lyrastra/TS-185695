using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Exceptions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentValidator))]
    internal sealed class RentPaymentValidator : IRentPaymentValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly InventoryCardValidator inventoryCardValidator;
        private readonly RentPeriodValidator rentPeriodValidator;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public RentPaymentValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            InventoryCardValidator inventoryCardValidator,
            RentPeriodValidator rentPeriodValidator,
            NumberValidator numberValidator,
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.inventoryCardValidator = inventoryCardValidator;
            this.rentPeriodValidator = rentPeriodValidator;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        public async Task ValidateAsync(RentPaymentSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await kontragentsValidator.ValidateAsync(request.Kontragent);
            await ValidateContractAsync(request);
            await inventoryCardValidator.ValidateAsync(request.InventoryCardBaseId);
            await rentPeriodValidator.ValidateAsync(request.RentPeriods.Select(x => x.RentalPaymentItemId).ToArray());
        }

        private Task ValidateContractAsync(RentPaymentSaveRequest request)
        {
            if (!request.ContractBaseId.HasValue)
            {
                throw new BusinessValidationException("ContractBaseId", "Не указан договор");
            }

            return contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
        }
        
        private async Task ValidatePaymentNumber(RentPaymentSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number);
            }
        }
    }
}
