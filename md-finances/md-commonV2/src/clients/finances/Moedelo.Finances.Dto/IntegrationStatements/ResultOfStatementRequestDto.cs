using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Dto.IntegrationStatements
{
    /// <summary>
    /// Результат запроса выписки по р/сч
    /// </summary>
    public class ResultOfStatementRequestDto
    {
        /// <summary>
        /// Р/сч
        /// </summary>
        public int SettlementAccountId { get; set; }
        
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