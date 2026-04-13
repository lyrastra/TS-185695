using System;
using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.FrameInfo
{
    /// <summary> Информация о базовых данных о фирме для фрейма </summary>
    public class AccountFrameInfoForFirmResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Организационно-правовая форма </summary>
        public string Opf { get; set; }

        /// <summary> Индивидуальный номер налогоплательщика </summary>
        public string Inn { get; set; }

        /// <summary> Главный пользователь </summary>
        public AccountFrameInfoForUserResponseDto LegalUser { get; set; }

        /// <summary> Телефон из реквизитов </summary>
        public string PhoneFromRequisites { get; set; }

        /// <summary> Телефон из электронной отчетности </summary>
        public string PhoneFromElectronicReport { get; set; }

        /// <summary> ФИО юридического лица </summary>
        public string JuridicalFio { get; set; }

        /// <summary> Название профессионального аутсорсера </summary>
        public string ProfessionalOutsourcer { get; set; }

        /// <summary> Дата государственной регистрации </summary>
        public DateTime? StateRegistrationDate { get; set; }

        /// <summary> Список пользователей </summary>
        public List<AccountFrameInfoForUserResponseDto> Users { get; set; }
    }
}