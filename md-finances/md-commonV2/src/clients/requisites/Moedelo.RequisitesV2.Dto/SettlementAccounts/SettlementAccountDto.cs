using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.RequisitesV2.Dto.SettlementAccounts
{
    public class SettlementAccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        
        public string TransitNumber { get; set; }
        public int BankId { get; set; }
        public SettlementAccountType Type { get; set; }
        public Currency Currency { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsDeleted { get; set; }
        public long? SubcontoId { get; set; }
        
        public int? LinkId { get; set; }
    }
}