using System.Linq;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Money.Autocomplete;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.RefundPaymentAutocomplete
{
    [InjectAsSingleton]
    public class RefundPaymentAutocompleteDao : IRefundPaymentAutocompleteDao
    {
        private readonly IMoedeloDbExecutor dbExecutor;

        public RefundPaymentAutocompleteDao(IMoedeloDbExecutor dbExecutor)
        {
            this.dbExecutor = dbExecutor;
        }

        public async Task<ListWithCount<PaymentAutocompleteItem>> GetByCriterionAsync(int firmId, PaymentAutocompleteCriterion criterion)
        {
            var queryObject = RefundPaymentAutocompleteQueryBuilder.GetByCriterion(firmId, criterion);
            var items = await dbExecutor.QueryAsync<PaymentAutocompleteItem>(queryObject).ConfigureAwait(false);

            return new ListWithCount<PaymentAutocompleteItem>
            {
                Items = items,
                TotalCount = items.FirstOrDefault()?.TotalCount ?? 0
            };
        }
    }
}
