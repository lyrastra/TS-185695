namespace Moedelo.Common.Enums.Enums.Accounting
{
    public enum KbkType
    {
        /// <summary>
        /// Тип Кбк не определен
        /// </summary>
        [KbkType("error")] 
        NotInitialized = 0,

        /// <summary>
        /// Платеж на накопительную часть пенсии за сотрудников
        /// </summary>
        [KbkType("accEmpl")] 
        AccumulatePayForEmployees = 1,

        /// <summary>
        /// Платеж на страховую часть пенсии за сотрудников
        /// </summary>
        [KbkType("insEmpl")] 
        InsurancePayForEmployees = 2,

        /// <summary>
        /// Платеж в ФСС за травматизм за сотрудников
        /// </summary>
        [KbkType("fssInjury")] 
        FssInjuryForEmployees = 3,

        /// <summary>
        /// Платеж в ФСС за нетрудоспособность сотрудников
        /// </summary>
        [KbkType("fssDisability")] 
        FssDisabilityForEmployees = 4,

        /// <summary>
        /// Платеж в Федеральный ФОМС
        /// </summary>
        [KbkType("federalFoms")] 
        FederalFoms = 5,

        /// <summary>
        /// Платеж в Терреториальный ФОМС
        /// </summary>
        [KbkType("terrFoms")] 
        TerretorialFoms = 6,

        /// <summary>
        /// Платеж за ЕНВД
        /// </summary>
        [KbkType("envd")] 
        Envd = 7,

        /// <summary>
        /// Платеж за ИП на накопительную часть
        /// </summary>
        [KbkType("accIp")] 
        AccumulatePaymentForIp = 8,

        /// <summary>
        /// Платеж за ИП на страховую часть
        /// </summary>
        [KbkType("insIp")] 
        InsurancePaymentForIp = 9,

        /// <summary>
        /// Оплата налога на доходы физического лица
        /// </summary>
        [KbkType("ndflSimple")] 
        Ndfl = 10,

        /// <summary>
        /// Годовой минимальный налог УСН при системе "доходы минус расходы"
        /// </summary>
        [KbkType("usnMin")] 
        DeclarationUsnProfitOutgoMinTax = 11,

        /// <summary>
        /// Годовой налог УСН при системе "доходы минус расходы"
        /// </summary>
        [KbkType("usnProfitOutgo")] 
        DeclarationUsnProfitOutgo = 12,

        /// <summary>
        /// Годовой налог УСН при системе "доходы"
        /// </summary>
        [KbkType("usnProfit")] 
        DeclarationUsnProfit = 13,

        /// <summary>
        /// Аванс по УСН - минимальный налог при системе "доходы минус расходы"
        /// </summary>
        [KbkType("prepUsnMin")] 
        PrepaymentUsnProfitOutgoMinTax = 14,

        /// <summary>
        /// Аванс по УСН - налог при системе "доходы минус расходы"
        /// </summary>
        [KbkType("prepUsnProfitOutgo")] 
        PrepaymentUsnProfitOutgo = 15,

        /// <summary>
        /// Аванс по УСН - налог при системе "доходы"
        /// </summary>
        [KbkType("prepUsnProfit")] 
        PrepaymentUsnProfit = 16,

        /// <summary>
        /// Платеж за восстановление свидетельства ЕГРН
        /// </summary>
        [KbkType("egrnRest")] 
        RestoreEgrn = 17,

        /// <summary>
        /// Платеж за получение выписку ЕГРЮЛ/ЕГРИП
        /// </summary>
        [KbkType("egrlu")] 
        GetEgrlu = 18,

        /// <summary>
        /// Пошлина за регистрацию ИП
        /// </summary>
        [KbkType("registrationIp")] 
        RegistraionIp = 19,

        /// <summary>
        /// Агентский НДС
        /// </summary>
        [KbkType("agentNds")] 
        AgentNds = 20,

        /// <summary>
        /// Оплата налога на прибыль в федеральный бюджет
        /// </summary>
        [KbkType("profitTaxFederal")]
        ProfitTaxFederal = 21,

        /// <summary>
        /// Оплата налога на прибыль в бюджет субъекта РФ
        /// </summary>
        [KbkType("profitTaxLocal")]
        ProfitTaxLocal = 22,

        /// <summary>
        /// Оплата НДС
        /// </summary>
        [KbkType("ndsTax")]
        NdsTax = 23,

        [KbkType("estateTax")]
        EstateTax = 24,

        [KbkType("tradingTax")]
        TradingTax = 25,

        /// <summary>
        /// Платеж на страховую часть пенсии за сотрудников
        /// </summary>
        [KbkType("insOverdraft")]
        InsurancePayOverdraft = 26,

        /// <summary>
        /// Платеж за ИП на страховую часть сверх лимита
        /// </summary>
        [KbkType("insOverIp")]
        InsurancePaymentOverdraftForIp = 27,

        /// <summary>
        /// Оплата налога на доходы физического лица (налоговый агент свыше 5 млн руб.)
        /// </summary>
        [KbkType("ndflOver5Millions")] 
        NdflOver5Millions = 28,
    }
}
