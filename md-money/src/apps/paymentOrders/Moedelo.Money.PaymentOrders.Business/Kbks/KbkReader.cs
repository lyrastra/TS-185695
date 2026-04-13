using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Kbks
{
    [InjectAsSingleton(typeof(KbkReader))]
    internal class KbkReader
    {
        private readonly IKbkApiClient kbkClient;

        public KbkReader(IKbkApiClient kbkClient)
        {
            this.kbkClient = kbkClient;
        }

        public async Task<IReadOnlyCollection<Kbk>> GetByAccountCodeAsync(int accountCode)
        {
            // реализация очень наивная под конкретный кейс
            var dtos = await kbkClient.GetAsync();
            return dtos
                .Where(x => x.AccountCode == accountCode)
                .Select(Map)
                .ToArray();
        }

        private static Kbk Map(KbkDto dto)
        {
            return new Kbk
            {
                Id = dto.Id,
                Number = dto.Number,
                KbkPaymentType = (KbkPaymentType)dto.KbkPaymentType,
                PaymentBase = (BudgetaryPaymentBase)dto.PaymentBase,
                DocNumber = dto.DocNumber,
                AccountCode = (BudgetaryAccountCodes)dto.AccountCode
            };
        }
    }
}
