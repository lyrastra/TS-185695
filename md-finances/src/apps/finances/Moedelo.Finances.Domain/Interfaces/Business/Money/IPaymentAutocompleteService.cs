using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Autocomplete;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IPaymentAutocompleteService : IDI
    {
        Task<PaymentAutocompleteResult> GetByCriterionAsync(
            IUserContext userContext,
            PaymentAutocompleteCriterion criterion);
    }
}