namespace Moedelo.Common.Enums.Enums.Calendar
{
    public enum СalendarEventActionStatus
    {
        /// <summary>
        /// Событие может быть переоткрыто
        /// </summary>
        Ok = 0,
        /// <summary>
        /// Событие не может быть переоткрыто
        /// UserMessage содержит описание причины для пользователя
        /// </summary>
        CannotBeReopened = 1,
        /// <summary>
        /// Событие не может быть завершено
        /// UserMessage содержит описание причины для пользователя
        /// </summary>
        CannotBeClosed = 2,
        /// <summary>
        /// Переоткрытие событие должно быть явно подтверждено пользователем
        /// UserMessage содержит описание причины для пользователя
        /// </summary>
        ConfirmationRequired = 3
    }
}
