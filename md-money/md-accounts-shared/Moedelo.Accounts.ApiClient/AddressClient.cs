using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using System.Threading.Tasks;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IAddressClient))]
    internal sealed class AddressClient : IAddressClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue endpointSetting;

        public AddressClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter
        )
        {
            this.httpRequestExecuter = httpRequestExecuter;
            endpointSetting = settingRepository.Get("AddressApiEndpoint");
        }

        public async Task<string> GetAddressStringAsync(long firmId, bool withAdditionalInfo)
        {
            var uri = $"{ApiEndPoint()}/GetFirmAddressString?firmId={firmId}&withAdditionalInfo={withAdditionalInfo}";
            var response = await httpRequestExecuter.GetAsync(uri);
            return response.FromJsonString<string>();
        }

        private string ApiEndPoint()
        {
            return endpointSetting.Value;
        }
    }
}