using System;

namespace Moedelo.Common.Enums.Enums.YandexKassaPayments
{
    public enum YandexKassaPaymentStatus
    {
        None = 0,
        Pending = 1,
        WaitingForCapture = 2,
        Succeeded = 3,
        Canceled = 4,
    }

    public static class YandexKassaPaymentStatusEnumMapper
    {
        public static YandexKassaPaymentStatus StringToStatusEnum(string status)
        {
            switch (status)
            {
                case "pending":
                    return YandexKassaPaymentStatus.Pending;
                case "waiting_for_capture":
                    return YandexKassaPaymentStatus.WaitingForCapture;
                case "succeeded":
                    return YandexKassaPaymentStatus.Succeeded;
                case "canceled":
                    return YandexKassaPaymentStatus.Canceled;
                default:
                    return YandexKassaPaymentStatus.None;
            }
        }

        public static string StatusEnumToString(this YandexKassaPaymentStatus status)
        {
            switch (status)
            {
                case YandexKassaPaymentStatus.Pending:
                    return "pending";
                case YandexKassaPaymentStatus.WaitingForCapture:
                    return "waiting_for_capture";
                case YandexKassaPaymentStatus.Succeeded:
                    return "succeeded";
                case YandexKassaPaymentStatus.Canceled:
                    return "canceled";
                default:
                    throw new Exception("Unsupported YooKassaPaymentStatus");
            }
        }
    }
}