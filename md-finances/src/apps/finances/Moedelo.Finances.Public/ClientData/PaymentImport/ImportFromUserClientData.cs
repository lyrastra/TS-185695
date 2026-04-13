using System.ComponentModel.DataAnnotations;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Public.ClientData.PaymentImport
{
    public class ImportFromUserClientData
    {
        [Required]
        public string FileId { get; set; }

        public bool CheckDocuments { get; set; }
        
        /// <summary>
        /// Второй расчетный счет, для создания валютного счета
        /// </summary> 
        public string SecondSettlementAccount { get; set; }
        
        public SettlementAccountType SettlementAccountType { get; set; }
    }
}