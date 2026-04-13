namespace Moedelo.CommissionAgents.Client.Dto
{
    /// <summary>
    /// Модели операций с комиссионерами из контрагента
    /// </summary>
    public static class FromKontragent
    {
        /// <summary>
        /// Запрос на создание
        /// </summary>
        public class CreateRequestDto
        {
            public int KontragentId { get; set; }
            public string Inn { get; set; }
            public string KontragentName { get; set; }
        }

        /// <summary>
        /// Результат создания
        /// </summary>
        public class CreateResultDto
        {
            public bool IsSuccess { get; set; }
            public int Status { get; set; }
            
            public int CommissionAgentId { get; set; }
            public long StockId { get; set; }
        }
        
        /// <summary>
        /// Запрос на удаление
        /// </summary>
        public class DeleteRequestDto
        {
            public int KontragentId { get; set; }
        }

        /// <summary>
        /// Результат удаления
        /// </summary>
        public class DeleteResultDto
        {
            public bool IsSuccess { get; set; }
            public string ValidateError { get; set; }
        }
    }
}