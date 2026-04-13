using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.Business.SettlementAccounts;
using Moedelo.Finances.Domain.SettlementAccounts;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.RequisitesV2.Client.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.SettlementAccounts
{
    [InjectAsSingleton]
    public class SettlementAccountsReader : ISettlementAccountsReader
    {
        private readonly ISettlementAccountClient settlementAccountClient;

        public SettlementAccountsReader(
            ISettlementAccountClient settlementAccountClient)
        {
            this.settlementAccountClient = settlementAccountClient;
        }

        public async Task<SettlementAccount> GetSettlementAccount(int firmId, int userId, int Id)
        {
            var settlementAccount = await settlementAccountClient.GetByIdAsync(firmId, userId, Id).ConfigureAwait(false);
            return settlementAccount.Map();
        }

        public async Task<List<SettlementAccount>> GetSettlementAccounts(int firmId, int userId, List<long> subcontoIds, long? sourceId)
        { 
            var settlementAccountsDto = await settlementAccountClient.GetBySubcontoIdsAsync(firmId, userId, subcontoIds)
              .ConfigureAwait(false);

           var settlementAccounts = settlementAccountsDto.Select(SettlementAccountsMapper.Map).ToList();

           //Убираем дубли остатков в транзитных счетах, если они введены только по валютным 
            if (settlementAccounts.Count != 0 && sourceId != 0)
            {
                settlementAccounts.RemoveAll(x => x.Id != sourceId);
            }

            return settlementAccounts;
        }
    }
}
