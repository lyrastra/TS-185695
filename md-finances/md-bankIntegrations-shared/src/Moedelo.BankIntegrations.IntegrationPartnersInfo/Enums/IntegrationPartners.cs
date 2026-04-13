using Moedelo.BankIntegrations.IntegrationPartnersInfo.Attributes;

namespace Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums
{
    public enum IntegrationPartners
    {

        [IntegrationPartnerInfo("", "", "", "", "", IsCanBeIntegrated = false)]
        TestMockAsync = -2,

        [IntegrationPartnerInfo("", "", "", "", "", IsCanBeIntegrated = false, IsOtp = true)]
        TestMock = -1,

        Undefined = 0,

        /// <summary> Лишён лицензии </summary>
        [IntegrationPartnerInfo("СБ Банк", "СБ Банка", "сб банк", "", "SbBankIntegrationTurn()", IsCanBeIntegrated = false, IsBank = true)]
        SbBank = 1001,

        [IntegrationPartnerInfo("Sape.Ru", "Sape.Ru", "", "", "ShowSapeDialog()", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = false, IsBank = false)]
        SapeRu = 1002,

        [IntegrationPartnerInfo("Яндекс.Деньги", "Яндекс.Деньги", "", "", "", IsCanBeIntegrated = false, IsBank = false)]
        YandexMoney = 1003,

