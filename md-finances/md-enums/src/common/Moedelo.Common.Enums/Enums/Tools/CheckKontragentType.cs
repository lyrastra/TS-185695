namespace Moedelo.Common.Enums.Enums.Tools
{
    /// <summary>
    /// Типы сервисов которые проверяют контрагента
    /// </summary>
    public enum CheckKontragentType
    {
        /// <summary>
        /// Плановая проверка контрагента
        /// </summary>
        GenProc = 1,

        /// <summary>
        /// Арбитражные дела (суды) контрагента
        /// </summary>
        Arbitration = 2,

        /// <summary>
        /// Проверка статуса активности контрагента по фнс
        /// </summary>
        Fns = 3,

        /// <summary>
        /// Получить выписку для юр. лица
        /// </summary>
        FnsUlExcerpt = 4,

        /// <summary>
        /// Получить выписку для ИП
        /// </summary>
        FnsIpExcerpt = 5,

        /// <summary>
        /// Проверка присутствия в реестрах
        /// </summary>
        AllRegistry = 6,

        /// <summary>
        /// Блок «Финансы» в карточке организации
        /// </summary>
        FinanceReport = 7,

        /// <summary>
        /// Проверка участия в гос. закупках
        /// </summary>
        AllZakupki = 8,

        /// <summary>
        /// Проверка подачи документов на изменение ЕГРЮЛ
        /// </summary>
        ChangeEgrul = 9,

        /// <summary>
        /// Проверка по реестру массовых руководителей/учредителей
        /// </summary>
        MassDirectorsAndFounders = 10,

        /// <summary>
        /// Информация о связях
        /// </summary>
        RelationsInfo = 11,

        /// <summary>
        /// Госконтракты
        /// </summary>
        StateContracts = 12,

        /// <summary>
        /// Росстат
        /// </summary>
        Rosstat = 13,

        /// <summary>
        /// Банкротства и исполнительные производства
        /// </summary>
        OrganizationMessages = 14,

        /// <summary>
        /// Реестр террористов и экстримистов
        /// </summary>
        Terrorist = 15,

        /// <summary>
        /// Единый реестр субъектов малого и среднего предпринимательства
        /// </summary>
        Msp = 16,

        /// <summary>
        /// Лицензии
        /// </summary>
        License = 17,

        /// <summary>
        /// Арбитражные дела контрагента только банкротного типа
        /// </summary>
        ArbitrationBancruptcy = 18,
    }
}