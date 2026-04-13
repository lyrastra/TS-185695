namespace Moedelo.Billing.Shared.Enums;

public enum RegionalPartnerType
{
    /// <summary>
    /// Тип не задан (невалидное значение)
    /// </summary>
    None = 0,

    /// <summary>
    /// Обыкновенный
    /// </summary>
    Usual = 1,

    /// <summary>
    /// Банк
    /// </summary>
    Bank = 2,

    /// <summary>
    /// Франчайзи
    /// </summary>
    Franchise = 3,

    /// <summary>
    /// Компания "Моё Дело"
    /// </summary>
    MoeDelo = 4,

    /// <summary>
    /// Компания "ГлавУчёт"
    /// </summary>
    GlavUchet = 5,
}