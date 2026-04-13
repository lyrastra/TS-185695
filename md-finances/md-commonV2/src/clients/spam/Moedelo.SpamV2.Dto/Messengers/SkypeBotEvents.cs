namespace Moedelo.SpamV2.Dto.Messengers
{
    public struct SkypeBotEvent
    {
        /// <summary>
        /// Отчет о работе консоли авто-завершения мастеров
        /// </summary>
        public const string ReportsAutoComplete = "master_auto_complete";

        /// <summary>
        /// Ошибка МЗМ, встроенного в Авансы/Декларацию УСН (для ИП)
        /// </summary>
        public const string MzmIpError = "mzm_ip_error";

        /// <summary>
        /// Отправка писем пользователю о напоминании отправить отчеты
        /// </summary>
        public const string UnsentEReportsNotificator = "unsent_ereports_notificator";
    }
}