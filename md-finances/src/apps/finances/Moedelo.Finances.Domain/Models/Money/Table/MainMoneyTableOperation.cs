using System;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MainMoneyTableOperation : MoneyTableOperation
    {
        public decimal StartBalance { get; set; }
        
        public decimal EndBalance { get; set; }

        public int IncomingCount { get; set; }
        
        public decimal IncomingBalance { get; set; }
        
        public DateTime IncomingDate { get; set; }

        public int OutgoingCount { get; set; }
        
        public decimal OutgoingBalance { get; set; }
        
        public DateTime OutgoingDate { get; set; }

        public bool HasOperations { get; set; }

        /// <summary>
        /// Тип контрагента в поле KontragentId (контрагент/сотрудник/неопределено)
        /// </summary>
        public MoneyContractorType KontragentType { get; set; }

        public int ClosingDocumentsCount { get; set; }
        
        public bool IsSummary { get; set; }
    }
}