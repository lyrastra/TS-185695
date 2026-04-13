using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.AlfabankSso
{
    /// <summary> Идентификационная информация Клиента АльфаБанк </summary>
    public class AlfabankClientInfoDto
    {
        /// <summary> ИНН Клиента </summary>
        public string Inn { get; set; }

        /// <summary> Телефон. Используется для реквизитов и документов, а также в электронной отчетности. </summary>
        public string Phone { get; set; }

        /// <summary> E-mail. Уведомления, логин при авторизации </summary>
        public string Email { get; set; }

        /// <summary> Система налогообложения Клиента  УСН / УСН + ЕНВД / ЕНВД / ОСНО и т.д. </summary>
        public AlfabankSnoEnum Sno { get; set; }

        /// <summary> Наименование для документов </summary>
        public string NameForDocuments { get; set; }

        /// <summary> ОКПО </summary>
        public string Okpo { get; set; }

        /// <summary> счета клиента </summary>
        public List<string> Accounts { get; set; }
    }
}