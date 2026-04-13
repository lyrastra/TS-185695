using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Money.Autocomplete;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money
{
    public interface IRefundPaymentAutocompleteDao : IDI
    {
        Task<ListWithCount<PaymentAutocompleteItem>> GetByCriterionAsync(int firmId, PaymentAutocompleteCriterion criterion);
    }
}