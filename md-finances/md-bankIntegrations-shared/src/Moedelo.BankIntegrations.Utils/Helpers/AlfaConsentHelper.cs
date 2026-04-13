using System;

namespace Moedelo.BankIntegrations.Utils.Helpers
{
    public static class AlfaConsentHelper
    {
        /// <summary>
        /// Дата, включительно с которой согласие пользователя действует 3 года
        /// </summary>
        private static readonly DateTime NewConsentDate = new DateTime(2025, 04, 22);

        /// <summary>
        /// Получить дату истечения согласия
        /// </summary>
        /// <param name="enableDate">Дата включения интеграции</param>
        /// <returns></returns>
        public static DateTime GetConsentExpireDate(DateTime enableDate)
        {
            // До 22.04.25 согласия действовали год
            return enableDate.Date >= NewConsentDate
                ? enableDate.AddYears(3)
                : enableDate.AddYears(1);
        }
    }
}