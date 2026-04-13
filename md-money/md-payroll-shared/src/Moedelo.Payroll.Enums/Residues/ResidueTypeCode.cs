namespace Moedelo.Payroll.Enums.Residues
{
    public enum ResidueTypeCode
    {
        Default = -1,

        None = 0,

        /// <summary> Социальный налоговый вычет </summary>
        Social = 1,

        /// <summary> Стандартный налоговый вычет </summary>
        Standart = 2,

        /// <summary> Профессиональный налоговый вычет </summary>
        Professional = 3,

        /// <summary> Имущественный налоговый вычет </summary>
        Propery = 4,

        /// <summary> Авансы по патенту </summary>
        AdvancePatent = 5,

        /// <summary> Долгосрочные сбережения </summary>
        LongTermSavings = 6
    }
}
