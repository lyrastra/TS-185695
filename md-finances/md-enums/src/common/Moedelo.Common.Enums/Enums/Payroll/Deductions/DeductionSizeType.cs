namespace Moedelo.Common.Enums.Enums.Payroll.Deductions
{
    public enum DeductionSizeType : byte
    {
        /// <summary> % от дохода </summary>
        Percent = 0,
        /// <summary> Доля дохода </summary>
        Proportion = 1,
        /// <summary> Конкретная сумма </summary>
        Sum = 2
    }
}