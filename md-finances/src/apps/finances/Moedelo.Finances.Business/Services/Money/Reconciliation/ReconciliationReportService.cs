using Moedelo.BankIntegrationsV2.Client.BankOperation;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationReportService : IReconciliationRequestReportService
    {
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBanksApiClient banksApiClient;
        private readonly IBankIntegrationsDataInformationClient dataInformationClient;
        private readonly IBankOperationClient bankOperationClient;

        public ReconciliationReportService(
            ISettlementAccountClient settlementAccountClient,
            IBankIntegrationsDataInformationClient dataInformationClient,
            IBankOperationClient bankOperationClient,
            IBanksApiClient banksApiClient)
        {
            this.settlementAccountClient = settlementAccountClient;
            this.dataInformationClient = dataInformationClient;
            this.bankOperationClient = bankOperationClient;
            this.banksApiClient = banksApiClient;
        }

        public async Task<bool> RequestReportAsync(IUserContext userContext, ReconciliationReport request)
        {
            var settlementAccount = await settlementAccountClient.GetByIdAsync(userContext.FirmId, userContext.UserId, request.SettlementAccountId).ConfigureAwait(false);
            var bank = (await banksApiClient.GetByIdsAsync(new List<int> { settlementAccount.BankId }).ConfigureAwait(false)).First();

            var requestIntSummaryDto = new IntSummaryBySettlementsRequestDto()
            {
                UserId = userContext.UserId,
                FirmId = userContext.FirmId,
                Settlements = new List<SettlementAccountV2Dto>
                {
                    new SettlementAccountV2Dto
                    {
                        Bik = bank.Bik,
                        SettlementNumber = settlementAccount.Number,
                        BankFullName = bank.FullName
                    }
                }
            };

            var intSummary = await dataInformationClient.GetIntSummaryBySettlementsAsync(requestIntSummaryDto).ConfigureAwait(false);
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);

            var requestMovementDto = new RequestMovementListRequestDto()
            {
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                IsAccounting = false,
                IsManual = false,
                IdentityDto = new IntegrationIdentityDto
                {
                    FirmId = userContext.FirmId,
                    Bik = bank.Bik,
                    Inn = contextExtraData.Inn,
                    IntegrationPartner = intSummary.Result.First().IntegrationPartner,
                    SettlementNumber = settlementAccount.Number
                },
                Email = request.Email,
                CallType = IntegrationCallType.ReviseForBackoffice
            };

            var response = await bankOperationClient.RequestMovementListAsync(requestMovementDto).ConfigureAwait(false);

            return response?.IsSuccess ?? false;
        }
    }

}
