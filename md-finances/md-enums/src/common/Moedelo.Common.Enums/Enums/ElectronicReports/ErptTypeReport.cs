namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum ErptTypeReport
    {
        None = 0,

        /// <summary>Ручная отправка</summary>
        AdminDocument = 1,

        /// <summary>Отправка из ЛК(обычный отчёт)</summary>
        FormalDocument = 2,

        /// <summary>Отправка неформализованной отчётности и ИОН</summary>
        NeformalDocument = 3,

        /// <summary>Отправка из ЛК(пользовательский файл)</summary>
        File = 4
    }
}