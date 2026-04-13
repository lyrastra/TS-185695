namespace Moedelo.HistoricalLogs.Enums
{
    public enum OutsourceCalendarLogActionType
    {
        /// <summary>Создание события</summary>
        CreateEvent = 1,

        /// <summary>Привязка событий юзерам</summary>
        AttachEvent = 2,

        /// <summary>Отмена привязки событий юзерам</summary>
        DetachEvent = 3
    }
}
