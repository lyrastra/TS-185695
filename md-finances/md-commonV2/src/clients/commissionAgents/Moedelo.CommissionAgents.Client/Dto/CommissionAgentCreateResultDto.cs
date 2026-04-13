namespace Moedelo.CommissionAgents.Client.Dto
{
    public class CommissionAgentCreateResultDto
    {
        /// <summary>
        /// Код ≥ 0: комиссионер создан.
        /// Код &lt; 0: комиссионер не создан.
        /// 
        /// Набор значений: https://github.com/moedelo/md-commissionAgents/blob/f1578a937d4b1e89d43924e03b7f254b8396e288/src/apps/Moedelo.CommissionAgents.Business.Abstractions/Enums/CommissionAgentCreationStatus.cs#L3
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// Модель комиссионера (если не создан - null)
        /// </summary>
        public CommissionAgentDto Value { get; set; }
    }
}