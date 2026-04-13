namespace Moedelo.Requisites.Enums.Calendar
{
    /// <summary> Статус ивента </summary>
    public enum CalendarEventStatus
    {
        /// <summary> Ивент Не создан </summary>
        NotCreated = 0,

        /// <summary> Ивент создан </summary>
        Created = 1,

        /// <summary> Перенесён </summary>
        MoveToNextQuarter = 2,

        /// <summary>
        /// Перенесен
        /// </summary>
        Moved = 20,

        /// <summary> Завершен </summary>
        Complete = 100,

        /// <summary> Завершен, но не должен быть видим пользователю </summary>
        Hidden = 101,

        /// <summary> Ивент удалён </summary>
        Deleted = 200,
    }
}
