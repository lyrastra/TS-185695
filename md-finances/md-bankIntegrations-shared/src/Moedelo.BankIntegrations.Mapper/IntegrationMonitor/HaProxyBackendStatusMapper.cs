using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class HaProxyBackendStatusMapper
    {
        public static HaProxyBackendStatusModel Map(this HaProxyBackendStatusDto dto)
        {
            return new HaProxyBackendStatusModel
            {
                HaProxyHost = dto.HaProxyHost,
                BackendName = dto.BackendName,
                ServerName = dto.ServerName,
                Status = dto.Status,
                ChecksFailed = dto.ChecksFailed,
                DownTransitions = dto.DownTransitions,
                LastChangeSeconds = dto.LastChangeSeconds
            };
        }

        public static HaProxyBackendStatusDto Map(this HaProxyBackendStatusModel model)
        {
            return new HaProxyBackendStatusDto
            {
                HaProxyHost = model.HaProxyHost,
                BackendName = model.BackendName,
                ServerName = model.ServerName,
                Status = model.Status,
                ChecksFailed = model.ChecksFailed,
                DownTransitions = model.DownTransitions,
                LastChangeSeconds = model.LastChangeSeconds
            };
        }

        public static List<HaProxyBackendStatusModel> Map(this List<HaProxyBackendStatusDto> list)
        {
            return list.Select(dto => dto.Map()).ToList();
        }

        public static List<HaProxyBackendStatusDto> Map(this List<HaProxyBackendStatusModel> list)
        {
            return list.Select(model => model.Map()).ToList();
        }
    }
}
