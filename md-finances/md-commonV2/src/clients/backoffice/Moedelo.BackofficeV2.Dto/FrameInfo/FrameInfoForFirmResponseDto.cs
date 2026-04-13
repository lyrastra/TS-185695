using System;
using System.Collections.Generic;

namespace Moedelo.BackofficeV2.Dto.FrameInfo
{
    /// <summary> Информация о фирме для фрейма </summary>
    public class FrameInfoForFirmResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Организационно-правовая форма </summary>
        public string Opf { get; set; }

        /// <summary> Индивидуальный номер налогоплательщика </summary>
        public string Inn { get; set; }

        /// <summary> Главный пользователь </summary>
        public FrameInfoUserResponseDto LegalUser { get; set; }

        /// <summary> Телефон из реквизитов </summary>
        public string PhoneFromRequisites { get; set; }

        /// <summary> Телефон из электронной отчетности </summary>
        public string PhoneFromElectronicReport { get; set; }

        /// <summary> ФИО юридического лица </summary>
        public string JuridicalFio { get; set; }

        /// <summary> ФИО профессионального аутсорсера </summary>
        public string ProfessionalOutsourcerFio { get; set; }

        /// <summary> Дата государственной регистрации </summary>
        public DateTime? StateRegistrationDate { get; set; }

        /// <summary> Список пользователей </summary>
        public List<FrameInfoUserResponseDto> Users { get; set; }

        /// <summary> Статус текущей оплаты </summary>
        public string CurrentPaidStatus { get; set; }

        /// <summary> Дата начала текущей оплаты </summary>
        public DateTime? CurrentPaymentStartDate { get; set; }

        /// <summary> Дата окончания текущей оплаты </summary>
        public DateTime? CurrentPaymentEndDate { get; set; }

        /// <summary> Продукт текущей оплаты </summary>
        public string CurrentPaymentProduct { get; set; }

        /// <summary> Статус покупки разовых услуг </summary>
        public bool HasPurchasedOneTimeServices { get; set; }

        /// <summary> История платежей </summary>
        public List<FrameInfoPayHistoryResponseDto> PayHistory { get; set; }

        /// <summary> Расчетные счета </summary>
        public List<FrameInfoSettlementAccountResponseDto> SettlementAccounts { get; set; }

        /// <summary> Банки с интеграцией </summary>
        public List<FrameInfoIntegratedBankResponseDto> IntegratedBanks { get; set; }

        /// <summary> Статус отчетности в ФНС </summary>
        public string FnsReportStatus { get; set; }

        /// <summary> Статус отчтности в ПФР и ФСС </summary>
        public string PfrAndFssReportStatus { get; set; }

        /// <summary> Статус отчетности в Росстат </summary>
        public string RosstatReportStatus { get; set; }

        /// <summary> Статус электронной подписи </summary>
        public string DigitalSignatureStatus { get; set; }
    }
}