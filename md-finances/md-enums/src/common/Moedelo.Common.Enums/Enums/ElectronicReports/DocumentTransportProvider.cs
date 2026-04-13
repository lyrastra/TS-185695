namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    /// <summary>
    /// Сервис для отправки отчетности
    /// </summary>
    public enum DocumentTransportProvider
    {
        /// <summary>
        /// Для тестирования
        /// </summary>
        Test = -1,

        /// <summary> Не определённ </summary>
        NotDefined = 0,

        /// <summary> Траст </summary>
        Trust = 1,

        /// <summary> Астрал </summary>
        Astral = 2,

        /// <summary> Usb token </summary>
        TrustUsbToken = 3,
    }
}