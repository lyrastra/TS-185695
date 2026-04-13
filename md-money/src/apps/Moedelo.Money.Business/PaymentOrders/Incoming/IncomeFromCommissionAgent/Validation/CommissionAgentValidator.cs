using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.CommissionAgents;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent.Validation
{
    [InjectAsSingleton(typeof(CommissionAgentValidator))]
    class CommissionAgentValidator
    {
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly CommissionAgentsReader commissionAgentsReader;

        public CommissionAgentValidator(
            IKontragentsValidator kontragentsValidator,
            CommissionAgentsReader commissionAgentsReader)
        {
            this.kontragentsValidator = kontragentsValidator;
            this.commissionAgentsReader = commissionAgentsReader;
        }

        public async Task ValidateAsync(KontragentWithRequisites kontragent)
        {
            await kontragentsValidator.ValidateAsync(kontragent);
            var commissionAgent = await commissionAgentsReader.GetByKontragentIdAsync(kontragent.Id);
            if (commissionAgent == null)
            {
                throw new BusinessValidationException("Contractor.Id", $"Не найден комиссионер с ид {kontragent.Id}");
            }
            if (string.IsNullOrWhiteSpace(kontragent.Inn) == false &&
                commissionAgent.Inn != kontragent.Inn)
            {
                throw new BusinessValidationException("Contractor.Inn", $"ИНН контрагента не соответствект ИНН комиссионера");
            }
        }
    }
}
