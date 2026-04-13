namespace Moedelo.Common.Enums.Enums.Calendar
{
    /// <summary> Тип ивента </summary>
    public enum CalendarEventType
    {

        /// <summary> Ивент для всех пользователей </summary>
        AllUserEvent = 1,

        /// <summary> Ивент для пользователя </summary>
        UserEvent = 2,
        
        /// <summary>
        /// Событие аутсорсеров для пользователя
        /// </summary>
        OutsourceUserEvent = 3

    }
}
