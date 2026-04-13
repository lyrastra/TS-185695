using System;
using Moedelo.Common.Enums.Enums.BankPartners;

namespace Moedelo.HomeV2.Dto.BankRequest
{
    /// <summary>
    /// Параметры получения списка отправленных заявок на расчётно-кассовое обслуживание
    /// </summary>
    public class GetSavedBankRequestsDto
    {
        /// <summary>
        /// по какому банку-партнёру
        /// </summary>
        public BankPartners Bank { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Дата конца периода
        /// </summary>
        public string EndDate { get; set; }
    }
}