namespace Moedelo.Common.Enums.Enums.Role
{
    public enum UserRole
    {
        /// <summary> Соответствует таблице uac.Role </summary>

        /// <summary> Полный доступ </summary>
        Admin = 1,

        /// <summary> Бухгалтер </summary>
        Bookkeeper = 2,

        /// <summary> Бухгалтер по зарплате </summary>
        BookkeeperPayroll = 3,

        /// <summary> Директор </summary>
        Director = 4,

        /// <summary> Менеджер </summary>
        Manager = 5,

        /// <summary> Главный бухгалтер </summary>
        SeniorBookkeeper = 6,

        /// <summary> Старший менеджер </summary>
        SeniorManager = 7,

        /// <summary> Кладовщик </summary>
        Stockkeeper = 8,

        /// <summary> Наблюдатель </summary>
        Watcher = 17,

        /// <summary> Директор чтение </summary>
        DirectorOutsource = 18,

        /// <summary> Директор документы </summary>
        DirectorOutsourceMD = 19,

        /// <summary> Управленческий учет </summary>
        ManagementAccounting = 20
    }
}
