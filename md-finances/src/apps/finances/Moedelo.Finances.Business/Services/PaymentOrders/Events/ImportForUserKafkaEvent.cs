using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Finances.Business.Services.PaymentOrders.Events
{
    public class ImportForUserKafkaEvent : IEntityEventData
    {
        public int FirmId { get; set; }
        public string FileId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        
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


