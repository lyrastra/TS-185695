using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Banks
{
    [InjectAsSingleton(typeof(IBankReader))]
    internal class BankReader : IBankReader
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;

        private readonly SettingValue catalogApiEndpoint;

        public BankReader(
            IHttpRequestExecuter httpRequestExecuter,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecuter = httpRequestExecuter;
            catalogApiEndpoint = settingRepository.Get("CatalogApiEndpoint");
        }

        public async Task<Bank> GetByIdAsync(int id)
        {
            var uri = $"{catalogApiEndpoint.Value}/Banks/V2/GetByIds";
            var content = new StringContent(new[] { id }.ToJsonString(), Encoding.UTF8, "application/json");
            var response = await httpRequestExecuter.PostAsync(uri, content).ConfigureAwait(false);
            var result = response.FromJsonString<BankDto[]>();
            return Map(result.FirstOrDefault());
        }

        public async Task<Bank> GetByBikAsync(string bik)
        {
            var uri = $"{catalogApiEndpoint.Value}/Banks/V2/GetByBiks";
            var content = new StringContent(new[] { bik }.ToJsonString(), Encoding.UTF8, "application/json");
            var response = await httpRequestExecuter.PostAsync(uri, content).ConfigureAwait(false);
            var result = response.FromJsonString<BankDto[]>();
            return Map(result.FirstOrDefault());
        }

        private static Bank Map(BankDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Bank
            {
                Id = dto.Id,
                Bik = dto.Bik,
                Kpp = dto.Kpp,
                Inn = dto.Inn,
                FullName = dto.FullName,
                City = dto.City,
                FullNameWithCity = dto.FullNameWithCity,
                CorrespondentAccount = dto.CorrespondentAccount
            };
        }
    }

    internal class BankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kpp { get; set; }
        public string Inn { get; set; }
        public string Bik { get; set; }
        public string CorrespondentAccount { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string FullNameWithCity { get; set; }
    }
}
