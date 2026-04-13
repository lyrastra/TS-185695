namespace Moedelo.Payroll.Shared.Enums.Common;

/// <summary>
/// Тип дня из рабочего расписания сотрудника
/// </summary>
public enum DayInfoType
{
    /* Значения полей не изменять! */

    /// <summary> Житие мое </summary>
    Unknown = -1,

    /// <summary> Стандартный рабочий день </summary>
    Standart = 0,

    /// <summary> Выходной </summary>
    Weekend = 1,

    /// <summary> Праздничный день </summary>
    Holiday = 2,

    /// <summary> Сокращенный рабочий день </summary>
    ShortDay = 3,

    /// <summary>  Отпуск  </summary>
    Vacation = 4,

    /// <summary>  Больничный  </summary>
    Illness = 5,

    /// <summary> Простой </summary>
    DeadTime = 6,

    /// <summary> Прогул  </summary>
    Truancy = 7,

    /// <summary> Дополнительные выходные дни </summary>
    AdditionalWeekend = 8,

    /// <summary> Командировка </summary>
    BusinessTrip = 9,

    /// <summary> Исполнение обязанностей </summary>
    Law = 10,

    /// <summary> Воинский учет </summary>
    MilitaryRegistration = 11,

    /// <summary> Переработка </summary>
    OvertimeWork = 12,

    /// <summary> Нерабочий день с сохранением зарплаты </summary>
    PaidHoliday = 13,
}