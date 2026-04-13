using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.ErptV2.Dto.EreportSettings;

namespace Moedelo.ErptV2.Client.EReportSettings
{
    public interface IEReportSettingsApiClient
    {
        Task<SendingSettings> GetSendgingSettings(int firmId, int userId);
        Task<bool> CanSend(int firmId, int userId, FundType fund, string code, string route, CancellationToken cancellationToken = default);
        Task<ProviderRegionInfoDto> GetProviderRegionInfo(int firmId);
        Task<List<SendingDirection>> GetSendingDirections(int firmId, int userId);
    }
}