namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    /// <summary>
    /// File type
    /// </summary>
    public enum ErptUserRequisitesFileType
    {
        /// <summary> Довереность </summary>
        KonturWarrant = 1,

        /// <summary> Скан паспорта </summary>
        KonturPassport = 2,

        /// <summary> Соглашение с ПФР </summary>
        AstralPFRAgreement = 4,

        /// <summary>Файл ЭЦП от Астрала </summary>
        AstralResult = 9,

        /// <summary> Сведения о доверенности контура (XML)</summary>
        KonturWarrantInfo = 12,

        EdsReRequest = 13
    }
}
