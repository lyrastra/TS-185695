using System;
using Moedelo.Common.Enums.Enums.BankPartners;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.HomeV2.Dto.BankRequest
{
    public class BankRequestDto
    {
        /// <summary>ID заявки</summary>
        public int RequestId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// День рождения ИП/директора ООО
        /// </summary>
        public DateTime? DirectorBirthday { get; set; }

        /// <summary>
        /// Код телефона
        /// </summary>
        public string PhoneCode { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Номер телефона вместе с кодом
        /// </summary>
        public string TelephoneFull { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        /// <summary>Код по кладру 2-х значный</summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Наименование предприятия
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Передача кода источника лида
        /// </summary>
        public string Code { get; set; }

        public BankPartners BankPartner { get; set; }

        /// <summary>
        /// Другой банк, предложенный пользователем
        /// Предложенное отделение банка в конкретном городе
        /// </summary>
        public string ProposeBankPartner { get; set; }

        public bool IsOoo { get; set; }

        /// <summary>
        /// УСН, ЕНВД, Общая и т.д.
        /// </summary>
        public string TaxationSystem { get; set; }

        public string Inn { get; set; }

        public string DateOfRegBusiness { get; set; }

        public bool IsOnlySaving { get; set; }

        public AnkorBankDto AnkorBankDto { get; set; }
        
        /// <summary>
        /// Тип валюты для счёта РКО может быть не передан,
        /// так как, на данный момент, валюта необходима только для не рублёвых счетов Tinkoff
        /// </summary>
        public Currency? CurrencyCode { get; set; }
    }
}