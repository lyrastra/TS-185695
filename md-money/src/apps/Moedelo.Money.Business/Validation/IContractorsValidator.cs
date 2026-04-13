using Moedelo.Money.Domain;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface IContractorsValidator
    {
        Task ValidateAsync(ContractorWithRequisites contractorWithRequisites);
    }
}
