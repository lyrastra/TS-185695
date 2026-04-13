namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum IonType
    {
        None = 0,
        /// <summary>Запрос по справке о состоянии расчетов по налогам, сборам, пеням и штрафам</summary>
        Debt = 1,
        /// <summary>Запрос по выписке операций по расчетам с бюджетом</summary>
        Operations = 2,
        /// <summary>Запрос по перечню налоговых деклараций (расчетов) и бухгалтерской отчетности</summary>
        Declarations = 3,
        /// <summary>Запрос по акту сверки расчетов по налогам, сборам, пеням и штрафам</summary>
        Check = 4
    }
}