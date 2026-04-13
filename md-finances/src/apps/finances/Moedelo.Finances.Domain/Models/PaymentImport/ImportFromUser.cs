using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Domain.Models.PaymentImport
{
    public class ImportFromUser
    {
        public string FileId { get; set; }

        public bool CheckDocuments { get; set; }
        
        public bool CheckSettlementAccount { get; set; }
        
        /// <summary>
        /// Второй расчетный счет, для создания валютного счета
        /// </summary> 
        public string SecondSettlementAccount { get; set; }
        
        public SettlementAccountType SettlementAccountType { get; set; }
    }
}