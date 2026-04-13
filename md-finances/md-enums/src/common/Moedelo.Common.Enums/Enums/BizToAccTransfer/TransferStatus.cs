namespace Moedelo.Common.Enums.Enums.BizToAccTransfer
{
    public enum TransferStatus
    {
        InQueue = 0,

        Success = 1,

        UnknownError = 2,

        NotOnBizTariff = 3,

        PriceListNotContainInPriceListMap = 4,

        NoAnyPayment = 5,

        AlreadyTransferred = 6,

        ExpiredPayment = 7,

        CurrencyNotSupportedInAcc = 8,

        UserNotFound = 9,

        NoAnyTaxationSystems = 10,

        LegalFirmNotFound = 11,

        Cancelled = 12
    }
}