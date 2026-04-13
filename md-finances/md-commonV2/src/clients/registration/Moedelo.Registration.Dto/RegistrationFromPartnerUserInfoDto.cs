using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.Registration.Dto
{
    public class RegistrationFromPartnerUserInfoDto
    {
        public string SourceName { get; set; }
        
        public string UtmSourceExtension { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public TariffForPartner Tariff { get; set; }

        public string Fio { get; set; }

        public string TrialCode { get; set; }

        public string Inn { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public string BankForOpenAccount { get; set; }

        public string CurrentBankName { get; set; }

        public OwnershipType OwnershipType { get; set; }

        public RegistrationVerificationMethod? RegistrationVerificationMethod { get; set; }

        /// <summary>
        /// Идентификатор истории смс из верификации регистрации
        /// </summary>
        public int? RegistrationVerificationSmsHistoryId { get; set; }

        /// <summary>
        /// Guid верификации телефона регистрации
        /// </summary>
        public Guid? RegistrationPhoneVerificationGuid { get; set; }

        /// <summary>
        /// Откуда происходит регистрация
        /// </summary>
        public string RegistrationSource { get; set; }

        /// <summary>
        /// Поля заполненные пользователем
        /// </summary>
        public HashSet<RegistrationFieldType> UserFilledFields { get; set; }
    }
}