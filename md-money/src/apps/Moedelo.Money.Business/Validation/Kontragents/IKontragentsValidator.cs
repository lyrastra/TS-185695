using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain;
using System.Threading.Tasks;
using KontragentWithRequisites = Moedelo.Money.Domain.KontragentWithRequisites;

namespace Moedelo.Money.Business.Validation
{
    internal interface IKontragentsValidator
    {
        Task<Kontragent> ValidateAsync(int kontragentId);

        Task<Kontragent> ValidateAsync(KontragentWithRequisites kontragentWithRequisites);
        Task<Kontragent> ValidateAsync(ContractorWithRequisites contractorWithRequisites);
    }
}
