using System;

namespace Moedelo.Common.Enums.Enums.UAC
{
    /// <summary>
    /// Keys for role names.
    /// </summary>
    public static class Role
    {
        /// <summary> Профессиональный аутсорсер </summary>
        [Obsolete("Такой роли у нас больше нет. Администратор на всех платформах CC_ADM")]
        public const string ProfOutsourceAdmin = "PO_ADM";

        /// <summary> Администратор </summary>
        public const string Admin = "ADM";

        /// <summary> Директор </summary>
        public const string Director = "DIR";

        /// <summary> Главный бухгалтер </summary>
        public const string SeniorBookkeeper = "S_BKR";

        /// <summary> Бухгалтер </summary>
        public const string Bookkeeper = "BKR";

        /// <summary> Бухгалтер по зарплате </summary>
        public const string BookkeeperPayroll = "BKR_PRL";

        /// <summary> Старший менеджер </summary>
        public const string SeniorManager = "S_MGR";

        /// <summary> Менеджер </summary>
        public const string Manager = "MGR";

        /// <summary> Кладовщик </summary>
        public const string Stockkeeper = "SKR";

        /// <summary> Директор (Аутсорс) </summary>
        public const string DirectorOutsource = "DIR_OUT";

        /// <summary> Директор (Аутсорс) Деньги + Документы </summary>
        public const string DirectorOutsourceMD = "DIR_OUTMD";

        /// <summary> Управленческий учет </summary>
        public const string ManagementAccounting = "ACC_MGR";
    }
}