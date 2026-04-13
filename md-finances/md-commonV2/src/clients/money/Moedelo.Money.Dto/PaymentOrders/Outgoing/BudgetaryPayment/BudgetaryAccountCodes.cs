namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment
{
    /// <summary>
    /// Тип взноса (Бух. коды)
    /// </summary>
    public enum BudgetaryAccountCodes
    {
        /// <summary>
        /// Налог на доходы физических лиц
        /// </summary>
        Ndfl = 680100,

        /// <summary>
        /// Налог на добавленную стоимость
        /// </summary>
        Nds = 680200,

        /// <summary>
        /// Налог на прибыль
        /// </summary>
        ProfitTaxes = 680400,

        /// <summary>
        /// Транспортный налог
        /// </summary>
        TransportTaxes = 680700,

        /// <summary>
        /// Налог на имущество
        /// </summary>
        PropertyTaxes = 680800,

        /// <summary>
        /// Торговый сбор
        /// </summary>
        TradingFees = 680900,

        /// <summary>
        /// Прочие налоги и сборы
        /// </summary>
        OtherTaxes = 681000,

        /// <summary>
        /// Единый налог на вмененный доход
        /// </summary>
        Envd = 681100,

        /// <summary>
        ///Единый налог при применении упрощенной системы налогообложения
        /// </summary>
        EnvdForUsn = 681200,

        /// <summary>
        /// Земельный налог
        /// </summary>
        LandTaxes = 681300,

        /// <summary>
        /// Расчеты по социальному страхованию (страховые взносы в части, перечисляемой в ФСС)
        /// </summary>
        FssFee = 690100,

        /// <summary>
        /// Страховая часть трудовой пенсии
        /// </summary>
        PfrInsuranceFee = 690201,

        /// <summary>
        /// Накопительная часть трудовой пенсии
        /// </summary>
        PfrAccumulateFee = 690202,

        /// <summary>
        /// Расчеты по обязательному медицинскому страхованию (страховые взносы в части, перечисляемой в фонды ОМС)
        /// </summary>
        FomsFee = 690300,

        /// <summary>
        /// Расчеты по обязательному социальному страхованию от несчастных случаев на производстве и профессиональных заболеваний
        /// </summary>
        FssInjuryFee = 691100,

        /// <summary>
        /// Налог по патенту
        /// </summary>
        Patent = 681400,
        
        /// <summary>
        /// Единый налоговый платеж (ЕНП)
        /// </summary>
        UnifiedBudgetaryPayment = 686900,
        
        /// <summary>
        /// Страховые взносы (ОПС, ОМС и ОСС по ВНиМ)
        /// </summary>
        InsuranceFee = 691000
    }
}
