using System;

namespace Moedelo.Common.Enums.Enums
{
    /// <summary>
    /// Products provided by ООО "Моё дело"
    /// </summary>
    public static class ProductPlatform
    {
        public const string Biz = "BIZ";
        public const string Accounting = "ACC";
        public const string Sps = "SPS";
        public const string ProfOutsource = "PO";
        public const string Con = "CON"; // Бюро Индивидуальный
        public const string Prt = "PRT"; // Мое Дело Партнер
        [Obsolete("Use Prt value instead")]
        public const string Rtp = Prt; // Мое Дело Партнер
    }
}