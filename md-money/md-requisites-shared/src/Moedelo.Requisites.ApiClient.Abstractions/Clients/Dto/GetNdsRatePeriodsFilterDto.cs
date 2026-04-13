using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto
{
    /// <summary>
    /// Фильтр для получения настроек НДС в учётной политике
    /// </summary>
    public class GetNdsRatePeriodsFilterDto
    {
        /// <summary>
        /// Фильтр настроек на конкретную дату
        /// </summary>
        public DateTime? OnDate { get; set; }
    }
}
