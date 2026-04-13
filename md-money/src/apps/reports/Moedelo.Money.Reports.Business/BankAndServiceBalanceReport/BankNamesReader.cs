using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Reports.Domain.Banks;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport
{
    [InjectAsSingleton(typeof(BankNamesReader))]
    internal class BankNamesReader
    {
        private readonly IBankApiClient bankApiClient;

        public BankNamesReader(IBankApiClient bankApiClient)
        {
            this.bankApiClient = bankApiClient;
        }

        public async Task<IReadOnlyDictionary<int, BankName>> GetByIdsAsync(IReadOnlyCollection<int> ids)
        {
            var banks = await bankApiClient.GetByIdsAsync(ids);

            var bankNames = banks.Select(Map).ToArray();

            return bankNames.ToDictionary(x => x.Id);
        }

        private static BankName Map(BankDto bank)
        {
            return new BankName
            {
                Id = bank.Id,
                Name = bank.FullName
            };
        }
    }
}
