namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// Причины, по которым документ нельзя редактировать
    /// Список общий для всей первички, хотя какие-то значения могут быть неактуальными для тех или иных типов документов
    /// </summary>
    public enum ReadOnlyReasonType
    {
        /// <summary>
        /// Нет прав на редактирование
        /// </summary>
        HasNoEditAccess = 1,
        
        /// <summary>
        /// Документ находится в закрытом периоде
        /// </summary>
        InClosedPeriod = 2,
        
        /// <summary>
        /// Документ проведен бухгалтером (нет прав на редактирование проведенных)
        /// </summary>
        ProvidedByAccountant = 3,
        
        /// <summary>
        /// Связан с розничным возвратом (для ОРП)
        /// </summary>
        HasLinkedRetailRefund = 4,
        
        /// <summary>
        /// Создан из первичного документа (для счета-фактуры)  
        /// </summary>
        CreatedFromPrimaryDocument = 5,
        
        /// <summary>
        /// Создан из остатков (для актов, накладных и сч-фактур в покупках)  
        /// </summary>
        CreatedFromBalances = 6,
        
        /// <summary>
        /// Создан из имущества (для актов и накладных в покупках)  
        /// </summary>
        CreatedFromEstate = 7,
        
        /// <summary>
        /// Связан с авансовым отчетом (для актов, накладных, УПД в покупках)
        /// </summary>
        HasLinkedAdvanceStatement = 8,

        /// <summary>
        /// Используется в возвратах в отчетах комиссионера (для отчетов комиссионера)
        /// </summary>
        UsedInCommissionAgentReportRefunds = 9,
    }
}