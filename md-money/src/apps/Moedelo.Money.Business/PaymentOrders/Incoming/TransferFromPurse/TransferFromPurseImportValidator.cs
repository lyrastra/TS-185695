using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    [InjectAsSingleton(typeof(TransferFromPurseImportValidator))]
    class TransferFromPurseImportValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IPurseApiClient purseApiClient;

        public TransferFromPurseImportValidator(
            IExecutionInfoContextAccessor contextAccessor,
            ISettlementAccountsValidator settlementAccountsValidator,
            IPurseApiClient purseApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.purseApiClient = purseApiClient;

        }

        public async Task ValidateAsync(TransferFromPurseImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            await ValidatePursesAsync(request).ConfigureAwait(false);
        }

        private async Task ValidatePursesAsync(TransferFromPurseImportRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var purses = await purseApiClient.GetAsync(context.FirmId, context.UserId);
            var isActivePursesExists = purses?.Any(x => x.IsDelete == false) == true;
            if (isActivePursesExists)
            {
                return;
            }
            throw new BusinessValidationException(string.Empty, "Не найдена ни одна активная платежная система");
        }
    }
}
