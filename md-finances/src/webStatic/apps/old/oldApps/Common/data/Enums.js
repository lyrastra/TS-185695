/* eslint-disable */

window.Enums = {
    fundChargeType: {
        // / На страховую ча сть пенсии в ПФР
        InsurancePfr: 0,
        // / На накопительную часть пенсии в ПФР
        AccumulatePfr: 1,
        // / В Федеральный ФОМС
        FederalFoms: 2,
        // / В Терреториальный ФОМС
        TerretorialFoms: 3,
        // / На нетрудопособность в ФСС
        DisabilityFss: 4,
        // / На травматизм в ФСС
        InjuredFss: 5,
        // / Солидарная часть для ПФР, начисляемая на суммы,
        // / превыщающие страховую базу (введена с 2012 года)
        InsuranceSolidaryPart: 6
    },

    budgetaryPaymentTypes: {
        Default: 0,
        // / <summary> Налог по УСН </summary>
        Usn: 1,
        // / <summary> Взнос в ПФ на страховую часть </summary>
        InsuranceForIp: 2,
        // / <summary> Взнос в ПФ на накопительную часть Ип за себя </summary>
        AccumulateForIp: 3,
        // / <summary> Взнос в Федеральный ФОМС ИП за себя </summary>
        FederalFomsForIp: 4,
        // / <summary> Взнос в ТФОМС </summary>
        TerretorialFOMSForIp: 5,
        // / <summary> По травматизму в ФСС </summary>
        InjuredFSS: 6,
        // / <summary> НДФЛ </summary>
        NDFL: 7,
        // / <summary> Пени </summary>
        Peni: 8,
        // / <summary> По нетрудоспособности в ФСС </summary>
        DisabilityFSS: 9,
        // / <summary> Штраф </summary>
        Penalty: 10,
        // / <summary>Взнос в ТФОМС за сотрудников</summary>
        TerrorotialFomsForEmployees: 11,
        // / <summary>Взнос в ФФОМС за сотрудников</summary>
        FederalFomsForEmployees: 12,
        // / <summary> Взнос в ПФ на накопительную часть за сотрудников</summary>
        AccumulateForEmployees: 13,
        // / <summary> Взнос в ПФ на страховую часть за сотрудников</summary>
        InsuranceForEmployees: 14,
        // / ЕНВД
        // / </summary>
        ENVD: 15,

        Usn6: 16,

        Usn15: 17,

        Patent: 18,

        Nds: 19,

        Other: 20,

        // / Минимальный налог
        UsnMinTax: 21,
        PatentNew: 22,

        // / Торговый сбор
        TradingTax: 23,

        InsuranceOverdraftForIp: 25,
    },

    budgetaryTypePayment: {
        // / Уплата налога или сбора
        tax: 1,

        // / уплата взноса
        fund: 2,

        // / уплата пошлины
        duty: 3,

        // / уплата пени
        peni: 4,

        // / налоговые санкции, установленные Налоговым кодексом Российской Федерации
        taxPenalties: 5,

        // / административные штрафы
        administrativePenalties: 6,

        // / иные штрафы, установленные соответствующими законодательными или иными нормативными актами
        otherPenalties: 7,

        // / уплата процентов
        percent: 8,

        // / уплата аванса или предоплата
        advance: 9,

        // / уплата платежа
        payment: 10,

        other: 11
    },

    Funds: {
        Default: -1,
        Ifns: 0,
        Pfr: 1,
        Fss: 2,
        Other: 3
    },

    // / основание платежа
    PaymentDetails: {
        // / платежи текущего года
        CurrentYear: 1,

        // / добровольное погашение задолженности по истекшим налоговым периодам при отсутствии требования об уплате налогов (сборов) от налогового органа
        VoluntaryRepayment: 2,

        // / погашение задолженности по требованию налогового органа об уплате налогов (сборов)
        AtRequest: 3,

        // / погашение рассроченной задолженности
        Installment: 4,

        // / погашение отсроченной задолженности
        Deferred: 5,

        // / погашение реструктурируемой задолженности
        Restructured: 6,

        // / погашение отсроченной задолженности в связи с введением внешнего управления
        ExternalControl: 7,

        // /  погашение задолженности, приостановленной к взысканию
        Paused: 8,

        // / погашение задолженности по акту проверки
        InspectionReport: 9,

        // / погашение задолженности по исполнительному документу
        ExecutiveDocument: 10,

        // /  БФ -  текущие платежи физических лиц – клиентов банков (владельцев счета), уплачиваемые со своего банковско
        BankClient: 11,

        // / ИН - Погашение инвестиционного налогового кредита
        IN: 12,

        // / ТЛ - номер определения арбитражного суда об удовлетворении заявления о намерении погасить требования к должнику;
        TL: 13,

        // / РК - номер реестра дела о банкротстве
        RK: 14,

        // / ЗТ - погашение текущей задолженности в ходе процедур, применяемых в деле о банкротстве
        ZT: 15,

        // /"0" - при невозможности однозначно идентифицировать платеж
        Undefined: 16
    },

    PayDaysPaymentTypes: {
        // / Зарплата
        Salary: 1,

        // / Премия
        Award: 2,

        // / Больничный
        Sick: 3,

        // / Пособие
        Allowance: 4,

        // / Мат. помощь
        MaterialAid: 5,

        // / Оплата по ГПД
        GpdPay: 6,

        // / Прочее
        Other: 7,

        // / Дивиденды
        Dividend: 8
    },

    PeriodTypes: {
        None: 0,
        Month: 4,
        Quarter: 3,
        HalfYear: 2,
        Year: 1,
        Date: 9
    },

    MoneyBayTypes: {
        // / <summary> Расчетный счет (по умолчанию)</summary>
        Settlement: 0,

        // / <summary> Богатства </summary>
        Cash: 1,

        // / <summary> Наличными без расчетного счета </summary>
        CashWithoutSettlement: 2,

        // / <summary> Движение со счета на кассу </summary>
        FromSettlementToCash: 3,

        // / <summary> Движение с кассы на счет </summary>
        FromCashToSettlement: 4,

        // / <summary> Движение со счета на счет </summary>
        FromSettlementToSettlement: 5,

        // / <summary> Движение со счета на личный банковский кошелек </summary>
        FromSettlementToBankPurse: 6,

        // / <summary> Платежная система </summary>
        Purse: 10,

        // / <summary> Безденежные </summary>
        WithoutMoney: 11,

        // / <summary> Наличными на расчетный счет </summary>
        CashWithSettlement: 12,

        // / <summary> По расчетной ведомости </summary>
        CashPaybill: 13,

        // / <summary> Списание проданного товара </summary>
        WriteOff: 14,

        // / <summary> По зарплатному проекту </summary>
        SalaryProject: 17
    },

    MoneyTransferOperationTypes: {
        Movement: 0,

        Incoming: 1,

        Outgoing: 2
    },

    OutgoingOperationTypes: {
        NotDefined: -1,

        OperatingExpenses: 0,

        PurchaseOfFixedAssets: 1,

        PayDays: 2,

        BudgetaryPayment: 3,

        OtherOutgoing: 4,

        RemovingTheProfit: 5,

        LoanParentOutgoing: 6,

        LoansThirdPartiesOutgoing: 7,

        DividendPayment: 8,

        MainActivityOutgoing: 9,

        PurseOutgoing: 10,

        WorkerPayment: 11,

        GetMoneyForEmployee: 12,

        RefundToCustomerOutgoing: 13,

        BankFeeOutgoing: 14,

        ElectronicOutgoing: 15
    },

    KontragentTypes: {
        None: 0,

        Kontragent: 1,

        Worker: 2,

        Ip: 3,

        Company: 4 // Текущая организация - ИП или ООО
    },

    // / статистика нажатия на кнопки сохранения операций
    StatisticsAction: {
        Unknown: 0,
        Save: 1,
        SaveAndNew: 2
    },

    BasicAssetTypes: {
        // / Прочее
        Other: 0,

        // / <summary> Автомобили и прицепы
        Cars: 1,

        // / Легкие строения
        LightweightConstruction: 2,

        // / Офисная техника
        OfficeEquipment: 3,

        // / Бытовые приборы
        HouseholdAppliances: 4,

        // / Торговое оборудование
        ShopEquipment: 5,

        // / Мебель и тара
        FurnitureAndPackaging: 6,

        // / Рекламные конструкции
        PromotionalConstruction: 7,

        // / Инструмент и приспособления
        Tools: 8,

        // / Оборудование производственное
        IndustrialEquipment: 9,

        // / Жилая недвижимость
        ResidentialProperty: 10,

        // / Капитальные строения
        CapitalBuilding: 11,

        // / Земля
        Land: 12,

        // / Программное обеспечение
        Software: 13,

        // / Товарные знаки
        Brand: 14,

        // / Авторское право
        Copyright: 15,

        // / Прочие НМА
        OtherIntangibles: 16
    },

    // / доступ к странице Enums.TypeAccessRule
    TypeAccessRule: {
        AccessDenied: 0,

        AccessView: 1,

        AccessEdit: 2
    },

    CreatedFrom: {
        Default: 0,

        Balances: 1
    },

    // / Enums.SalesDocumentTypes
    SalesDocumentTypes: {
        All: 0,
        Statement: 2, // Акты 1
        Project: 5, // Договор 1
        VerificationStatement: 8, // 1
        Bill: 1, // Счета 1
        Invoice: 7, // Счёт-фактура 1
        Waybill: 3, // Накладная 1
        AccountingStatements: 10, // Бухгалтерская справка
        ReturnToSupplier: 9,
        ReturnFromBuyer: 11, // Возврат от покупателя
        IncomingStatementOfFixedAsset: 12, // Входящий акт приёма-передачи ОС
        IncomingCashOrder: 16, // приходный Кассовый ордер
        OutcomingCashOrder: 17, // расходный кассовый ордер
        PaymentOrder: 13, // Платежное поручение
        RetailReport: 15, // Отчет о розничной продаже
        doctypeSwitcher(docType, showType) {
            if (!showType) {
                switch (docType) {
                    case Enums.SalesDocumentTypes.Statement:
                        return 'акт';
                    case Enums.SalesDocumentTypes.Waybill:
                        return 'накладную';
                    case Enums.SalesDocumentTypes.Invoice:
                        return 'счет-фактуру';
                    case Enums.SalesDocumentTypes.Bill:
                        return 'счет';
                    case Enums.SalesDocumentTypes.AccountingStatements:
                        return 'бухгалтерскую справку';
                    case Enums.SalesDocumentTypes.ReturnFromBuyer:
                        return 'возврат от покупателя';
                    case Enums.SalesDocumentTypes.ReturnToSupplier:
                        return 'возврат постащику';
                    case Enums.SalesDocumentTypes.IncomingStatementOfFixedAsset:
                        return 'входящий акт приёма-передачи ОС';
                    case Enums.SalesDocumentTypes.IncomingCashOrder:
                        return 'приходный кассовый ордер';
                    case Enums.SalesDocumentTypes.OutcomingCashOrder:
                        return 'расходный кассовый ордер';
                    case Enums.SalesDocumentTypes.PaymentOrder:
                        return 'платежное поручение';
                    case Common.Data.AccountingDocumentType.MiddlemanReport:
                        return 'отчет посредника';
                }
            } else if (showType == 'nominative') {
                switch (docType) {
                    case Enums.SalesDocumentTypes.Statement:
                        return 'акт';
                    case Enums.SalesDocumentTypes.Waybill:
                        return 'накладная';
                    case Enums.SalesDocumentTypes.Invoice:
                        return 'счет-фактура';
                    case Enums.SalesDocumentTypes.Bill:
                        return 'счет';
                    case Enums.SalesDocumentTypes.AccountingStatements:
                        return 'бухгалтерская справка';
                    case Enums.SalesDocumentTypes.ReturnFromBuyer:
                        return 'возврат от покупателя';
                    case Enums.SalesDocumentTypes.ReturnToSupplier:
                        return 'возврат постащику';
                    case Enums.SalesDocumentTypes.IncomingStatementOfFixedAsset:
                        return 'входящий акт приёма-передачи ОС';
                    case Enums.SalesDocumentTypes.IncomingCashOrder:
                        return 'приходный кассовый ордер';
                    case Enums.SalesDocumentTypes.OutcomingCashOrder:
                        return 'расходный кассовый ордер';
                    case Enums.SalesDocumentTypes.PaymentOrder:
                        return 'платежное поручение';
                    case Common.Data.AccountingDocumentType.MiddlemanReport:
                        return 'отчет посредника';
                }
            } else {
                switch (docType) {
                    case Enums.SalesDocumentTypes.All:
                        return 'акты, накладные и счета-фактуры';
                    case Enums.SalesDocumentTypes.Statement:
                        return 'акты';
                    case Enums.SalesDocumentTypes.Waybill:
                        return 'накладные';
                    case Enums.SalesDocumentTypes.Invoice:
                        return 'счета-фактуры';
                    case Enums.SalesDocumentTypes.Bill:
                        return 'счета';
                    case Enums.SalesDocumentTypes.AccountingStatements:
                        return 'бухгалтерские справки';
                    case Enums.SalesDocumentTypes.ReturnFromBuyer:
                        return 'возвраты от покупателя';
                    case Enums.SalesDocumentTypes.ReturnToSupplier:
                        return 'возвраты постащику';
                    case Enums.SalesDocumentTypes.IncomingStatementOfFixedAsset:
                        return 'входящий акт приёма-передачи ОС';
                    case Enums.SalesDocumentTypes.IncomingCashOrder:
                        return 'приходный кассовый ордер';
                    case Enums.SalesDocumentTypes.OutcomingCashOrder:
                        return 'расходный кассовый ордер';
                    case Enums.SalesDocumentTypes.PaymentOrder:
                        return 'платежное поручение';
                    case Common.Data.AccountingDocumentType.MiddlemanReport:
                        return 'отчеты посредника';
                }
            }
        },

        convertToAccountingDocumentType(docType) {
            switch (docType) {
                case Enums.SalesDocumentTypes.Statement:
                    return Common.Data.AccountingDocumentType.Statement;
                case Enums.SalesDocumentTypes.Waybill:
                    return Common.Data.AccountingDocumentType.Waybill;
                case Enums.SalesDocumentTypes.Invoice:
                    return Common.Data.AccountingDocumentType.Invoice;
                case Enums.SalesDocumentTypes.Bill:
                    return Common.Data.AccountingDocumentType.Bill;
                case Enums.SalesDocumentTypes.AccountingStatements:
                    return Common.Data.AccountingDocumentType.AccountingStatement;
                case Enums.SalesDocumentTypes.RetailReport:
                    return Common.Data.AccountingDocumentType.RetailReport;
                default:
                    return docType;
            }
        }
    },

    SalesDocumentSubTypes: {
        All: 0,
        BillForPayment: 1, // Счет на оплату
        BillContract: 2 // Счет-договор
    },

    // Enums.SalesDocumentStatuses
    SalesDocumentStatuses: {
        Default: 0, // Не подписан
        OnSigning: 1, // Скан
        Signed: 2, // На руках

        Kontragent: 3,
        NotPayed: 4, // НЕТ (счета)
        PartialPayed: 5,
        Payed: 6 // ДА (счета)
    },

    // Enums.BillTypeStatuses
    BillTypeStatuses: {
        Default: 0,
        Last: 1,
        NotPayed: 2,
        NotCovered: 3,
        OverDue: 4,
        Payed: 5,
        OnLine: 6,
        WithClosingDocuments: 7
    },

    // / Тип счета
    BillFullClientDataTypes: {
        // счет на оплату
        BillSimple: 0,

        // счет-договор
        BillContract: 1
    },

    // Enums.PaymentStatus
    PaymentStatus: {
        Unpaid: 1,
        Trial: 2,
        Demo: 3,
        Paid: 4,
        Unknown: 0
    },

    WaybillReason: {
        Bill: 'Счет',
        Project: 'Договор',
        Order: 'Заказ'
    },

    DocFormat:
    {
        Excel: 0,
        Pdf: 1,
        Doc: 2
    },

    NdsTypes: {
        None: -1,
        Nds0: 0,
        Nds10: 10,
        Nds18: 18
    },

    WaybillKontragentTypes: {
        Text: 0,
        Firm: 1,
        Kontragent: 2
    },

    TableSorter: {
        Types: {
            Number: 'number',
            Date: 'date',
            Kontragent: 'kontragent',
            Sum: 'summ'
        },

        SortOrder: {
            Asc: 0,
            Desc: 1
        }
    },

    Kontragents: {
        KontragentTypes: {
            Kontragent: 1,
            Client: 2,
            Partner: 3
        }
    },

    // Enums.TimeFilterTypes ( Типы временного фильтра )
    TimeFilterTypes: {
        Year: 0,
        Month: 1,
        Quarter: 2,
        Custom: 3,
        All: 4
    },

    BuyDocumentTypes: {
        All: 0,
        Statement: 2,
        Waybill: 3,
        Invoice: 7,
        ReturnToSupplier: 9,
        Upd: 33,
        convertToAccountingDocumentType(docType) {
            switch (docType) {
                case Enums.BuyDocumentTypes.Statement:
                    return Common.Data.AccountingDocumentType.Statement;
                case Enums.BuyDocumentTypes.Waybill:
                    return Common.Data.AccountingDocumentType.Waybill;
                case Enums.BuyDocumentTypes.Invoice:
                    return Common.Data.AccountingDocumentType.Invoice;
                case Enums.BuyDocumentTypes.Upd:
                    return Common.Data.AccountingDocumentType.Upd;
                default:
                    return null;
            }
        }
    },


    EstimateMethodOfMpzForWriteOffForTaxAccounting: {
        // По средней стоимости.
        ByAverageCost: 1,
        // ФИФО.
        Fifo: 2,
        // По стоимости единицы.
        ByUnitCost: 3,
        // Разные способы для различных групп.
        DifferentMethodsForDifferentGroups: 4,
        // ЛИФО.
        Lifo: 5
    },

    // Оценка товаров для розничной торговли
    EstimateMethodForProductsCost: {
        // В покупных ценах
        InPurchasePrices: 1,
        // В продажных ценах с отдельным учетом наценок (скидок) на счете 42 «Торговая наценка»
        InSalePricesWithDiscounts: 2
    }
};
