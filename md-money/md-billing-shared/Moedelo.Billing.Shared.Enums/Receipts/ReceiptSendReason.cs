namespace Moedelo.Billing.Shared.Enums.Receipts;

public enum ReceiptSendReason
{
    /// <summary> Автоматически распознанный платеж при импорте выписки </summary>
    AutoRecognizedPayment,
    /// <summary> Ручное распознание платежа </summary>
    ManualRecognizedPayment,
    /// <summary> Платеж распознан по частям </summary>
    RecognizedPaymentByParts,
    /// <summary> Нераспознанный платеж </summary>
    NotRecognizedPayment,
    /// <summary> Успешный платеж в ЮКассе </summary>
    YooKassaPaymentSucceeded,
    /// <summary> Успешный платеж в YaPay </summary>
    YaPayOrderCaptured,
}