using Moedelo.Finances.Domain.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.SettlementAccounts
{
    public static class SettlementAccountsMapper
    {
        public static SettlementAccount Map(this SettlementAccountDto settlementAccountsDto)
        {
            if (settlementAccountsDto == null)
                return null;

            return new SettlementAccount(
                settlementAccountsDto.Id,
                settlementAccountsDto.Name,
                settlementAccountsDto.Number,
                settlementAccountsDto.TransitNumber,
                settlementAccountsDto.BankId,
                settlementAccountsDto.Type,
                settlementAccountsDto.Currency,
                settlementAccountsDto.IsPrimary,
                settlementAccountsDto.IsDeleted,
                settlementAccountsDto.SubcontoId,
                settlementAccountsDto.LinkId);
        }
    }
}
