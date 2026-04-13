using Moedelo.Common.Enums.Attributes;
using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Billing
{
    public enum Tariff
    {
        Any = -2,

        Block = -1,

        [ObsoleteTariff]
        IpFree = 0,

        [ObsoleteTariff]
        OooFree = 1,

        [ObsoleteTariff]
        [Description("ИП без сотрудников")]
        IpWithOutWorkers = 2,

        [ObsoleteTariff]
        [Description("ИП с сотрудниками")]
        IpWithWorkers = 3,

        [Description("Регистрация ИП")]
        IpRegistration = 4,

        [ObsoleteTariff]
        [Description("ООО Старт")]
        OooStart = 5,

        [ObsoleteTariff]
        [Description("ООО Оптимальный")]
        OooOptimum = 6,

        [ObsoleteTariff]
        [Description("Отчетность VIP")]
        IpReportVip = 7,

        [ObsoleteTariff]
        [Description("Без сотрудников")]
        WithoutWorkers = 8,

        [Description("С сотрудниками")]
        WithWorkers = 9,

        [Description("Регистрация ООО")]
        OooRegistration = 10,

        [Description("Консультант бухгалтера")]
        AccountantConsultatnt = 11,

        [Description("Личный кабинет бухгалтера")]
        AccountantChamber = 12,

        [Description("УСНО + ЕНВД")]
        UsnAccountant = 13,

        [Description("Зарплата и кадры")]
        SalaryAndPersonal = 14,

        [Description("ИП без сотрудников")]
        IpWithoutWorkers = 15,

        [Description("ООО без сотрудников")]
        OooWithoutWorkers = 16,

        [Description("Консультант бухгалтера. Малый Бизнес")]
        AccountantConsultantSmallBusiness = 17,

        [Description("Кабинет бухгалтера. Малый Бизнес")]
        AccountantChamberSmallBusiness = 18,

        [Description("Зарплата и Кадры. Малый Бизнес")]
        SalaryAndPersonalSmallBusiness = 19,

        [Description("ЭЦП")]
        DigitalSign = 20,

        [Description("Консультант бухгалтера до 5 пользователей")]
        AccountantConsultant5Users = 21,

        [Description("Аутсорсинг - СТАРТ")]
        OutsourcingStart = 22,

        [Description("Аутсорсинг - СТАРТ Плюс")]
        OutsourcingStartPlus = 23,

        [Description("Аутсорсинг - Микро бизнес")]
        OutsourcingMicroBusiness = 24,

        [Description("Аутсорсинг - Микро бизнес плюс")]
        OutsourcingMicroBusinessPlus = 25,

        [Description("Аутсорсинг - Малый бизнес")]
        OutsourcingSmallBusiness = 26,

        [Description("Аутсорсинг - Малый бизнес +")]
        OutsourcingSmallBusinessPlus = 27,

        [Description("Аутсорсинг - Индивидуальный")]
        OutsourcingIndividual = 28,

        [Description("Консультант бухгалтера. Многопользовательский до 10 пользователей")]
        AccountantConsultant10Users = 29,

        [Description("Консультант бухгалтера. Многопользовательский до 50 пользователей")]
        AccountantConsultant50Users = 30,

        [Description("Совместный тариф Мое дело алиас 9 - С сотрудниками")]
        AccountingAndBank = 31,

        [Description("Для бухгалтера - Малый бизнес")]
        BuhSmallBusiness = 32,

        [Description("Для бухгалтера - Стандарт")]
        BuhStandart = 33,

        [Description("Для бухгалтера - Проф. версия")]
        BuhPro = 34,

        [Description("Отчетность в ФНС")]
        UsnZero = 35,

        [Description("УСН+ЕНВД Без сотрудников")]
        UsnWithoutWorkers = 36,

        [Description("УСН+ЕНВД До 5 сотрудников")]
        UsnUpToFive = 37,

        [Description("УСН+ЕНВД Максимальный")]
        UsnMax = 38,

        [Description("Справочная Система для Бухгалтера и Кадровика. Малый бизнес")]
        SpsSmallBusiness = 39,

        [Description("Справочная Система для Бухгалтера и Кадровика. Стандарт")]
        SpsStandart = 40,

        [Description("Справочная Система для Бухгалтера и Кадровика. Проф. версия однопользовательская")]
        SpsProSingleUser = 41,

        [Description("Справочная Система для Бухгалтера и Кадровика. Проф версия до 5")]
        SpsPro5Users = 42,

        [Description("Справочная Система для Бухгалтера и Кадровика. Проф версия до 10")]
        SpsPro10Users = 43,

        [Description("Справочная Система для Бухгалтера и Кадровика. Проф версия до 50")]
        SpsPro50Users = 44,

        [Description("Старт ОСНО")]
        StartOsno = 45,

        [Description("Старт Осно +")]
        StartOsnoPlus = 46,

        [Description("Микро бизнес ОСНО")]
        MicroBusinessOsno = 47,

        [Description("Микро бизнес Осно +")]
        MicroBusinessOsnoPlus = 48,

        [Description("Малый бизнес ОСНО")]
        SmallBusinessOsno = 49,

        [Description("Малый бизнес ОСНО +")]
        SmallBusinessOsnoPlus = 50,

        [Description("Старт ИП 6%")]
        StartIp6 = 51,

        [Description("Старт ИП 15%")]
        StartIp15 = 52,

        [Description("Старт +")]
        StartPlus = 53,

        [Description("Микро бизнес")]
        MicroBusiness = 54,

        [Description("Микро бизнес +")]
        MicroBusinessPlus = 55,

        [Description("Малый бизнес")]
        SmallBusiness = 56,

        [Description("Малый бизнес +")]
        SmallBusinessPlus = 57,

        [Description("УСН+ЕНВД расширенный")]
        ProfessionalUsn = 58,

        [Description("ОСНО+ЕНВД Расширенный")]
        ProfessionalOsno = 59,

        [Description("Тариф Открытие")]
        Openning = 60,

        [Description("Справочная Система для Бухгалтера и Кадровика. Стандарт + ОСНО")]
        SpsStandartOsno = 61,

        [Description("Справочная Система для Бухгалтера и Кадровика. Проф. версия однопользовательская + ОСНО")]
        SpsProSingleUserOsno = 62,

        [Description("Фингуру")]
        Finguru = 63,

        [Description("Кнопка")]
        Knopka = 64,

        [Description("Новогодний")]
        NewYear = 65,

        [Description("УСН+ЕНВД максимальный для аутсорс-тарифа Фингуру")]
        FinguruUsnMax = 66,

        [Description("УСН+ЕНВД расширенный для аутсорс-тарифа Фингуру")]
        FinguruProfessionalUsn = 67,

        [Description("ОСНО+ЕНВД расширенный для аутсорс-тарифа Фингуру")]
        FinguruProfessionalOsno = 68,

        [Description("УСН+ЕНВД максимальный для аутсорс-тарифа Кнопка")]
        KnopkaUsnMax = 69,

        [Description("УСН+ЕНВД расширенный для аутсорс-тарифа Кнопка")]
        KnopkaProfessionalUsn = 70,

        [Description("ОСНО+ЕНВД расширенный для аутсорс-тарифа Кнопка")]
        KnopkaProfessionalOsno = 71,

        [Description("Справочная Система для Бухгалтера и Кадровика. Стандарт новый")]
        SpsStandartNew = 73,

        [Description("Счета и документы")]
        BillsAndDocuments = 74,

        [Description("Бюро")]
        Office = 75,

        [Description("Бюро Проф")]
        OfficePro = 76,

        [Description("Справочная Система для Бухгалтера и Кадровика. Стандарт плюс")]
        SpsStandartPlus = 77,

        [Description("Профессиональный аутсорсер")]
        ProfOutsource = 78,

        [Description("Регистрация ИП/ООО")]
        MasterOfRegistration = 79,

        [Description("Интеза")]
        Intesa = 80,

        [Description("Открытие УСН+ЕНВД Без сотрудников")]
        OpenningUsnWithoutWorkers = 81,

        [Description("Открытие УСН+ЕНВД До 5 сотрудников")]
        OpenningUsnUpToFive = 82,

        [Description("Открытие УСН+ЕНВД Максимальный")]
        OpenningUsnMax = 83,

        [Description("Главучёт БИЗ")]
        GlavuchetBiz = 84,

        [Description("Главучёт УСН+ЕНВД")]
        GlavuchetUsnEnvd = 85,

        [Description("Главучёт ОСНО")]
        GlavuchetOsno = 86,

        [Description("Бюро Лайт")]
        OfficeLite = 87,

        [Description("Малыш")]
        Kiddy = 88,

        [Description("УСН+ЕНВД Лайт")]
        UsnLite = 89,

        [Description("Страховой агент")]
        InsuranceAgent = 90,

        [Description("Старт мини до 250 тыс. руб.")]
        StartMiniUpTo250 = 91,

        [Description("Старт до 500 тыс. руб.")]
        StartUpTo500 = 92,

        [Description("Дело пошло от 750 тыс. руб. до 1 млн. руб.")]
        BusinessWentUpTo1000 = 93,

        [Description("Дело пошло от 1 млн. руб. до 2 млн. руб.")]
        BusinessWentUpTo2000 = 94,

        [Description("Дело пошло от 2 млн. руб. до 3 млн. руб.")]
        BusinessWentUpTo3000 = 95,

        [Description("Мое Дело от 3 млн. руб. до 4 млн. руб.")]
        MyBusinessUpTo4000 = 96,

        [Description("Мое Дело от 4 млн. руб. до 6 млн. руб.")]
        MyBusinessUpTo6000 = 97,

        [Description("Мое Дело от 6 млн. руб.")]
        MyBusinessOf6000 = 98,

        [Description("Бюро Партнёрский")]
        OfficePartner = 99,

        [Description("Бюро Старт")]
        OfficeStart = 100,

        [Description("Бюро Правовед")]
        OfficeLegist = 101,

        [Description("Сбербанк Без сотрудников")]
        SberbankWithOutWorkers = 102,

        [Description("Сбербанк C сотрудниками")]
        SberbankWithWorkers = 103,

        [Description("Сбербанк Максимальный")]
        SberbankMax = 104,

        [Description("Бюро Проверка контрагентов")]
        OfficeCheckContragent = 105,

        [Description("УСН+ЕНВД Без сотрудников +")]
        UsnWithoutWorkersPlus = 106,

        [Description("УСН+ЕНВД До 5 сотрудников +")]
        UsnUpToFivePlus = 107,

        [Description("УСН+ЕНВД Максимальный +")]
        UsnMaxPlus = 108,

        [Description("Сбербанк Аутсорсинг")]
        SberbankOutsourcing = 109,

        [Description("Сбербанк Отчетность в ФНС")]
        SberbankZero = 110,

        [Description("Бюро Проверка контрагентов +")]
        OfficeCheckContragentPlus = 111,

        [Description("Мэйл.ру Без сотрудников")]
        MailRuWithoutWorkers = 112,

        [Description("Мэйл.ру До 5 сотрудников")]
        MailRuUpToFive = 113,

        [Description("Мэйл.ру Максимальный")]
        MailRuMax = 114,

        [Description("Бюро Стандарт Мэйл.ру")]
        OfficeMailRuStandart = 115,

        [Description("Бюро Проф Мэйл.ру")]
        OfficeMailRuPro = 116,

        [Description("Новый бизнес")]
        NewBusiness = 117,

        [Description("УС УСН+ЕНВД Без сотрудников")]
        ProfessionalUsnWithoutWorkers = 118,

        [Description("УС УСН+ЕНВД До 5 сотрудников")]
        ProfessionalUsnUpToFive = 119,

        [Description("УС УСН+ЕНВД Максимальный")]
        ProfessionalUsnMax = 120,

        [Description("Беззаботный (от 4 млн. до 6 млн.)")]
        CarefreeFrom4M = 121,

        [Description("Беззаботный (от 6 млн.)")]
        CarefreeFrom6M = 122,

        [Description("Мое дело команда (до 15 млн совокупно до 3-х лиц)")]
        MoedeloTeam = 123,

        [Description("Моё дело Такси")]
        Taxi = 124,

        [Description("Бюро Проф+")]
        OfficeProPlus = 125,

        [Description("Бюро Индивидуальный")]
        OfficeIndividual = 126,

        [Description("Убер Без сотрудников")]
        UberWithoutWorkers = 127,

        [Description("CaseLook + Стандарт")]
        CaseLookStandart = 128,

        [Description("CaseLook + Проф")]
        CaseLookPro = 129,

        [Description("CaseLook + Проф+")]
        CaseLookProPlus = 130,

        [Description("Доставка ЭЦП")]
        DeliveryDigitalSign = 131,

        [Description("Дело пошло от 500 тыс. руб. до 750 тыс. руб.")]
        BusinessWentUpTo750 = 132,

        [Description("Старт ОСНО до 500 тыс. руб.")]
        StartOsnoUpTo500 = 133,

        [Description("Дело пошло ОСНО от 500 тыс. руб. до 750 тыс. руб.")]
        BusinessWentOsnoUpTo750 = 134,

        [Description("Дело пошло ОСНО от 750 тыс. руб. до 1 млн. руб.")]
        BusinessWentOsnoUpTo1000 = 135,

        [Description("Дело пошло ОСНО от 1 млн. руб. до 2 млн. руб.")]
        BusinessWentOsnoUpTo2000 = 136,

        [Description("Дело пошло ОСНО от 2 млн. руб. до 3 млн. руб.")]
        BusinessWentOsnoUpTo3000 = 137,

        [Description("Дело пошло ОСНО от 3 млн. руб. до 4 млн. руб.")]
        BusinessWentOsnoUpTo4000 = 138,

        [Description("Дополнительный юрист 20 часов")]
        AdditionalJurist20Hours = 139,

        [Description("Дополнительный пакет документов 50 шт.")]
        AdditionalDocuments50Pcs = 140,

        [Description("Старт ОСНО до 500 тыс. руб. повыш. коэфф.")]
        StartOsnoUpTo500RaiseFactor2 = 141,

        [Description("Дело пошло ОСНО от 500 тыс. руб. до 750 тыс руб. повыш. коэфф.")]
        BusinessWentOsnoUpTo750RaiseFactor2 = 142,

        [Description("Дело пошло ОСНО от 750 тыс. руб. до 1 млн. руб. повыш. коэфф.")]
        BusinessWentOsnoUpTo1000RaiseFactor2 = 143,

        [Description("Дело пошло ОСНО от 1 млн. руб. до 2 млн. руб. повыш. коэфф.")]
        BusinessWentOsnoUpTo2000RaiseFactor2 = 144,

        [Description("Дело пошло ОСНО от 2 млн. руб. до 3 млн. руб. повыш. коэфф.")]
        BusinessWentOsnoUpTo3000RaiseFactor2 = 145,

        [Description("Дело пошло ОСНО от 3 млн. руб. до 4 млн. руб. повыш. коэфф.")]
        BusinessWentOsnoUpTo4000RaiseFactor2 = 146,

        [Description("Сбербанк Trial")]
        SberbankTrial = 147,

        [Description("Сбербанк Базовый")]
        SberbankBasic = 148,

        [Description("Сбербанк Премиальный")]
        SberbankPremium = 149,

        [Description("Мое дело Касса")]
        MoedeloCash = 150,

        [Description("УС УСН+ЕНВД Без сотрудников +")]
        ProfessionalUsnWithoutWorkersPlus = 151,

        [Description("УС УСН+ЕНВД До 5 сотрудников +")]
        ProfessionalUsnUpToFivePlus = 152,

        [Description("УС УСН+ЕНВД Максимальный +")]
        ProfessionalUsnMaxPlus = 153,

        [Description("УСН+ЕНВД Без сотрудников от 06.2017")]
        UsnWithoutWorkersFrom062017 = 154,

        [Description("УСН+ЕНВД До 5 сотрудников от 06.2017")]
        UsnUpToFiveFrom062017 = 155,

        [Description("УСН+ЕНВД Максимальный от 06.2017")]
        UsnMaxFrom062017 = 156,

        [Description("УСН+ЕНВД Без сотрудников + от 06.2017")]
        UsnWithoutWorkersPlusFrom062017 = 157,

        [Description("УСН+ЕНВД До 5 сотрудников + от 06.2017")]
        UsnUpToFivePlusFrom062017 = 158,

        [Description("УСН+ЕНВД Максимальный + от 06.2017")]
        UsnMaxPlusFrom062017 = 159,

        [Description("УС УСН+ЕНВД Без сотрудников от 06.2017")]
        ProfessionalUsnWithoutWorkersFrom062017 = 160,

        [Description("УС УСН+ЕНВД До 5 сотрудников от 06.2017")]
        ProfessionalUsnUpToFiveFrom062017 = 161,

        [Description("УС УСН+ЕНВД Максимальный от 06.2017")]
        ProfessionalUsnMaxFrom062017 = 162,

        [Description("УС УСН+ЕНВД Без сотрудников + от 06.2017")]
        ProfessionalUsnWithoutWorkersPlusFrom062017 = 163,

        [Description("УС УСН+ЕНВД До 5 сотрудников + от 06.2017")]
        ProfessionalUsnUpToFivePlusFrom062017 = 164,

        [Description("УС УСН+ЕНВД Максимальный + от 06.2017")]
        ProfessionalUsnMaxPlusFrom062017 = 165,

        [Description("ОСНО+ЕНВД Максимальный от 06.2017")]
        ProfessionalOsnoFrom062017 = 166,

        [Description("ОСНО+ЕНВД Максимальный + от 06.2017")]
        ProfessionalOsnoPlusFrom062017 = 167,

        [Description("Стартап")]
        Startup = 168,

        [Description("УСН+ЕНВД Максимальный Касса")]
        UsnMaxCash = 169,

        [Description("УС УСН+ЕНВД Максимальный Касса")]
        AccountingUsnMaxCash = 170,

        [Description("Регистрация ИП под ключ")]
        OutsourceIpRegistration = 171,

        [Description("Регистрация ООО под ключ")]
        OutsourceOooRegistration = 172,

        [Description("Бюро Фримиум")]
        OfficeFreemium = 173,

        [Description("Мое дело Касса Новый пользователь")]
        MoedeloCashNewUser = 174,

        [Description("Платёж по составному тарифу из системы 'Новый Биллинг' (платформа УС). См. право AccessRule.CompoundTariff")]
        CompoundTariff = 177,
        
        [Description("Платёж по составному тарифу из системы 'Новый Биллинг' (платформа БИЗ). См. право AccessRule.CompoundTariff")]
        CompoundBizTariff = 178,

        [Description("Аутсорсинг - Убер")]
        OutsourcingUber = 179,

        [Description("Аутсорсинг - Гольфстрим")]
        OutsourcingGolfstream = 180,

        [Description("ОСНО+ЕНВД Максимальный Касса")]
        OsnoMaxCash = 181,

        [Description("Дело пошло + Касса")]
        BusinessWentAndCash = 188,

        [Description("Бюро Cтандарт+Проверка ЮЛ и ФЛ")]
        OfficeStandartCheckPerson = 189,

        [Description("Бюро Проф+Проверка ЮЛ и ФЛ")]
        OfficeProCheckPerson = 190,

        [Description("Бюро Проверка контрагентов ЮЛ и ФЛ")]
        OfficeCheckKontragentAndCheckPerson = 191,

        [Description("УСН + ЕНВД До 5 сотрудников + Subtotal")]
        UsnSubtotalUpToFive = 192,

        [Description("УС УСН + ЕНВД До 5 сотрудников + Subtotal")]
        ProfessionalUsnSubtotalUpToFive = 193,

        [Description("Сбербанк ИП 6% без сотрудников")]
        SberbankIp6WithoutWorkers = 194,

        [Description("Доставка Кассы")]
        CashShipping = 195,

        [Description("Аутсорсинг- Регистрация бизнеса")]
        OutsourceRegistration = 197,

        [Description("УСН+ЕНВД Максимальный Демо")]
        UsnMaxDemo = 198,

        [Description("УСН+ЕНВД Максимальный + Subtotal")]
        UsnSubtotalMax = 199,

        [Description("УС УСН+ЕНВД Максимальный + Subtotal")]
        ProfessionalUsnSubtotalMax = 200,

        [Description("ОСНО+ЕНВД Максимальный + Subtotal")]
        ProfessionalOsnoSubtotalMax = 201,

        [Description("Дело-Банк Без сотрудников")]
        SkbbankProfessionalUsnWithoutWorkers = 202,

        [Description("Дело-Банк До 5 сотрудников")]
        SkbbankProfessionalUsnUpToFive = 203,

        [Description("Дело-Банк Максимальный")]
        SkbbankProfessionalUsnMax = 204,

        [Description("Дело-Банк ОСНО")]
        SkbbankProfessionalOsno = 205,

        [Description("Моя бухгалтерия Онлайн Без сотрудников v1.1")]
        SberbankWithoutWorkersV11 = 206,

        [Description("Моя бухгалтерия Онлайн C сотрудниками v1.1")]
        SberbankWithWorkersV11 = 207,

        [Description("Моя бухгалтерия Онлайн Максимальный v1.1")]
        SberbankMaxV11 = 208,

        [Description("ЗП и Кадры до 5 сотрудников")]
        OutsorceSalaryAndPersonalUpTo5Workers = 209,

        [Description("ЗП и Кадры до 10 сотрудников")]
        OutsorceSalaryAndPersonalUpTo10Workers = 210,

        [Description("ЗП и Кадры до 20 сотрудников")]
        OutsorceSalaryAndPersonalUpTo20Workers = 211,

        [Description("ЗП и Кадры до 50 сотрудников")]
        OutsorceSalaryAndPersonalUpTo50Workers = 212,

        [Description("Персональный бухгалтер + Касса v1.1")]
        OutsorcePersonalAccountantCashV11 = 213,

        [Description("Формирование декларации для ИП на УСН 6% без сотрудников")]
        SberbankDeclarationFormationForIp6WithoutWorkers = 214,

        [Description("Совместный тариф ПСБ - Мое дело v1.1")]
        PromsvyazbankJointV11 = 215,

        [Description("Активация кассы")]
        CashActivation = 216,

        [Description("Доступ к оператору фискальных данных")]
        CashFDOAccess = 217,

        [Description("Бесплатная регистрация в Москве")]
        FreeRegistrationMoscow = 218,

        [Description("API Бюро Базовый")]
        OfficeApiBasic = 219,

        [Description("API Бюро Комплексный")]
        OfficeApiComplex = 220,

        [Description("Ассистент")]
        Assistant = 221,

        [Description("Ассистент. Новый пользователь")]
        AssistantForNewUser = 222,

        [Description("Компонент кассы")]
        CashComponent = 223,

        [Description("Персональный бухгалтер ЕНВД")]
        PersonalAccountantEnvd = 224,

        [Description("Персональный бухгалтер. Посредник УСН")]
        PersonalAccountantAgentUsn = 225,

        [Description("Персональный бухгалтер. Посредник ОСНО")]
        PersonalAccountantAgentOsno = 226,

        [Description("Бэк-офис ОСНО от 4 млн. руб. до 6 млн. руб. повыш. коэфф.")]
        BackofficeOsnoUpTo6000RaiseFactor2 = 227,

        [Description("Мое Дело Партнер")]
        Partner = 228,

        [Description("ИБ УС \"Без сотрудников\" с расширенным Товароучетом")]
        ProductAccountingWithoutWorkers = 229,

        [Description("ИБ УС \"До 5 сотрудников\" с расширенным Товароучетом")]
        ProductAccountingUpToFive = 230,

        [Description("ИБ УС \"Максимальный\" с расширенным Товароучетом")]
        ProductAccountingMax = 231,

        [Description("ИБ ОСНО \"Максимальный\" с расширенным Товароучетом")]
        ProductAccountingOsnoMax = 232,

        [Description("Моя бухгалтерия Онлайн УС \"Без сотрудников\" v1.0")]
        SberbankAccountantWithOutWorkers = 233,

        [Description("Моя бухгалтерия Онлайн УС \"До 5 сотрудников\" v1.0")]
        SberbankAccountantWithWorkers = 234,

        [Description("Моя бухгалтерия Онлайн УС \"Максимальный\" v1.0")]
        SberbankAccountantMax = 235,

        [Description("УС ИП 6% без сотрудников v1.0")]
        SberbankAccountantIp6WithoutWorkers = 236,

        [Description("Персональный бухгалтер до 250 тыс. руб. v2.0")]
        PersonalAccountantUpTo250V2 = 237,

        [Description("Персональный бухгалтер от 250 до 500 тыс. руб. v2.0")]
        PersonalAccountantUpTo500V2 = 238,

        [Description("Персональный бухгалтер от 500 до 750 тыс. руб. v2.0")]
        PersonalAccountantUpTo750V2 = 239,

        [Description("Персональный бухгалтер ОСНО до 500 тыс. руб. v2.0")]
        PersonalAccountantOsnoUpTo500V2 = 240,

        [Description("Персональный бухгалтер ОСНО от 500 тыс. руб. до 750 тыс. руб. v2.0")]
        PersonalAccountantOsnoUpTo750V2 = 241,

        [Description("Персональный бухгалтер ОСНО до 500 тыс. руб. повыш. коэфф. v2.0")]
        PersonalAccountantOsnoUpTo500RaiseFactorV2 = 242,

        [Description("Персональный бухгалтер ОСНО от 500 тыс. руб. до 750 тыс. руб. повыш. коэфф. v2.0")]
        PersonalAccountantOsnoUpTo750RaiseFactorV2 = 243,

        [Description("Бэк-офис от 750 тыс. руб. до 1 млн. руб. v2.0")]
        BackOfficeUpTo1000V2 = 244,

        [Description("Бэк-офис от 1 млн. руб. до 2 млн. руб. v2.0")]
        BackOfficeUpTo2000V2 = 245,

        [Description("Бэк-офис от 2 млн. руб. до 3 млн. руб. v2.0")]
        BackOfficeUpTo3000V2 = 246,

        [Description("Бэк-офис от 3 млн. руб. до 4 млн. руб. v2.0")]
        BackOfficeUpTo4000V2 = 247,

        [Description("Бэк-офис (от 4 млн. до 6 млн.) v2.0")]
        BackOfficeUpTo6000V2 = 248,

        [Description("")]
        BackOfficeOsnoUpTo6000RaiseFactorV2 = 249,

        [Description("Бэк-офис ОСНО от 750 тыс. руб. до 1 млн. руб. v2.0")]
        BackOfficeOsnoUpTo750V2 = 250,

        [Description("Бэк-офис ОСНО от 750 тыс. руб. до 1 млн. руб. повыш. коэфф. v2.0")]
        BackOfficeOsnoUpTo750RaiseFactorV2 = 251,

        [Description("Бэк-офис ОСНО от 1 млн. руб. до 2 млн. руб. v2.0")]
        BackOfficeOsnoUpTo2000V2 = 252,

        [Description("Бэк-офис ОСНО от 1 млн. руб. до 2 млн. руб. повыш. коэфф. v2.0")]
        BackOfficeOsnoUpTo2000RaiseFactorV2 = 253,

        [Description("Бэк-офис ОСНО от 2 млн. руб. до 3 млн. руб. v2.0")]
        BackOfficeOsnoUpTo3000V2 = 254,

        [Description("Бэк-офис ОСНО от 2 млн. руб. до 3 млн. руб. повыш. коэфф. v2.0")]
        BackOfficeOsnoUpTo3000RaiseFactorV2 = 255,

        [Description("Бэк-офис ОСНО от 3 млн. руб. до 4 млн. руб. v2.0")]
        BackOfficeOsnoUpTo4000V2 = 256,

        [Description("Бэк-офис ОСНО от 3 млн. руб. до 4 млн. руб. повыш. коэфф. v2.0")]
        BackOfficeOsnoUpTo4000RaiseFactorV2 = 257,

        [Description("Мое дело команда v2.0")]
        MoedeloTeamV2 = 258,

        [Description("Решение для бизнеса УС До 5 сотрудников")]
        SberbankSolutionForBusinessAccWithWorkers = 259,

        [Description("УС УСН+ЕНВД Совместный тариф ПСБ - Мое дело")]
        PsbMoedeloUsn = 260,

        [Description("УС ОСНО+ЕНВД Совместный тариф ПСБ - Мое дело")]
        PsbMoedeloOsno = 261,

        [Description("Товароучет УСН")]
        ProductAccountingUsn = 262,

        [Description("Товароучет ОСНО")]
        ProductAccountingOsno = 263,

        [Description("Бэк-офис Торговля v2.0")]
        BackOfficeTrading = 264,

        [Description("Бэк-офис Торговля повыш. коэф. v2.0")]
        BackOfficeTradingRf = 265,

        [Description("ОСНО+ЕНВД До 5 сотрудников v1.1")]
        OsnoEnvdUpTo5Workers = 266,

        // 267,268 заняты под тарифы (см. БД, если это важно)

        [Description("Дело-Банк Отчётность")]
        SkbbankReporting = 269,

        [Description("РНКБ УСН+ЕНВД \"Без сотрудников\"")]
        RnkbUsnEnvdWithoutWorkers = 270,

        [Description("РНКБ УСН+ЕНВД \"До 5 сотрудников\"")]
        RnkbUsnEnvdWith5Workers = 271,

        [Description("РНКБ УСН+ЕНВД \"Максимальный\"")]
        RnkbUsnEnvdMax = 272,

        [Description("РНКБ ОСНО \"Максимальный\"")]
        RnkbOsnoMax = 273,

        // значения с 274 по 306 включительно заняты под последние версии тарифов ГУ (2.1). Мы попробуем сюда эти значения не добавлять

        [Description("Регистрация ИП/ООО УС")]
        MasterOfRegistrationAcc = 308,

        [Description("Моя бухгалтерия Онлайн ОСНО + ЕНВД")]
        SberbankAccountantOsnoEnvdV12 = 309,

        //с 310 по 313 заняты под тарифы

        [Description("Премиум")]
        Premium = 314,

        [Description("Премиум + Проверка контрагентов")]
        PremiumCheckContragent = 315,

        [Description("Премиум + Товарный учет")]
        PremiumPlus = 316,

        [Description("Премиум + Товарный учет + Проверка Контрагентов")]
        PremiumCheckContragentPlus = 317,

        [Description("Комфорт")]
        Comfort = 318,

        [Description("Комфорт + Проверка контрагентов")]
        ComfortCheckContragent = 319,

        [Description("Комфорт + Товарный учет")]
        ComfortPlus = 320,

        [Description("Комфорт + Товарный учет + Проверка Контрагентов")]
        ComfortCheckContragentPlus = 321,

        [Description("Эконом")]
        Econom = 322,

        [Description("Эконом + Проверка контрагентов")]
        EconomCheckContragent = 323,

        [Description("Эконом + Товарный учет")]
        EconomPlus = 324,

        [Description("Эконом + Товарный учет + Проверка Контрагентов")]
        EconomCheckContragentPlus = 325,

        [Description("Премиум ОСНО")]
        PremiumOsno = 326,

        [Description("Премиум ОСНО + Проверка контрагентов")]
        PremiumOsnoCheckContragent = 327,

        [Description("Премиум ОСНО + Товарный учет")]
        PremiumOsnoPlus = 328,

        [Description("Премиум ОСНО + Товарный учет + Проверка Контрагентов")]
        PremiumOsnoCheckContragentPlus = 329,

        [Description("Бизнес-ассистент")]
        BusinessAssistant = 330,

        [Description("Проф. бухгалтерия 5")]
        ProfessionalAccountant5 = 331,

        [Description("Проф. бухгалтерия ОСНО 5")]
        ProfessionalAccountantOsno5 = 335,

        [Description("ПростоБанк Бухгалтерия Мини")]
        ProstobankAccountantMini = 339,

        [Description("ПростоБанк Бухгалтерия Эконом")]
        ProstobankAccountantEconom = 340,

        [Description("ПростоБанк Бухгалтерия Комфорт")]
        ProstobankAccountantComfort = 341,

        [Description("ПростоБанк Бухгалтерия Премиум")]
        ProstobankAccountantPremium = 342,

        [Description("ПростоБанк Бухгалтерия Премиум ОСНО")]
        ProstobankAccountantPremiumOsno = 343,

        [Description("Ак Барс Банк Премиум")]
        AkbarsbankPremium = 347,

        [Description("Ак Барс Банк Премиум ОСНО")]
        AkbarsbankPremiumOsno = 348,

        [Description("Премиум + Управленческий учет")]
        PremiumManagementAccounting = 349,

        [Description("Премиум ОСНО + Управленческий учет")]
        PremiumOsnoManagementAccounting = 350,

        [Description("Премиум + Товарный учет + Управленческий учет")]
        PremiumManagementAccountingPlus = 351,

        [Description("Премиум ОСНО + Товарный учет + Управленческий учет")]
        PremiumOsnoManagementAccountingPlus = 352,

        [Description("Сервис закрытия ИП")]
        IpClosure = 413,

        [Description("Выпуск квалифицированной электронной подписи для ИП")]
        ElectronicSignatureIssueForIp = 414,

        [Description("Сервис ликвидации ИП")]
        IpElimination = 415,

        [Description("Ликвидация ИП/ООО")]
        IpOooLiquidation = 420,
        
        [Description("Переход с ЕНВД")]
        TransitionFromEnvd = 421,
        
        [Description("Пакет Базовый Freemium")]
        BaseFreemium = 460,

        [Description("Дело-Банк Без сотрудников 1 мес")]
        SkbbankProfessionalUsnWithoutWorkersMonth = 461,

        [Description("Дело-Банк До 5 сотрудников 1 мес")]
        SkbbankProfessionalUsnUpToFiveMonth = 462,

        [Description("Дело-Банк Максимальный 1 мес")]
        SkbbankProfessionalUsnMaxMonth = 463,

        [Description("Дело-Банк ОСНО 1 мес")]
        SkbbankProfessionalOsnoMonth = 464,

        [Description("Платёж по составному тарифу из системы 'Новый Биллинг' (платформа Бюро). См. право AccessRule.CompoundTariff")]
        CompoundSpsTariff = 465,

        [Description("Интеграция с банком для Финансиста")]
        FinControlIntegrationWithBanks = 470,

        [Description("Моя бухгалтерия Онлайн \"Максимальный\" v1.3")]
        SberbankMaxV13 = 529,
        
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников\" v1.3")]
        SberbankWithWorkersV13 = 530,
        
        [Description("Моя бухгалтерия Онлайн \"Без сотрудников\" v1.3")]
        SberbankWithoutWorkersV13 = 531,

        [Description("Моя бухгалтерия Онлайн \"Максимальный\" УС v1.3")]
        SberbankAccountantMaxV13 = 532,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный ОСНО\" УС v1.3")]
        SberbankAccountantMaxOsnoV13 = 533,
        
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников\" УС v1.3")]
        SberbankAccountantWithWorkersV13 = 534,
        
        [Description("Моя бухгалтерия Онлайн \"Без сотрудников\" УС v1.3")]
        SberbankAccountantWithoutWorkersV13 = 535,
        
        [Description("Пакет Премиум для клиентов ПАО Банк Открытие")]
        OpenbankPremium = 538,
        
        [Description("Пакет Премиум ОСНО для клиентов ПАО Банк Открытие")]
        OpenbankPremiumOsno = 539,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный\" УС v1.4")]
        SberbankAccountantMaxV14 = 547,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный ОСНО\" УС v1.4")]
        SberbankAccountantMaxOsnoV14 = 548,
        
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников\" УС v1.4")]
        SberbankAccountantWithWorkersV14 = 549,

        [Description("Моя бухгалтерия Онлайн \"Без сотрудников\" УС v1.4")]
        SberbankAccountantWithoutWorkersV14 = 550,
                
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников + МП\" УС v1.4")]
        SberbankAccountantWithWorkersAndMarketPlaceV14 = 551,
        
        [Description("Моя бухгалтерия Онлайн \"Без сотрудников + МП\" УС v1.4")]
        SberbankAccountantWithoutWorkersAndMarketPlaceV14 = 552,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный\" УС Акция")]
        SberbankAccountantMaxPromo = 561,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный ОСНО\" УС Акция")]
        SberbankAccountantMaxOsnoPromo = 562,
        
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников\" УС Акция")]
        SberbankAccountantWithWorkersPromo = 563,
        
        [Description("Моя бухгалтерия Онлайн \"Без сотрудников\" УС Акция")]
        SberbankAccountantWithoutWorkersPromo = 564,
        
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников + МП\" УС Акция")]
        SberbankAccountantWithWorkersAndMarketPlacePromo = 565,
        
        [Description("Моя бухгалтерия Онлайн \"Без сотрудников + МП\" УС Акция")]
        SberbankAccountantWithoutWorkersAndMarketPlacePromo = 566,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный\" УС v1.5")]
        SberbankAccountantMaxV15 = 569,
        
        [Description("Моя бухгалтерия Онлайн \"Максимальный ОСНО\" УС v1.5")]
        SberbankAccountantMaxOsnoV15 = 570,
        
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников\" УС v1.5")]
        SberbankAccountantWithWorkersV15 = 571,

        [Description("Моя бухгалтерия Онлайн \"Без сотрудников\" УС v1.5")]
        SberbankAccountantWithoutWorkersV15 = 572,
                
        [Description("Моя бухгалтерия Онлайн \"До 5 сотрудников + МП\" УС v1.5")]
        SberbankAccountantWithWorkersAndMarketPlaceV15 = 573,
        
        [Description("Моя бухгалтерия Онлайн \"Без сотрудников + МП\" УС v1.5")]
        SberbankAccountantWithoutWorkersAndMarketPlaceV15 = 574,
        
        [Description("БЮРО проф+ЭДО 999999")]
        BuroEdo = 999999
    }
}
