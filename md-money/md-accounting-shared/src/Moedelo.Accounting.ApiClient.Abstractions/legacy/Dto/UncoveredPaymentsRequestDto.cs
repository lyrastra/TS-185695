using System;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class UncoveredPaymentsRequestDto
    {
        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// В случае "пустого" реестра: добавлять к названию файла префикс 
        /// </summary>
        public string NoUncoveredFileNamePrefix { get; set; }
    }
}