using System;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy
{
    public class FinancialOperationDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Des { get; set; }
        public string Number { get; set; }
        public DateTime OperationDate { get; set; }
        public long? DocumentBaseId { get; set; }
        public int? Order { get; set; }
        public int? ParentFinancialOperationId { get; set; }
        public virtual string Name => string.Empty;
        public FinancialOperationDirection Direction { get; set; }
    }
}
