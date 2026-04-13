namespace Moedelo.Payroll.Shared.Enums.Charge;

public enum ChargeType
{
    Default = 1,

    /// <summary>Оклад</summary>
    GeneralSalary = 100,

    #region Региональный коэффициент, надбавки (201-299)

    /// <summary>Региональный коэффициент</summary>
    BonusesRegional = 201,

    /// <summary>Северная надбавка</summary>
    BonusesNorth = 203,

    #endregion

    #region Премии (301-399)

    /// <summary>Разовая премия за бизнес-показатели</summary>
    PremiumBusinessPerformance = 301,

    /// <summary>Премия за месяц</summary>
    PremiumMonth = 302,

    /// <summary>Премия за квартал</summary>
    PremiumQuarter = 303,

    #endregion

    #region Отпуск

    /// <summary>Ежегодный оплачиваемый отпуск</summary>
    VacationGeneral = 501,

    /// <summary>Учебный отпуск</summary>
    VacationStudy = 502,

    /// <summary>Отпуск без сохранения зарплаты</summary>
    VacationWithoutSalary = 503,

    #endregion

    #region Больничный, отпуск по уходу (601-699)

    /// <summary>Пособие на 1 ребенка до 1.5 лет</summary>
    SickListAllowanceForOneChild = 601,

    /// <summary>Пособие на второго и последующего ребенка до 1,5лет</summary>
    SickListAllowanceForSecondChild = 602,

    /// <summary>Компенсация по уходу за ребёнком</summary>
    SickListCompensationForChildCare = 603,

    /// <summary>По беременности и родам</summary>
    SickListPregnancy = 604,
    
    /// <summary>Больничный (после увольн)</summary>
    SickListAfterDismissal = 605,
    
    /// <summary>Больничный при карантине</summary>
    SickListQuarantine = 606,
    
    /// <summary>Больничный сан.-кур. долечивание</summary>
    SickListSpaTreatment = 607,
    
    /// <summary>Больничный сан.-кур. долечивание (туберкулез)</summary>
    SickListSpaTreatmentTuberculosis = 608,
    
    /// <summary>Больничный (протезирование)</summary>
    SickListProsthetics = 609,
    
    /// <summary>Больничный по уходу за членом семьи (амбулаторно)</summary>
    SickListNursingCareAmbulatory = 610,
    
    /// <summary>Больничный по уходу за Ребенком-инвалидом Амбул.</summary>
    SickListChildInvalidAmbulatory = 611,
    
    /// <summary>Больничный по уходу за Ребенком-инвалидом Стац.</summary>
    SickListChildInvalidStationary = 612,
    
    /// <summary>Больничный по уходу за ВИЧ-инфиц реб Амбул.</summary>
    SickListChildHIVAmbulatory = 613,
    
    /// <summary>Больничный по уходу за ВИЧ-инфиц реб Стац.</summary>
    SickListChildHIVStationary = 614,
    
    /// <summary>Больничный по уходу за реб поствакцина Амбул.</summary>
    SickListChildAfterVaccinationAmbulatory = 615,
    
    /// <summary>Больничный по уходу за Реб поствакцина Стац.</summary>
    SickListChildAfterVaccinationStationary = 616,
    
    /// <summary>Больничный по уходу за реб. Амбул.</summary>
    SickListCareChild7Ambulatory = 617,
    
    /// <summary>Больничный по уходу за реб Стац.</summary>
    SickListCareChild7Stationary = 618,
    
    /// <summary>Больничный по уходу за реб. Вкл в перечень Амбул</summary>
    SickListCareChild7YearAmbulatoryIn = 619,
    
    /// <summary>Больничный по уходу за реб вкл перечень Стационар</summary>
    SickListCareChild7YearStationaryIn = 620,
    
    /// <summary>Больничный по уходу за Реб от 7 до 15 лет Амбул.</summary>
    SickListCareChild15YearAmbulatory = 621,
    
    /// <summary>Больничный по уходу за реб  от 7 до 15 лет Стац.</summary>
    SickListCareChild15YearStationary = 622,
    
    /// <summary>Больничный (травма произ)</summary>
    SickListWorkTrauma = 623,
    
    /// <summary>Больничный (заболевание)</summary>
    SickListIllness = 624,
    
    #endregion

    #region Другие виды отсутствия (701-799)

    /// <summary>Пособие при рождении ребенка</summary>
    OtherAbsenceChildBirth = 701,

    /// <summary>Пособие за ранние сроки беременности</summary>
    OtherAbsenceEarlyPregnancy = 702,

    /// <summary>Пособие на погребение</summary>
    OtherAbsenceFuneralAllowance = 703,

    #endregion

    #region Присутствия и оплата по среднему заработку (801-899)

    /// <summary>Командировочные расходы</summary>
    PresenceBusinessTripExpenses = 801,

    /// <summary>Сверхнормативные суточные</summary>
    PresenceBusinessTripOverDailyAllowances = 802,

    /// <summary>Аванс командировочных расходов</summary>
    PresenceBusinessTripAdvanceExpenses = 803,

    /// <summary>Оплата командировки по среднему заработку</summary>
    PresenceBusinessTrip = 804,

    /// <summary>Доплата за работу в праздники</summary>
    PresenceHolidayWeekendPayment200 = 805,

    #endregion

    #region Выплаты при увольнении (901-999)

    /// <summary>Выходное пособие, не облагаемое взносами в фонды</summary>
    FireSeverancePay = 901,

    /// <summary>Выходное пособие облагаемое взносами в фонды</summary>
    FireSeverancePayWithPayToFund = 902,

    /// <summary>Компенсация при увольнении с которой начисляются взносы в фонды</summary>
    FireCompensationWithPayToFund = 903,

    /// <summary>Компенсация при увольнении</summary>
    FireCompensation =  904,

    /// <summary>Выходное пособие за второй и третий месяц</summary>
    FireSeverancePayForSecondMonth = 905,

    /// <summary>Компенсация за неиспользованный отпуск при увольнении</summary>
    FireUnusedVacationDismissal = 906,

    #endregion

    #region Доп.доход и прочие начисления (1001-1099)

    /// <summary>Аренда имущества у физ.лица</summary>
    OtherIncomeRent = 1001,

    /// <summary>Материальная помощь</summary>
    OtherIncomeHelp = 1002,

    /// <summary>Дивиденды</summary>
    OtherIncomeDividends = 1003,

    /// <summary>Подарок</summary>
    OtherIncomeGift = 1004,
    
    /// <summary>Выгода от беспроцентного не целевого займа</summary>
    OtherIncomeNonPercentLoan = 1005,

    #endregion

    #region Удержания,долги (1101-1199)
    
    /// <summary>Удержание из заработной платы</summary>
    Deduction = 1101,
    
    /// <summary>Удержание за неотработанные дни отпуска</summary>
    DeductionByUnusedVacationForFired = 1102,
    
    #endregion
    
    /// <summary>Аванс</summary>
    Advance = 1200,
    
    #region Гражданский правовой договор (1401-1499),

    /// <summary>Работы и услуги</summary>
    WorkContractWork = 1401,

    /// <summary>Аренда имущества</summary>
    WorkContractOwnership = 1402,

    /// <summary>Проценты по займам</summary>
    WorkContractInterestOnLoans = 1403
    
    #endregion
}