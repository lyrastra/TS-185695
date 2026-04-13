namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    /// <summary> Тип фонда </summary>
    public enum FundType
    {
        /// <summary> Не установленно </summary>
        None = 0,

        /// <summary> ФНС </summary>
        FNS = 1,

        /// <summary> ПФР </summary>
        PFR = 2,

        /// <summary> СФР ФСС (Социальный Фонд России) </summary>
        FSS = 3,

        /// <summary> РосСтат </summary>
        ROSSTAT = 4,

        /// <summary> ФНС неформал </summary>
        FnsNeformal = 5,

        /// <summary>ФНС ИОН </summary>
        FnsIon = 6,

        /// <summary>ПФР неформал </summary>
        PfrNeformal = 7,

        /// <summary>Входящие письма ФНС (рассылка)</summary>
        FnsNeformalMailing = 8,

        /// <summary>Входящие письма ФНС (письмо НО)</summary>
        FnsNeformalLetter = 9,

        /// <summary>Входящие письма ПФР</summary>
        PfrNeformalLetter = 10,

        /// <summary>Росстат неформал</summary>
        RosstatNeformal = 11,

        /// <summary>Входящие письма Росстат</summary>
        RosstatNeformalLetter = 12,

        /// <summary>ФСС СЭДО</summary>
        FssSedo = 13,
        
        /// <summary>СФР ПФР (Социальный Фонд России) </summary>
        SFR = 14
    }
}
