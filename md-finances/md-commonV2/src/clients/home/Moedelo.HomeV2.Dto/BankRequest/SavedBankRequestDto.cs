using System;
using Moedelo.Common.Enums.Enums.BankPartners;

namespace Moedelo.HomeV2.Dto.BankRequest
{
    public class SavedBankRequestDto
    {
        /// <summary>
        /// Ф.И.О. в основном, иногда Имя
        /// </summary>
        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Передача кода источника лида
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Время отправки запроса
        /// </summary>
        public DateTime RequestDateTime { get; set; }

        public BankPartners BankPartner { get; set; }

        /// <summary>
        /// Другой банк, предложенный пользователем
        /// </summary>
        public string ProposeBankPartner { get; set; }

        /// <summary> Инн организации </summary>
        public string Inn { get; set; }

        /// <summary>Дата регистрации бизнеса </summary>
        public DateTime? DateOfRegBusiness { get; set; }

        /// <summary>
        /// Статус заявки на РКО в crm банка
        /// </summary>
        public string BankCrmStatus { get; set; }

        /// <summary>
        /// Код валюты из заявки
        /// </summary>
        public int? CurrencyCode { get; set; }
    }
}