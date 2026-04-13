namespace Moedelo.Common.Enums.Enums.Billing
{
    public enum  PaymentPositionType
    {
        /// <summary> 
        /// Доступ к сервису
        /// </summary>
        Service,

        /// <summary> 
        /// Касса
        /// </summary>
        Cash,

        /// <summary> 
        /// Доступ к оператору фискальных данных
        /// </summary>
        CashFDO,

        /// <summary> 
        /// Активация и настройка ККТ и ОФД
        /// </summary>
        CashActivation,

        /// <summary> 
        /// Доставка
        /// </summary>
        Shipping,
        
        /// <summary>
        /// Разовая услуга
        /// </summary>
        OneTimeService,

        /// <summary>
        /// Услуга бухгалтерского сопровождения
        /// </summary>
        OutsourceSupport,
        
        /// <summary>
        /// За консультационные услуги сервиса "Моё Дело"
        /// </summary>
        Consultations
    }
}
