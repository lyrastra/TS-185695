namespace Moedelo.Finances.Domain.Enums.Money.Table
{
    public enum MoneyTableFilterBudgetaryType
    {
        None = 0,
        /// <summary> Налог на доходы физических лиц </summary>
        Ndfl = 1,
        /// <summary> Налог на добавленную стоимость </summary>
        Nds = 2,
        /// <summary> Налог на прибыль (расчеты с бюджетом) (только ООО) </summary>
        ProfitTax = 3,
        /// <summary> Транспортный налог (только ООО) </summary>
        TransportTax = 4,
        /// <summary> Налог на имущество (только ООО) </summary>
        PropertyTax = 5,
        /// <summary> Торговый сбор </summary>
        MerchantTax = 6,
        /// <summary> Единый налог на вмененный доход </summary>
        Envd = 7,
        /// <summary> Единый налог при применении УСН </summary>
        EnvdForUsn = 8,
        /// <summary> Земельный налог </summary>
        LandTax = 9,
        /// <summary> Прочие налоги и сборы </summary>
        OtherTax = 10,
        /// <summary> Расчеты по соц. страхованию (страховые взносы в части, перечисляемой в ФСС) </summary>
        FssFee = 11,
        /// <summary> Страховые взносы на травматизм </summary>
        FssInjuryFee = 12,
        /// <summary> Расчеты по обязательному медицинскому страхованию (страховые взносы в части, перечисляемой в фонды ОМС) </summary>
        FomsFee = 13,
        /// <summary> Страховая часть трудовой пенсии </summary>
        PfrInsuranceFee = 14,
        /// <summary> Накопительная часть трудовой пенсии </summary>
        PfrAccumulateFee = 15,
        /// <summary> Страховые взносы (ОПС, ОМС и ОСС по ВНиМ) </summary>
        InsuranceFee = 16,
        /// <summary> Страховые взносы за сотрудников </summary>
        AllInsuranceFee = 17,
        /// <summary> Фиксированные взносы ИП </summary>
        AllInsuranceFeeIP = 18,
    }
}