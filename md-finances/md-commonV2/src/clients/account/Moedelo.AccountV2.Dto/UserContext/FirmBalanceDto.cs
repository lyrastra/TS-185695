using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.AccountV2.Dto.UserContext
{
    public class FirmBalanceDto
    {
        public int Id { get; set; }

        public int? FirmId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public double? Value { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsPrimary { get; set; }

        public Currency? Currency { get; set; }

        public SettlementAccountType? SettlementAccountType { get; set; }
    }
}