namespace Moedelo.Payroll.Enums.Ndfl
{
    public static class NdflRateTypeExtentions
    {
        public static decimal DecimalValue(this NdflRateType rate)
        {
            return (decimal) rate.IntValue() / 100;
        }

        public static int IntValue(this NdflRateType rate)
        {
            switch (rate)
            {
                case NdflRateType.NdflRate5:
                    return 5;
                case NdflRateType.NdflRate9:
                    return 9;
                case NdflRateType.NdflRate13:
                    return 13;
                case NdflRateType.NdflRate15:
                    return 15;
                case NdflRateType.NdflRate30:
                    return 30;
                case NdflRateType.NdflRate35:
                    return 35;
                case NdflRateType.NdflRate15OverLimit:
                    return 15;
                case NdflRateType.NdflRate18OverLimit:
                    return 18;
                case NdflRateType.NdflRate20OverLimit:
                    return 20;
                case NdflRateType.NdflRate22OverLimit:
                    return 22;
                default:
                    return 0;
            }
        }

        public static NdflRateType ToNdflRateType(this decimal rate)
        {
            switch (rate)
            {
                case 0.05m:
                    return NdflRateType.NdflRate5;
                case 0.09m:
                    return NdflRateType.NdflRate9;
                case 0.13m:
                    return NdflRateType.NdflRate13;
                case 0.15m:
                    return NdflRateType.NdflRate15;
                case 0.30m:
                    return NdflRateType.NdflRate30;
                case 0.35m:
                    return NdflRateType.NdflRate35;
                case 0.18m:
                    return NdflRateType.NdflRate18OverLimit;
                case 0.20m:
                    return NdflRateType.NdflRate20OverLimit;
                case 0.22m:
                    return NdflRateType.NdflRate22OverLimit;
                default:
                    return NdflRateType.NdflRate0;
            }
        }
        
        public static NdflRateType ToNdflRateType(this int rate)
        {
            switch (rate)
            {
                case 5:
                    return NdflRateType.NdflRate5;
                case 9:
                    return NdflRateType.NdflRate9;
                case 13:
                    return NdflRateType.NdflRate13;
                case 15:
                    return NdflRateType.NdflRate15;
                case 30:
                    return NdflRateType.NdflRate30;
                case 35:
                    return NdflRateType.NdflRate35;
                case 18:
                    return NdflRateType.NdflRate18OverLimit;
                case 20:
                    return NdflRateType.NdflRate20OverLimit;
                case 22:
                    return NdflRateType.NdflRate22OverLimit;
                default:
                    return NdflRateType.NdflRate0;
            }
        }

        public static bool IsResident(this NdflRateType rate) =>
            rate is NdflRateType.NdflRate13 or NdflRateType.NdflRate15OverLimit or NdflRateType.NdflRate18OverLimit
                or NdflRateType.NdflRate20OverLimit or NdflRateType.NdflRate22OverLimit;

        public static bool IsNonResidentWithDividends(this NdflRateType rate, bool isDividends) =>
            rate == NdflRateType.NdflRate15 && isDividends;
        
        public static bool IsBonusRate(this NdflRateType rate) =>
            rate is NdflRateType.NdflRate13 or NdflRateType.NdflRate15OverLimit;
    }
}
