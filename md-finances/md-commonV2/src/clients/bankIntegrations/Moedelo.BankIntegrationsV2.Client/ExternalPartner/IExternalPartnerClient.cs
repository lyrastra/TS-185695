using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa;
using Moedelo.BankIntegrationsV2.Dto.ExternalPartner.RobokassaAcquirer;
using Moedelo.BankIntegrationsV2.Dto.IntegratedFile;

namespace Moedelo.BankIntegrationsV2.Client.ExternalPartner
{
    public interface IExternalPartnerClient
    {
        Task<IntegrationResponseDto<RobokassaTransferFilesResponseDto>> RobokassaTransferFiles(RobokassaTransferFilesRequestDto requestDto);

        Task<Dictionary<string, string>> GetRequisitesForFormByFirmId(int firmId);

        Task<IntegrationResponseDto<string>> RobokassaGetLinkToPayAsync(RobokassaGetLinkToPayRequestDto requestDto);
    }
}
