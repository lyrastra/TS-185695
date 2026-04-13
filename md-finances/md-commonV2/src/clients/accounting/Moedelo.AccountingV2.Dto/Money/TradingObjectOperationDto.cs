using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class TradingObjectOperationDto
    {
        public int TradingObjectId { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime PeriodDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Sum { get; set; }

        public string Oktmo { get; set; }

        public string Kbk { get; set; }

        public string Description { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        
        // Информация о реквизитах налоговой
        public string TaxName { get; set; }

        public string Inn { get; set; }

        public string SettlementAccount { get; set; }

        public string UnifiedSettlementAccount { get; set; }

        public string Bik { get; set; }

        public string Kpp { get; set; }

        public bool IsUsn15 { get; set; }
    }
}
