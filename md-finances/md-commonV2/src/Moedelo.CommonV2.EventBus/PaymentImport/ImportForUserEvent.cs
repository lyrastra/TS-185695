using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.CommonV2.EventBus.PaymentImport
{
    public class ImportForUserEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FileId { get; set; }
        
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