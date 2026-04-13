using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.ClientInfo;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Request;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.StatementSummary;
using System.Threading.Tasks;

namespace Moedelo.SberbankCryptoEndpointV2.Client.Upg2
{
    public interface IUpg2Client : IDI
    {
        Task<RequestMovementListResponseDto> RequestMovementListAsync(RequestMovementListRequestDto dto, HttpQuerySetting setting);

        Task<GetSberbankSettlementsToSsoResponseDto> GetSberbankSettlementsToSsoAsync(GetSberbankSettlementsToSsoRequestDto dto);

        Task<GetSberbankPaymentsStatusResponseDto> GetSberbankPaymentsStatusAsync(GetSberbankPaymentsStatusRequestDto dto);

        Task<GetStatementSummaryResponseDto> GetStatementSummaryAsync(GetStatementSummaryRequestDto dto);

        Task<ClientInfoResponseDto> GetClientInfoAsync(int firmId);
    }
}