using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.FinancialOperations
{
    public enum BudgetaryPaymentType
    {
        Default = 0,

        /// <summary> Взнос в ПФ на страховую часть </summary>
        InsuranceForIp = 2,

        /// <summary> Взнос в ПФ на накопительную часть Ип за себя </summary>
        AccumulateForIp = 3,

        /// <summary> Взнос в Федеральный ФОМС ИП за себя </summary>
        FederalFomsForIp = 4,

        /// <summary> Взнос в ТФОМС </summary>
        TerretorialFomsForIp = 5,

        /// <summary> По травматизму в ФСС </summary>
        [ForEmployees]
        InjuredFSS = 6,

        /// <summary> НДФЛ </summary>
        Ndfl = 7,

        /// <summary> Пени </summary>
        Peni = 8,

        /// <summary> По нетрудоспособности в ФСС </summary>
        [ForEmployees]
        DisabilityFSS = 9,

        /// <summary> Штраф </summary>
        Penalty = 10,

        /// <summary>Взнос в ТФОМС за сотрудников</summary>
        [ForEmployees]
        TerrorotialFomsForEmployees = 11,

        /// <summary>Взнос в ФФОМС за сотрудников</summary>
        [ForEmployees]
        FederalFomsForEmployees = 12,

        /// <summary> Взнос в ПФ на накопительную часть за сотрудников</summary>
        [ForEmployees]
        AccumulateForEmployees = 13,

        /// <summary> Взнос в ПФ на страховую часть за сотрудников</summary>
        [ForEmployees]
        InsuranceForEmployees = 14,

        /// <summary> ЕНВД </summary>
        Envd = 15,

        /// <summary> УСН 6% </summary>
        [Usn]
        Usn6 = 16,

        /// <summary> УСН 15% </summary>
        [Usn]
        Usn15 = 17,

        /// <summary> Патент </summary>
        Patent = 18,

        /// <summary> Агентский НДС </summary>
        AgentNds = 19,

        /// <summary> Прочее </summary>
        Other = 20,

        /// <summary> Минимальный налог </summary>
        [Usn]
        UsnMinTax = 21,

        PatentNew = 22,
		
		/// <summary>
        /// Торговый сбор
        /// </summary>
        TradingTax = 23,

        /// <summary> Взнос в ПФ на страховую часть за сотрудников c превышения базы </summary>
        [ForEmployees]
        InsuranceOverdraftForEmployees = 24,

        /// <summary> Взнос в ПФ на страховую часть сверх лимита </summary>
        InsuranceOverdraftForIp = 25,
        
        /// <summary>
        /// Оплата налога на доходы физического лица (налоговый агент свыше 5 млн руб.)
        /// </summary>
        NdflOver5Millions = 26,
    }
}