using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(IContractorsValidator))]
    class ContractorsValidator : IContractorsValidator
    {
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IEmployeeValidator employeeValidator;

        public ContractorsValidator(
            IKontragentsValidator kontragentsValidator,
            IEmployeeValidator employeeValidator)
        {
            this.kontragentsValidator = kontragentsValidator;
            this.employeeValidator = employeeValidator;
        }

        public async Task ValidateAsync(ContractorWithRequisites contractorWithRequisites)
        {
            if (contractorWithRequisites.Type == ContractorType.Kontragent)
            {
                await kontragentsValidator.ValidateAsync(contractorWithRequisites).ConfigureAwait(false);
            }
            else if (contractorWithRequisites.Type == ContractorType.Worker)
            {
                await employeeValidator.ValidateAsync(contractorWithRequisites, "Contractor").ConfigureAwait(false);
            }
        }
    }
}
