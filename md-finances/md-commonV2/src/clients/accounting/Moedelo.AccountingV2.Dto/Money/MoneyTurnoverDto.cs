using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class MoneyTurnoverDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string OperationName { get; set; }
        public decimal Sum { get; set; }
        public string Type { get; set; }
        public int DocumentBaseId { get; set; }
        public AccountingDocumentType DocumentType { get; set; }
        public int? ContractId { get; set; }
        public int? KontragentId { get; set; }
        public int? SettlementAccountId { get; set; }
        public int Direction { get; set; }
        public int? OperationType { get; set; }
    }
}
