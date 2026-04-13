using System.Linq;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Models.Money.Autocomplete;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.KontragentsV2.Client.Kontragents;

namespace Moedelo.Finances.Business.Services.Money
{
    [InjectAsSingleton]
    public class PaymentAutocompleteService : IPaymentAutocompleteService
    {
        private const int maxLimit = 50;

        private readonly IRefundPaymentAutocompleteDao refundPaymentAutocompleteDao;
        private readonly IKontragentsClient kontragentsClient;

        public PaymentAutocompleteService(
            IRefundPaymentAutocompleteDao refundPaymentAutocompleteDao,
            IKontragentsClient kontragentsClient)
        {
            this.refundPaymentAutocompleteDao = refundPaymentAutocompleteDao;
            this.kontragentsClient = kontragentsClient;
        }

        public async Task<PaymentAutocompleteResult> GetByCriterionAsync(
            IUserContext userContext,
            PaymentAutocompleteCriterion criterion)
        {
            if (criterion.Limit > maxLimit)
            {
                criterion.Limit = maxLimit;
            }

            var data = await refundPaymentAutocompleteDao.GetByCriterionAsync(
                userContext.FirmId,
                criterion).ConfigureAwait(false);

            var kontragentIds = data.Items.Select(x => x.KontragentId).Distinct().ToList();
            var kontragents = await kontragentsClient.GetByIdsAsync(
                userContext.FirmId,
                userContext.UserId,
                kontragentIds).ConfigureAwait(false);

            foreach (var item in data.Items)
            {
                var kontragent = kontragents.FirstOrDefault(k => k.Id == item.KontragentId);
                item.KontragentName = kontragent?.ShortName ?? kontragent?.Fio;
            }

            return new PaymentAutocompleteResult
            {
                List = data.Items,
                TotalCount = data.TotalCount,
                Offset = criterion.Offset,
                Limit = criterion.Limit,
            };
        }
    }
}
