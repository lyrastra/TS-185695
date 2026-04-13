using System.Linq;

namespace Moedelo.Billing.Shared.Enums.Extentions;

public static class PaymentMethodTypeExtentions
{
    private static readonly PaymentMethodType[] RealPaymentTypes = new[]
    {
        PaymentMethodType.SettlementAccount,
        PaymentMethodType.EMoney,
    };

    private static readonly PaymentMethodType[] TechnicalPaymentTypes = new[]
    {
        PaymentMethodType.Technical,
        PaymentMethodType.Free,
    };

    public static bool IsRealPaymentMethodType(this PaymentMethodType paymentMethodType)
    {
        return RealPaymentTypes.Contains(paymentMethodType);
    }

    public static bool IsTechnicalPaymentMethodType(this PaymentMethodType paymentMethodType)
    {
        return TechnicalPaymentTypes.Contains(paymentMethodType);
    }
}