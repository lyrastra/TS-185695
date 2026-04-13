namespace Moedelo.Common.Enums.Enums.KbkNumbers
{
    public enum KbkNumberType
    {
        /// <summary>
        /// Тип Кбк не определен
        /// </summary>
        NotInitialized = 0,

        /// <summary>
        /// Платеж в ФСС за нетрудоспособность сотрудников
        /// </summary>
        FssDisabilityForEmployees = 1,

        /// <summary>
        /// ФСС Добровольные взносы (ИП)
        /// </summary>
        FssVoluntaryContributions = 2,

        /// <summary>
        /// ФСС Штрафы за нарушение зак-ва о взносах
        /// </summary>
        FssForfeitByViolationOfLegislation = 3,

        /// <summary>
        /// Платеж на страховую часть пенсии за сотрудников
        /// </summary>
        InsurancePayForEmployees = 4,

        /// <summary>
        /// Платеж за ИП на страховую часть
        /// </summary>
        InsurancePaymentForIp = 5,

        /// <summary>
        /// Платеж на страховую часть Доп. взносы (досроч. назнач. пенсии, Список 1)
        /// </summary>
        InsuranceAdditionalPayList1 = 6,

        /// <summary>
        /// Платеж на страховую часть Доп. взносы (досроч. назнач. пенсии, Список 2)
        /// </summary>
        InsuranceAdditionalPayList2 = 7,

        /// <summary>
        /// на накопительную часть пенсии за сотрудников
        /// </summary>
        AccumulatePayForEmployees = 8,

        /// <summary>
        /// за ИП на накопительную часть
        /// </summary>
        AccumulatePaymentForIp = 9,
        
        /// <summary>
        /// Платеж в Федеральный ФОМС за сотрудника
        /// </summary>
        FederalFoms = 10,

        /// <summary>
        /// Платеж в Федеральный ФОМС за ИП
        /// </summary>
        FederalFomsForIp = 11,

        /// <summary>
        /// Платеж в Терреториальный ФОМС
        /// </summary>
        TerretorialFoms = 12,

        /// <summary>
        /// Платеж в ФСС за травматизм за сотрудников
        /// </summary>
        FssInjuryForEmployees = 13,

        /// <summary>
        /// Оплата налога на доходы физического лица (налоговый агент)
        /// </summary>
        Ndfl = 14,

        /// <summary>
        /// Оплата налога на доходы физического лица (по доходам ИП)
        /// </summary>
        NdflForIp = 15,

        /// <summary>
        /// Оплата НДС (реализация в РФ)
        /// </summary>
        NdsTax = 16,

        /// <summary>
        /// Оплата НДС (импорт, на таможне)
        /// </summary>
        NdsTaxOnCustomsHouse = 17,

        /// <summary>
        /// Оплата НДС (импорт, в налоговую)
        /// </summary>
        NdsTaxImportToFns = 18,

        /// <summary>
        /// Оплата налога на прибыль в федеральный бюджет
        /// </summary>
        ProfitTaxFederal = 19,

        /// <summary>
        /// Оплата налога на прибыль в бюджет субъекта РФ
        /// </summary>
        ProfitTaxLocal = 20,

        /// <summary>
        /// Оплата налога на прибыль (рос. дивиденды)
        /// </summary>
        ProfitTaxRussianDividends = 21,

        /// <summary>
        /// Оплата налога на прибыль (иностр. дивиденды)
        /// </summary>
        ProfitTaxForeignDividends = 22,

        /// <summary>
        /// Оплата налога на прибыль (цен. бум.)
        /// </summary>
        ProfitTaxSecurities = 23,

        /// <summary>
        /// Транспортный налог
        /// </summary>
        TransportTax = 24,

        /// <summary>
        /// Налог на имущество
        /// </summary>
        PropertyTax = 25,

        /// <summary>
        /// Налог на имущество (им-во, вход. в ЕСГ)
        /// </summary>
        PropertyTaxInEsg = 26,
        
        /// <summary>
        /// Платеж за ЕНВД
        /// </summary>
        Envd = 27,

        /// <summary>
        /// Годовой минимальный налог УСН 6%
        /// </summary>
        DeclarationUsn6 = 28,

        /// <summary>
        /// Годовой минимальный налог УСН 15%
        /// </summary>
        DeclarationUsn15 = 29,

        /// <summary>
        /// Годовой минимальный налог УСН при системе "доходы минус расходы"
        /// </summary>
        DeclarationUsnProfitOutgoMinTax = 30,

        /// <summary>
        /// Земельный налог (Москва, СПб, Севастополь)
        /// </summary>
        TaxOnLand03Moscow = 31,

        /// <summary>
        /// Земельный налог  (гор. округ.)
        /// </summary>
        TaxOnLand03City = 32,

        /// <summary>
        /// Земельный налог  (межселен. тер.)
        /// </summary>
        TaxOnLand03Village = 33,

        /// <summary>
        /// Земельный налог  (сельских пос.)
        /// </summary>
        TaxOnLand03Location = 34,

        /// <summary>
        /// Земельный налог (гор. пос.)
        /// </summary>
        TaxOnLand15Moscow = 35,

        /// <summary>
        /// Земельный налог (гор. округ. с внутригородским дел.) 
        /// </summary>
        TaxOnLand15City = 36,

        /// <summary>
        /// Земельный налог (внутригород. районов)
        /// </summary>
        TaxOnLand15Village = 37,

        /// <summary>
        /// Платеж на страховую часть Доп. взносы (досроч. назнач. пенсии, Список 1)
        /// </summary>
        InsuranceForfeitByViolationOfLegislation = 39,

        /// <summary>
        /// Платеж на страховую часть Доп. взносы (досроч. назнач. пенсии, Список 2)
        /// </summary>
        InsuranceForFailureToDocument = 40,

        /// <summary>
        /// Торговый сбор
        /// </summary>
        SalesTax = 41,

        /// <summary>
        /// Платеж на страховую часть пенсии за сотрудников с превышения базы
        /// </summary>
        InsurancePayOverdraft = 42,

        /// <summary>
        /// Платеж за патент
        /// </summary>
        Patent = 43,
        
        /// <summary>
        /// Оплата налога на доходы физического лица (налоговый агент свыше 5 млн руб.)
        /// </summary>
        NdflOver5Millions = 44,

        /// <summary>
        /// Единый налоговый счет
        /// </summary>
        UnifiedTaxAccount = 45,

        /// <summary>
        /// Страховые взносы (ОПС, ОМС и ОСС по ВНиМ)
        /// </summary>
        InsuranceContribution = 46,

        /// <summary>
        /// Страховые взносы ВНиМ
        /// </summary>
        InsuranceTempDisability = 47,

        /// <summary>
        /// Страховые взносы ОПС
        /// </summary>
        InsuranceForPension = 48,

        /// <summary>
        /// Страховые взносы ОМС
        /// </summary>
        InsuranceForMedical = 49,

        /// <summary>
        /// НДФЛ (с див. до 5 млн. руб.) 
        /// </summary>
        NdflDividends = 50,

        /// <summary>
        /// НДФЛ (с див. свыше. 5 млн. руб.)
        /// </summary>
        NdflDividendsOver5Millions = 51,

        /// <summary>
        /// Доп. взносы за ИП
        /// </summary>
        AdditionalContributionForIp = 52,

        /// <summary>
        /// Доптариф (9%) за вредные усл. без СОУТ
        /// </summary>
        HarmfulConditions9 = 53,

        /// <summary>
        /// Доптариф (9%) за вредные усл. по рез-там СОУТ
        /// </summary>
        HarmfulConditions9SA = 54,

        /// <summary>
        /// Доптариф (6%) за вредные усл. без СОУТ
        /// </summary>
        HarmfulConditions6 = 55,

        /// <summary>
        /// Доптариф (6%) за вредные усл. по рез-там СОУТ
        /// </summary>
        HarmfulConditions6SA = 56,
        
        /// <summary>
        /// НДФЛ (нал. агент до 2,4 млн. руб/с див. нерезиденту РФ)
        /// </summary>
        NdflBelow2_4Millions = 57,
        
        /// <summary>
        /// НДФЛ (нал. агент с 2,4 до 5 млн. руб.)
        /// </summary>
        NdfFrom2_4To5Millions = 58,

        /// <summary>
        /// НДФЛ (нал. агент с 5 до 20 млн. руб)
        /// </summary>
        NdflFrom5To20Millions = 59,
        
        /// <summary>
        /// НДФЛ (нал. агент с 20 до 50 млн. руб)
        /// </summary>
        NdflFrom20To50Millions = 60,
        
        /// <summary>
        /// НДФЛ (нал. агент свыше 50 млн. руб)
        /// </summary>
        NdflOver50Millions = 61,
        
        /// <summary>
        /// НДФЛ для РК и северной: НДФЛ (нал. агент до 5 млн. руб)
        /// </summary>
        NdflRKandNorthBelow5Millions = 62,
        
        /// <summary>
        /// НДФЛ для РК и северной: НДФЛ (нал. агент свыше 5 млн. руб)
        /// </summary>
        NdflRKandNorthOver5Millions = 63,
        
        /// <summary>
        /// НДФЛ (с див. до 2,4 млн. руб)
        /// </summary>
        NdflDividendsBelow2_4Millions = 64,
        
        /// <summary>
        /// НДФЛ (с див. свыше 2,4 млн. руб)
        /// </summary>
        NdflDividendsOver2_4Millions = 65,
        
        /// <summary>
        /// НДФЛ (по доходам ИП до 2,4 млн. руб.)
        /// </summary>
        NdflBelow2_4MillionsForIp = 66,
        
        /// <summary>
        /// НДФЛ (по доходам ИП с 2,4 до 5 млн. руб.)
        /// </summary>
        NdflFrom2_4to5MillionsForIp = 67,
        
        /// <summary>
        /// НДФЛ (по доходам ИП с 5 до 20 млн. руб.)
        /// </summary>
        NdflFrom5to20MillionsForIp = 68,
        
        /// <summary>
        /// НДФЛ (по доходам ИП с 20 до 50 млн. руб.)
        /// </summary>
        NdflFrom20to50MillionsForIp = 69,
        
        /// <summary>
        /// НДФЛ (по доходам ИП свыше 50 млн. руб.)
        /// </summary>
        NdflOver50MillionsForIp = 70,
        
        /// <summary>
        /// Земельный налог (муниципальный округ)
        /// </summary>
        TaxOnLand15District = 71,
        
        /// <summary>
        /// Туристический налог
        /// </summary>
        TouristTax = 72,
        
        /// <summary>
        /// Единый сельскохозяйственный налог (ЕСХН)
        /// </summary>
        AgriculturalTax = 73
    }
}