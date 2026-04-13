using System;

namespace Moedelo.CashV2.Dto.Evotor.Partner
{
    public class IntegrationStatusDto
    {
        /// <summary>
        /// Дата включения интеграции, NULL если интеграция никогда не включалась
        /// </summary>
        public DateTime? IntegrationDate { get; set; }

        /// <summary>
        /// Показывает, активна ли интеграция
        /// </summary>
        public bool IsActive { get; set; }
    }
}