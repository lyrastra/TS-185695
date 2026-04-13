using System.Linq;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentCalendar;

public static class PaymentCalendarChargeExtensions
{
    private static readonly int[] ByAccountablePersonCodes =
    {
        (int)PresenceType.BusinessTripExpenses,
        (int)PresenceType.BusinessTripAdvanceExpenses,
        (int)PresenceType.BusinessTripOverDailyAllowances
    };
    
    private static readonly ParentChargeTypeCode[] RegularOtherIncomeCodes =
    {
        ParentChargeTypeCode.OtherIncome,
        ParentChargeTypeCode.Custom
    };

    private static readonly int[] AllowanceForChildCodes =
    {
        (int)SicklistChargeType.AllowanceForOneChild,
        (int)SicklistChargeType.AllowanceForSecondChild,
        (int)SicklistChargeType.AllowanceForOneChildOld,
        (int)SicklistChargeType.AllowanceForSecondChildOld
    };  
    public static bool IsGpd(this PaymentCalendarChargeDto charge)
    {
        return charge.Type is { ParentCode: ParentChargeTypeCode.Gpd };
    }

    public static bool IsDividends(this PaymentCalendarChargeDto charge)
    {
        return charge.Type != null && (charge.Type.Code == (int)OtherIncomeType.Dividends ||
                                       charge.Type.ParentCode == ParentChargeTypeCode.Custom &&
                                       charge.Type.NdflCode == (int)NdflChargeCode.Dividends);
    }

    public static bool IsParticularInCalendar(this PaymentCalendarChargeDto charge)
    {
        return charge.IsGpd() || charge.IsDividends() || charge.IsChargeAccountablePerson() || charge.IsPayKontragent;
    }
    
    public static bool IsChargeAccountablePerson(this PaymentCalendarChargeDto charge)
    {
        return charge.Type != null && ByAccountablePersonCodes.Contains(charge.Type.Code);
    }
    
    public static bool IsSalaryScale(this PaymentCalendarChargeDto charge)
    {
        return charge.Type is { ParentCode: ParentChargeTypeCode.Salary };
    }

    public static bool IsRegularOtherIncome(this PaymentCalendarChargeDto charge)
    {
        return charge.Type != null && charge.IsRegular && RegularOtherIncomeCodes.Contains(charge.Type.ParentCode);
    }

    public static bool IsPresence(this PaymentCalendarChargeDto charge)
    {
        return charge.Type is { ParentCode: ParentChargeTypeCode.Presence };
    }

    public static bool IsRegularPremium(this PaymentCalendarChargeDto charge)
    {
        return charge.Type != null && charge.IsRegular && charge.Type.ParentCode == ParentChargeTypeCode.Premium;
    }

    public static bool IsAdvance(this PaymentCalendarChargeDto charge)
    {
        return charge.Type is { ParentCode: ParentChargeTypeCode.Advance };
    }
    
    public static bool IsSickList(this PaymentCalendarChargeDto charge)
    {
        return charge.Type is { ParentCode: ParentChargeTypeCode.SickList };
    }
    
    public static bool IsAllowanceForChild(this PaymentCalendarChargeDto charge)
    {
        return charge.Type != null && AllowanceForChildCodes.Contains(charge.Type.Code);
    }

    public static bool IsVacation(this PaymentCalendarChargeDto charge)
    {
        return charge.Type is { ParentCode: ParentChargeTypeCode.Vacation };
    }
}