        [IntegrationPartnerInfo("Робокасса", "Робокассы", "робокасса", "", "RoboIntegrationTurn()", IsCanBeIntegrated = true, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Robokassa = 1004,

        [IntegrationPartnerInfo("Альфа-Банк", "Альфа-Банка", "альфа", "системе \"Альфа-Бизнес Онлайн\"", "AlphaBankIntegrationTurn()", IsCanBeIntegrated = false, IsBank = true)]
        AlfaBank = 1005,

        [IntegrationPartnerInfo("ПАО \"Банк ПСБ\"", "ПАО \"Банка ПСБ\"", "\"банк псб\"", "ПАО \"Банке ПСБ\"", "PsBankIntegrationTurn()", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, BankLicenseNumbers = [3251])]
        PsBank = 1006,

        [IntegrationPartnerInfo("СДМ-Банк (старый)", "СДМ-Банка (старый)", "сдм-банк", "СДМ-БАНКЕ (старый)", "SdmBankIntegrationTurn()", IsCanBeIntegrated = false, IsBank = true)]
        SdmBank = 1007,

        [IntegrationPartnerInfo("Точка банк", "банка Точка", "точка", "Банке Точка", "TochkaIntegrationTurn()", IsCanBeIntegrated = false, IsBank = true)]
        Tochka = 1008,

        /// <summary> Отказались от неё, в пользу 1027 </summary>
        [IntegrationPartnerInfo("Сбербанк (отключено)", "Сбербанка (отключено)", "сбербанк", "Сбербанке (отключено)", "SberBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true, IsOtp = true)]
        SberBankEasyFinance = 1009,

        [IntegrationPartnerInfo("ЛокоБанк", "ЛокоБанка", "локо-банк", "ЛокоБанке", "LokoBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        LokoBank = 1010,

        [IntegrationPartnerInfo("ФК Открытие", "ФК Открытие", "фк открытие", "ФК Открытие", "FkOpenIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        FkOpen = 1011,

        [IntegrationPartnerInfo("Bss", "Bss", "", "", "", IsCanBeIntegrated = false, IsBank = true)]
        Bss = 1012,

        /// <summary> Bss. Так и не стартанул. </summary>
        [IntegrationPartnerInfo("ТРОЙКА-Д банк", "ТРОЙКА-Д банка", "тройка-д банк", "", "", IsCanBeIntegrated = false, IsBank = true)]
        ThreeDBank = 1013,

        [IntegrationPartnerInfo("Открытие", "Открытие", "банк открытие", "Банке Открытие", "OpenBankIntegrationTurn()", IsCanBeIntegrated = false, IsBank = true)]
        OpenBank = 1014,

        /// <summary> Был поглащён Бинбанком </summary>
        [IntegrationPartnerInfo("ФК Открытие (бывший Бинбанк, МДМ)", "ФК Открытие (бывший Бинбанк, МДМ)", "мдм банк", "ФК Открытие (бывший Бинбанк, МДМ)", "MdmBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        MdmBank = 1015,

        /// <summary> Bss </summary>
        [IntegrationPartnerInfo("Интеза банк", "Интеза банка", "интеза", "Банке Интеза", "ShowIntesaBankDialog()", IsCanBeIntegrated = false, IsBank = true)]
        IntesaBank = 1016,

        [IntegrationPartnerInfo("Модульбанк", "Модульбанка", "модульбанк", "Модульбанке", "ModulBankIntegrationTurnOn()", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, BankLicenseNumbers = [1927])]
        ModulBank = 1017,

        [IntegrationPartnerInfo("Уралсиб банк", "Уралсиб банка", "уралсиб", "Уралсиб Банке", "UralsibBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        UralsibBank = 1018,

        /// <summary> Лишён лицензии </summary>
        [IntegrationPartnerInfo("Интеркоммерц банк", "Интеркоммерц банка", "интеркоммерц", "Интеркоммерц Банке", "IntercommerzBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        IntercommerzBank = 1019,

        [IntegrationPartnerInfo("Тинькофф Банк (старый)", "Тинькофф банка (старый)", "тинькофф", "Тинькофф Банке (старый)", "TinkoffBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        TinkoffBank = 1020,

        [IntegrationPartnerInfo("ОТП банк (old)", "ОТП банка", "отп банк", "ОТП Банке", "OtpBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsIntegrationMonitor = false, IsBank = true)]
        OtpBank = 1021,

        [IntegrationPartnerInfo("ФК Открытие (бывший Бинбанк)", "ФК Открытие (бывший Бинбанк)", "бинбанк", "ФК Открытие (бывший Бинбанк)", "BinBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        BinBank = 1022,

        [IntegrationPartnerInfo("ВТБ 24", "ВТБ 24", "втб 24", "ВТБ 24", "", IsCanBeIntegrated = false, IsBank = true)]
        Vtb24Bank = 1023,

        [IntegrationPartnerInfo("Эвотор", "Эвотора", "эвотор", "Эвоторе", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Evotor = 1024,

        /// <summary> ВТБ + экс Банк Москвы </summary>
        [IntegrationPartnerInfo("ВТБ (beta)", "ВТБ (beta)", "банк%втб", "ВТБ (beta)", "VtbBankIntegrationTurnOn()", IsCanBeIntegrated = false, IsBank = true)]
        VtbBank = 1025,

        [IntegrationPartnerInfo("Яндекс.Касса", "Яндекс.Кассы", "яндекс.касса", "Яндекс.Кассе", "YandexKassaIntegrationTurnOn()", IsCanBeIntegrated = true, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        YandexKassa = 1026,

        [IntegrationPartnerInfo("Сбербанк", "Сбербанка", "сбербанк", "Сбербанке", "", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, TypePartner = TypeIntegration.IsBoth, BankLicenseNumbers = [1481])]
        SberBank = 1027,

        [IntegrationPartnerInfo("Сабтотал", "Сабтотала", "сабтотал", "Сабтотале", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Subtotal = 1028,

        [IntegrationPartnerInfo("LifePay", "LifePay", "LifePay", "LifePay", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        LifePay = 1029,

        [IntegrationPartnerInfo("B2B", "B2B", "B2B", "B2B", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        B2B = 1030,

        [IntegrationPartnerInfo("InSales", "InSales", "InSales", "InSales", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        InSales = 1031,

        [IntegrationPartnerInfo("СКБ-Банк", "СКБ-Банка", "банк синара", "СКБ-Банке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, TypePartner = TypeIntegration.IsUsedWL, BankLicenseNumbers = [705])]
        Skb = 1032,

        [IntegrationPartnerInfo("Мой Кассир", "Моего Кассира", "мой кассир", "Моем кассире", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        MyCashier = 1033,

        //Точка роста самовыпилилась, удалена из сервиса
        [IntegrationPartnerInfo("Точка роста + Мой Склад", "Точки роста + Моего Склада", "точка роста + мой склад", "Точке роста + Моем Складе", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        GrowthPoint = 1034,

        [IntegrationPartnerInfo("Bitrix24B2B", "Bitrix24B2B", "Bitrix24B2B", "Bitrix24B2B", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Bitrix24B2B = 1035,

        [IntegrationPartnerInfo("Appecs", "Appecs", "Appecs", "Appecs", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Appecs = 1036,

        [IntegrationPartnerInfo("Envybox", "Envybox", "Envybox", "Envybox", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Envybox = 1037,

        [IntegrationPartnerInfo("Модуль Касса", "Модуль Кассы", "модуль касса", "Модуль кассе", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        ModulKassa = 1038,

        [IntegrationPartnerInfo("Ekam", "Ekam", "ekam", "Ekam", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Ekam = 1039,

        [IntegrationPartnerInfo("Уралсиб банк", "Уралсиб банка", "уралсиб", "Уралсиб Банке","" , IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [2275])]
        UralsibBankSso = 1040,

        [IntegrationPartnerInfo("Альфа-Банк (new)", "Альфа-Банка (new)", "альфа", "системе \"Альфа-Бизнес Онлайн\" (new)", "", IsCanBeIntegrated = false, IsBank = true, BankLicenseNumbers = [1326])]
        AlfaBankSso = 1041,

        [IntegrationPartnerInfo("НБД-Банк", "НБД-Банка", "нбд", "НБД-Банке", "NbdBankIntegrationTurn()", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [1966])]
        NbdBank = 1042,

        [IntegrationPartnerInfo("Bitrix24", "Bitrix24", "Bitrix24", "Bitrix24", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Bitrix24 = 1043,

        [IntegrationPartnerInfo("Kassatka", "Kassatka", "Kassatka", "Kassatka", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Kassatka = 1044,

        [IntegrationPartnerInfo("Entera", "Entera", "Entera", "Entera", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Entera = 1045,

        [IntegrationPartnerInfo("РНКБ банк", "РНКБ банка", "рнкб", "РНКБ банке", "", IsCanBeIntegrated = false, IsBank = true)]
        RncbBankSso = 1046,

        [IntegrationPartnerInfo("Райффайзенбанк", "Райффайзенбанка", "райффайзен", "Райффайзенбанке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [3292])]
        RaiffeisenBank = 1047,

        [IntegrationPartnerInfo("Россельхозбанк (старый)", "Россельхозбанка (старый)", "россельхоз", "Россельхозбанке (старый)", "", IsCanBeIntegrated = false, IsBank = true)]
        RosselhozBank = 1048,

        [IntegrationPartnerInfo("ПростоБанк", "ПростоБанка", "\"куб\"", "", "", IsCanBeIntegrated = false, IsBank = true)]
        ProstoBank = 1049,

        [IntegrationPartnerInfo("Совкомбанк", "Совкомбанка", "совкомбанк", "Совкомбанке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [963])]
        SovComBank = 1050,

        [IntegrationPartnerInfo("Bss sso", "Bss sso", "bss sso", "", "", IsCanBeIntegrated = false, IsBank = true)]
        BssSso = 1051,

        [IntegrationPartnerInfo("ФК Открытие \"Бизнес Портал\"", "ФК Открытия \"Бизнес Портал\"", "фк открытие", "Открытии", "", IsCanBeIntegrated = false, IsBank = true)]
        OpenBankSso = 1052,

        [IntegrationPartnerInfo("SalesapCRM", "SalesapCRM", "SalesapCRM", "SalesapCRM", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        SalesapCRM = 1053,

        [IntegrationPartnerInfo("Ак Барс банк", "Ак Барс банка", "ак барс", "", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, TypePartner = TypeIntegration.IsUsedWL, BankLicenseNumbers = [2590])]
        AkbarsBankSso = 1054,

        [IntegrationPartnerInfo("Восточный банк", "Восточного банка", "восточный", "", "", IsCanBeIntegrated = false, IsBank = true)]
        VostochniyBankSso = 1055,

        [IntegrationPartnerInfo("МТС", "МТС", "МТС", "МТС", "", IsCanBeIntegrated = false, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        Mts = 1056,

        [IntegrationPartnerInfo("Авангард", "Авангарда", "авангард", "Авангарде", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [2879])]
        AvangardSso = 1057,

        [IntegrationPartnerInfo("Росбанк", "Росбанка", "росбанк", "Росбанке", "", IsCanBeIntegrated = false, IsBank = true)]
        Rosbank = 1058,

        [IntegrationPartnerInfo("ТБанк", "ТБанка", "ао \"тбанк\"", "ТБанке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [2673])]
        TinkoffBankSso = 1059,

        [IntegrationPartnerInfo("ЮKassa", "ЮKassы", "юkassa", "ЮKassе", "YandexKassaIntegrationTurnOn()", IsCanBeIntegrated = true, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        YMoney = 1060,

        [IntegrationPartnerInfo("СДМ-Банк", "СДМ-Банка", "сдм-банк", "СДМ-БАНКЕ", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [1637])]
        SdmBankSso = 1061,

        /// <summary>Удален с сервиса. MtsBankSso - не корректное название. Нужно было называть MtsYourBusiness. 1062 - это не банк.</summary>
        [IntegrationPartnerInfo("Мтс-Бизнесс", "Мтс-Бизнесса", "мтс-бизнесс", "Мтс-Бизнессе", "", IsCanBeIntegrated = false, IsBank = false)]
        MtsBankSso = 1062,
        
        [IntegrationPartnerInfo("Челиндбанк", "Челиндбанка", "челиндбанк", "Челиндбанке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [485])]
        ChelindBank = 1063,

        [IntegrationPartnerInfo("Точка", "Точка", "точка", "Точке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [3545])]
        PointBank = 1064,

        [IntegrationPartnerInfo("Бланк", "Бланка", "бланк", "Бланке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [2368])]
        BlancBank = 1065,
        
        [IntegrationPartnerInfo("Ренессанс Банк", "Ренессанса банка", "ренессанс", "Ренессансе Банке", "", IsCanBeIntegrated = false, IsBank = true)]
        RenCreditBank = 1066,

        /// <summary>МТС Банк</summary>
        [IntegrationPartnerInfo("МТС-Банк", "МТС-Банка", "мтс-банк", "МТС-Банке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [2268])]
        MobileTelesystemsBank = 1067,
        
        [IntegrationPartnerInfo("СберРешения", "СберРешений", "сберрешения", "СберРешениях", "", IsCanBeIntegrated = false, IsBank = true)]
        SberSolutions = 1068,
        
        [IntegrationPartnerInfo("Wildberries", "Wildberries", "wildberries", "Wildberries", "", IsCanBeIntegrated = true, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        WildberriesMarketplace = 1069,
        
        [IntegrationPartnerInfo("Ozon", "Ozon", "ozon", "Ozon", "", IsCanBeIntegrated = true, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        OzonMarketplace = 1070,
        
        [IntegrationPartnerInfo("Альфа-Банк", "Альфа-Банка", "альфа", "Альфа-Банке", "", IsCanBeIntegrated = true, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [1326])]
        Alfa = 1071,

        [IntegrationPartnerInfo("Яндекс Маркет", "Яндекс Маркета", "яндекс маркет", "Яндекс Маркете", "", IsCanBeIntegrated = true, UsePaymentSystemIntegrationUI = true, IsBank = false)]
        YandexMarket = 1072,
        
        [IntegrationPartnerInfo("Россельхозбанк", "Россельхозбанка", "россельхозбанк", "Россельхозбанке", "", IsCanBeIntegrated = true, InDevelopment = false, IsBank = true, IsIntegrationMonitor = true, BankLicenseNumbers = [3349])]
        Rshb = 1073,

        [IntegrationPartnerInfo("Совкомбанк WL", "Совкомбанка", "совкомбанк", "Совкомбанке", "", IsCanBeIntegrated = true, IsBank = true, InDevelopment = true, IsIntegrationMonitor = true, TypePartner = TypeIntegration.IsUsedWL, BankLicenseNumbers = [963])]
        SovComBankWl = 1074,

        [IntegrationPartnerInfo("ОТП банк", "ОТП банка", "отп банк", "ОТП Банке", "", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, InDevelopment = true, BankLicenseNumbers = [2766])]
        OtpBankSso = 1075,
        
        [IntegrationPartnerInfo("Инго банк", "Инго банка", "инго", "Инго банке", "", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, InDevelopment = false, BankLicenseNumbers = [2307])]
        Ingo = 1076,

        [IntegrationPartnerInfo("Вайлдберриз Банк", "Вайлдберриз банка", "вайлдберриз банк", "Вайлдберриз банке", "", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, InDevelopment = true, BankLicenseNumbers = [841])]
        WbBankWl = 1077,
        
        [IntegrationPartnerInfo("Прио-Внешторгбанк", "Прио-Внешторгбанка", "прио-внешторгбанк", "Прио-Внешторгбанке", "", IsCanBeIntegrated = true, IsIntegrationMonitor = true, IsBank = true, InDevelopment = false, BankLicenseNumbers = [212])]
        Vneshtorg = 1078,
    }
}
