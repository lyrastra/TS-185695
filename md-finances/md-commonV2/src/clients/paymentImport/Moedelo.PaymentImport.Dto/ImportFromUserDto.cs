using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.PaymentImport.Dto
{
    public class ImportFromUserDto
    {
        public string FileId { get; set; }

        public bool CheckDocuments { get; set; }
        
        public bool CheckSettlementAccount { get; set; }
        
        /// <summary>
        /// Второй расчетный счет, для создания валютного счета
        /// </summary> 
        public string SecondSettlementAccount { get; set; }
        
        /// <summary>
        /// Тип счета, указанного в выписке
        /// </summary>
        public SettlementAccountType SettlementAccountType { get; set; }
    }
}