using System;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.Shared.Enums.Extensions;

public static class ChargeTypeExtensions
{
    public static ChargeTypeCode GetChargeTypeCode(this ChargeType chargeType)
    {
        return (int)chargeType switch
        {
           < 200  => ChargeTypeCode.GeneralSalary,
           >= 200 and < 300  => ChargeTypeCode.Bonuses,
           >= 300 and < 400  => ChargeTypeCode.Premium,
           >= 400 and < 500  => ChargeTypeCode.MaterialAid,
           >= 500 and < 600  => ChargeTypeCode.Vacation,
           >= 600 and < 700  => ChargeTypeCode.SickList,
           >= 700 and < 800  => ChargeTypeCode.OtherAbsence,
           >= 800 and < 900  => ChargeTypeCode.Presence,
           >= 900 and < 1000  => ChargeTypeCode.Fired,
           >= 1000 and < 1100  => ChargeTypeCode.OtherIncome,
           >= 1100 and < 1200  => ChargeTypeCode.Deduction,
           >= 1200 and < 1300  => ChargeTypeCode.Advance,
           >= 1300 and < 1400  => ChargeTypeCode.Tax,
           >= 1400 and < 1500  => ChargeTypeCode.WorkContract,
           >= 1500 and < 1600  => ChargeTypeCode.Custom,
           _ => throw new ArgumentOutOfRangeException(nameof(chargeType), chargeType, null)
        };
    }

    public static ChargeTypeCode? GetChargeTypeCode(this ChargeType? chargeType)
    {
        return chargeType?.GetChargeTypeCode();
    }
}