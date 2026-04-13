using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.HomeV2.Client.Phone.Models;
using Moedelo.HomeV2.Dto.Phone;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.Phone
{
    [InjectAsSingleton(typeof(IPhoneV2Client))]
    public sealed class PhoneV2Client : BaseApiClient, IPhoneV2Client
    {
        private readonly SettingValue apiEndPoint;

        public PhoneV2Client(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
                : base(
                    httpRequestExecutor,
                    uriCreator,
                    responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        public Task<PhoneDto> Get(
            int firmId,
            int userId,
            PhoneTypes type,
            CancellationToken cancellationToken)
        {
            return GetAsync<PhoneDto>($"/GetPhoneByTypeV2", new { firmId, userId, type }, cancellationToken: cancellationToken);
        }

        public Task<List<PhoneDto>> Get(
            IReadOnlyCollection<int> firmIds,
            IReadOnlyCollection<PhoneTypes> types,
            CancellationToken cancellationToken)
        {
            if (firmIds?.Any() != true || types?.Any() != true)
            {
                return Task.FromResult(new List<PhoneDto>());
            }

            var requestDto = new FirmsPhonesRequestDto
            {
                FirmIds = firmIds,
                OnlyTypes = types,
            };

            return PostAsync<FirmsPhonesRequestDto, List<PhoneDto>>("/GetFirmPhones", requestDto, cancellationToken: cancellationToken);
        }

        public Task UpdatePhone(
            int firmId,
            int userId,
            PhoneDto dto,
            CancellationToken cancellationToken)
        {
            return PostAsync($"/UpdatePhone?firmId={firmId}&userId={userId}", dto, cancellationToken: cancellationToken);
        }

        public Task<bool> AvailableRegistrationForBankPartnerAsync(
            string phone,
            CancellationToken cancellationToken)
        {
            return GetAsync<bool>($"/AvailableRegistrationForBankPartner?phone={phone}", cancellationToken: cancellationToken);
        }

        public Task<List<PhoneDto>> Get(
            string phone,
            PhoneTypes type,
            CancellationToken cancellationToken)
        {
            var uri = $"/Find?phone={phone}&type={type}";

            return GetAsync<List<PhoneDto>>(uri, cancellationToken: cancellationToken);
        }

        public Task<PhoneSearchByFilterResponseDto> SearchByFilterAsync(
            PhoneSearchByFilterRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            return PostAsync<PhoneSearchByFilterRequestDto, PhoneSearchByFilterResponseDto>(
                "/SearchByFilter", requestDto,
                cancellationToken: cancellationToken);
        }

        public Task<string> CreateFakePhoneAsync(
            FakePhoneCreateRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            return PostAsync<FakePhoneCreateRequestDto, string>(
                $"/CreateFakePhone", requestDto,
                cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/Phones";
        }
    }
}