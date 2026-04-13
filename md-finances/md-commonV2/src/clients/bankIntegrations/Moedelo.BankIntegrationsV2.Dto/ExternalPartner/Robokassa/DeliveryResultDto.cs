namespace Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa
{
    public enum DeliveryResultDto
    {
        /// <summary> Ошибка при доставке </summary>
        Error = -1,

        /// <summary> Адресат не найден, либо интеграция не включена </summary>
        Missing = 0,

        /// <summary> Успешная доставка </summary>
        Success = 1,

        /// <summary> Партнёр заблокирован со стороны Робокассы </summary>
        PartnerBlocked = 2,

        /// <summary> Не ассоциирован р/с в ОкеанБанке с этим пользователем </summary>
        IncorrectCurrentAccount = 3,

        /// <summary> Не является клиентом ОкеанБанка </summary>
        NoOceanBankClient = 4,

        /// <summary> Некорректный интервал дат при запросе выписки </summary>
        IncorrectDatePeriod = 5
    }
}
