using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Domain.SettlementAccounts
{
    public class SettlementAccount
    {
        public SettlementAccount(
            int id, 
            string name, 
            string number, 
            string transitNumber, 
            int bankId, 
            SettlementAccountType type, 
            Currency currency, 
            bool isPrimary, 
            bool isDeleted, 
            long? subcontoId, 
            int? linkId
            )
        {
            Id = id;
            Name = name;
            Number = number;
            TransitNumber = transitNumber;
            BankId = bankId;
            Type = type;
            Currency = currency;
            IsPrimary = isPrimary;
            IsDeleted = isDeleted;
            SubcontoId = subcontoId;
            LinkId = linkId;
        }

        public int Id { get; }
        public string Name { get; }
        public string Number { get; }
        public string TransitNumber { get; }
        public int BankId { get; }
        public SettlementAccountType Type { get; }
        public Currency Currency { get; }
        public bool IsPrimary { get; }
        public bool IsDeleted { get; }
        public long? SubcontoId { get; }
        public int? LinkId { get; }
    }
}
