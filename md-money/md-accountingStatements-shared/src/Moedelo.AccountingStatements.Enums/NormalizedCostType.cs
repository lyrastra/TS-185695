namespace Moedelo.AccountingStatements.Enums
{
    public enum NormalizedCostType
    {
        None = 0,

        /// <summary>
        /// Расходы на возмещение затрат работников по уплате процентов по кредитам на приобретение или строительство жилого помещения
        /// </summary>
        CompensationForEmployeesLoans = 1,

        /// <summary>
        /// НИОКР
        /// </summary>
        ResearchAndDevelopment = 2,

        /// <summary>
        /// Представительские расходы
        /// </summary>
        EntertainmentCosts = 3,

        /// <summary>
        /// Расходы на рекламу
        /// </summary>
        Advertisement = 4,

        /// <summary>
        /// Расходы в виде процентов по долговым обязательствам
        /// </summary>
        InterestOnDebt = 5,

        /// <summary>
        /// Убыток от уступки права требования
        /// </summary>
        Cession = 6,

        /// <summary>
        /// Потери от недостачи и/или порчи при хранении и транспортировке МПЗ
        /// </summary>
        DamageDuringStorage = 7,

        /// <summary>
        /// Расходы по оплате стоимости проезда и провоза багажа работнику организации, 
        /// расположенной в районах Крайнего Севера и приравненных к ним местностям, и членам его семьи
        /// </summary>
        CompensationForTransportationFarNorth = 8,

        /// <summary>
        /// Расходы на обязательное страхование имущества
        /// </summary>
        RequiredPropertyInsurance = 9,

        /// <summary>
        /// Расходы на компенсацию за использование для служебных поездок личного транспорта
        /// </summary>
        CompensationForUsagePersonalTransport = 10,

        /// <summary>
        /// Плата государственному и/или частному нотариусу за нотариальное оформление
        /// </summary>
        Notarization = 11,

        /// <summary>
        /// Расходы обслуживающих производств и хозяйств
        /// </summary>
        ServiceProductionAndFarms = 12
    }
}