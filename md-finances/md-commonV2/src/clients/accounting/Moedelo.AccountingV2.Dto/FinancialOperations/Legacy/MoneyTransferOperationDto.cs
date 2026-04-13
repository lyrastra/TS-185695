using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy
{
    public class MoneyTransferOperationDto : FinancialOperationDto
    {
        public decimal Sum { get; set; }
        public int? ProjectId { get; set; }
        public int? KontragentId { get; set; }
        public MoneyBayType MoneyBayType { get; set; }
        public WriteOff WriteOffBy { get; set; }
        public decimal UsnSum { get; set; }
        public decimal EnvdSum { get; set; }
        public string DestanitionDescription { get; set; }
        public string NumberOfDocument { get; set; }
        public int? SettlementAccountId { get; set; }
        public int? PurseId { get; set; }
    }
}
