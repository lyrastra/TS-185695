using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    [InjectAsSingleton(typeof(ITransferFromPurseValidator))]
    class TransferFromPurseValidator : ITransferFromPurseValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IPurseApiClient purseApiClient;

        public TransferFromPurseValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IPurseApiClient purseApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.purseApiClient = purseApiClient;
        }

        public async Task ValidateAsync(TransferFromPurseSaveRequest request)
        {
            await ValidatePursesAsync();
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }

        private async Task ValidatePursesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var purses = await purseApiClient.GetAsync(context.FirmId, context.UserId);
            var isActivePursesExists = purses?.Any(x => x.IsDelete == false) == true;
            if (isActivePursesExists)
            {
                return;
            }

            throw new BusinessValidationException(string.Empty, "Не найдена ни одна активная платежная система")
            {
                Reason = ValidationFailedReason.OperationTypeNotAllowed
            };
        }
    }
}
