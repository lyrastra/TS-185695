using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Operations.Duplicates
{
    public class OperationDuplicate
    {
        public long Id { get; set; }
        public long DocumentBaseId { get; set; }
        public string Number { get; set; }
        public MoneyDirection Direction { get; set; }
        public string Description { get; set; }
        // Р/с Плательщика
        public int? SettlementAccountId { get; set; }
        // Р/с Получателя
        public string RecipientSettlementNumber { get; set; }
        // Р/с плательщика
        public string PayerSettlementNumber { get; set; }
        public string RecipientInn { get; set; }
        public string PayerInn { get; set; }
        public int? KontragentId { get; set; }
        public bool IdenticalKontragent { get; set; } = true;
        public bool IsSalaryOperation { get; set; }
    }
}