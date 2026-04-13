using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings
{
    public class AccPostingDto
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Код счета по дебету
        /// </summary>
        public int DebitCode { get; set; }

        /// <summary>
        /// Номер счета по дебету
        /// </summary>
        public string DebitNumber { get; set; }

        /// <summary>
        /// Код счета по кредиту
        /// </summary>
        public int CreditCode { get; set; }

        /// <summary>
        /// Номер счета по кредиту
        /// </summary>
        public string CreditNumber { get; set; }

        /// <summary>
        /// Список дебетовых субконто
        /// </summary>
        public IReadOnlyCollection<SubcontoDto> DebitSubconto { get; set; }

        /// <summary>
        /// Список кредитовых субконто
        /// </summary>
        public IReadOnlyCollection<SubcontoDto> CreditSubconto { get; set; }

        /// <summary>
        /// Описание проводки
        /// </summary>
        public string Description { get; set; }
    }
}
