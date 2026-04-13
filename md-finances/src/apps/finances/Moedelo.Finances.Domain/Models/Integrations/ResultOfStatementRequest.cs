using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    /// <summary>
    /// Результат запроса выписки по р/сч
    /// </summary>
    public class ResultOfStatementRequest
    {
        /// <summary>
        /// Идентификатор р/сч
        /// </summary>
        public int SettlementAccountId { get; set; }
        
        /// <summary>
        /// Номер р/сч
        /// </summary>
        public string SettlementAccountNumber { get; set; }
        
        /// <summary>
        /// Причина, по которой запрос выписки невозможен
        /// </summary>
        public StatementRequestBlockedReason? BlockedReason { get; set; }

        /// <summary>
        /// Успешно ли запрошена выписка
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Сообщение об ошибке (в случае неуспешного запроса выписки) 
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Id запроса выписки
        /// </summary>
        public string RequestId { get; set; }
    }
}