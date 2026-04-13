namespace Moedelo.Common.Enums.Enums.TradingObjects
{
    public enum BenefitType
    {
        /// <summary>
        /// Нет льготы
        /// </summary>
        No = 0,

        /// <summary>
        /// Торговля с использованием торговых (вендинговых) автоматов
        /// </summary>
        WithUseVendingMachine = 1,

        /// <summary>
        /// Торговля на территории ярмарки
        /// </summary>
        FairTerritory = 2,

        /// <summary>
        /// Торговля на территории розничного рынка
        /// </summary>
        RetailMarketTerritory = 3,

        /// <summary>
        /// Торговля в помещениях, находящихся в оперативном управлении автономных, бюджетных и казенных учреждений
        /// </summary>
        AutonomousOrBudgetOrStateInstitutionLocation = 4,

        /// <summary>
        /// Торговля на территории агропромышленного кластера
        /// </summary>
        AgroindustrialClusterTerritory = 5,

        /// <summary>
        /// Сопутствующая торговля при продаже билетов в кинотеатрах, театрах и музеях
        /// </summary>
        WithSaleTicketsInCinemaOrTheaterOrMuseum = 6,

        /// <summary>
        /// Торговля печатной продукцией
        /// </summary>
        PrintedProduct = 7,

        /// <summary>
        /// Религиозная организация занимается торговлей в культовом здании, сооружении или на прилегающей территории
        /// </summary>
        ReligiousOrganization = 8,

        /// <summary>
        /// Сопутствующая торговля при оказании бытовых услуг.
        /// </summary>
        PersonalServices = 9
    }
}