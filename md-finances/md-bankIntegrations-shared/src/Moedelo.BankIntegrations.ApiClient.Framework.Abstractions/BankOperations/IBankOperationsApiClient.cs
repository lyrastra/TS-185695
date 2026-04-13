using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations
{
    public interface IBankOperationsApiClient
    {
        Task<IntegrationResponseDto<SendBankInvoiceResponseDto>> SendInvoiceAsync(int firmId, int userid, SendBankInvoiceRequestDto dto);

        Task<IntegrationResponseDto<List<InvoiceDetailResponseDto>>> GetListInvoiceDetailByDocumentBaseIdsAsync(int firmId, int userid, IReadOnlyCollection<long> documentBaseIds);

        Task<IntegrationResponseDto<bool>> GetHaveInvoiceAccessAsync(int userid, int firmId, int partner);
        
        Task<GetAccountsResponseDto> GetAccountsAsync(int userId, int firmId, IntegrationPartners partner);

        Task RequestMovementsAfterTurnIntegrationByFirmAsync(RequestMovementForAllSettlementsDto dto);
    }
}