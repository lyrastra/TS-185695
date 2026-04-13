using System;
using System.Collections.Generic;

namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// Информация о непрочитанных ДО
    /// </summary>
    public class UnreadDocflowsInfoDto
    {
        /// <summary>Кол-во непрочитанных ДО по текущему роумингу</summary>
        public int UnreadCount { get; set; }

        /// <summary>
        /// Cписок доступных контрагентов с текущим ИНН, находящихся в адресной книге для переключения настроенного роминга
        /// </summary>
        public List<AvailableKontragentDto> AvailableKontragents { get; set; }

        /// <summary>
        /// Дата истечения лицензии
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// Дата истечения подписи
        /// </summary>
        public DateTime SignatureExpiryDate { get; set; }

        /// <summary>
        /// Решения
        /// </summary>
        public List<UnreadDocflowDecisionDto> Decisions { get; set; }


    }
}
