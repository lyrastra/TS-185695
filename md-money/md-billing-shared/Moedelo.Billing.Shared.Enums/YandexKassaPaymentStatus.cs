namespace Moedelo.Billing.Shared.Enums;

public enum YandexKassaPaymentStatus
{
    None = 0,
    Pending = 1,
    WaitingForCapture = 2,
    Succeeded = 3,
    Canceled = 4,
}