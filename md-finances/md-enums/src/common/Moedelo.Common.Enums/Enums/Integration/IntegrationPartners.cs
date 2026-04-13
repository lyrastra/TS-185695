using System;

namespace Moedelo.Common.Enums.Enums.Integration
{

    [Obsolete("Для получения данных партнеров нужно использовать расширение из сборки IntegrationPartnersInfo репозитория bankIntegrations-shared")]
    public enum IntegrationPartners
    {
        TestMockAsync = -2,

        TestMock = -1,

        Undefined = 0,

        /// <summary> Лишён лицензии </summary>      
        SbBank = 1001,

        SapeRu = 1002,

        YandexMoney = 1003,

        Robokassa = 1004,

        AlfaBank = 1005,

        PsBank = 1006,

        SdmBank = 1007,

        Tochka = 1008,

        /// <summary> Отказались от неё, в пользу 1027 </summary>
        SberBankEasyFinance = 1009,

        LokoBank = 1010,

        FkOpen = 1011,

        Bss = 1012,

        /// <summary> Bss. Так и не стартанул. </summary>
        ThreeDBank = 1013,

        OpenBank = 1014,

        /// <summary> Был поглащён Бинбанком </summary>    
        MdmBank = 1015,

        /// <summary> Bss </summary>       
        IntesaBank = 1016,

        ModulBank = 1017,

        UralsibBank = 1018,

        /// <summary> Лишён лицензии </summary>
        IntercommerzBank = 1019,

        TinkoffBank = 1020,

        OtpBank = 1021,

        BinBank = 1022,

        Vtb24Bank = 1023,

        Evotor = 1024,

        /// <summary> ВТБ + экс Банк Москвы </summary> 
        VtbBank = 1025,

        YandexKassa = 1026,

        SberBank = 1027,

        Subtotal = 1028,

        LifePay = 1029,

        B2B = 1030,

        InSales = 1031,

        Skb = 1032,

        MyCashier = 1033,

        //Точка роста самовыпилилась, удалена из сервиса
        GrowthPoint = 1034,

        Bitrix24B2B = 1035,

        Appecs = 1036,

        Envybox = 1037,

        ModulKassa = 1038,

        Ekam = 1039,

        UralsibBankSso = 1040,

        AlfaBankSso = 1041,

        NbdBank = 1042,

        Bitrix24 = 1043,

        Kassatka = 1044,

        Entera = 1045,

        RncbBankSso = 1046,

        RaiffeisenBank = 1047,

        RosselhozBank = 1048,

        ProstoBank = 1049,

        SovComBank = 1050,

        BssSso = 1051,

        OpenBankSso = 1052,

        SalesapCRM = 1053,

        AkbarsBankSso = 1054,

        VostochniyBankSso = 1055,

        Mts = 1056,

        AvangardSso = 1057,

        Rosbank = 1058,

        TinkoffBankSso = 1059,

        UKassa = 1060,
    }
}