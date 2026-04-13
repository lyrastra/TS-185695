namespace Moedelo.Payroll.Shared.Enums.Charge;

public enum ParentChargeTypeCode
{
    // Гражданский правовой договор
    Gpd = -2,

    // Пользовательский тип
    Custom = -1,

    // Оклад
    Salary = 0,

    // Доплаты, надбавки
    Bonus = 1000,

    // Премии
    Premium = 2000,

    // Материальная помощь
    MaterialAid = 2700,

    // Отпуск
    Vacation = 4000,

    // Больничный
    SickList = 4400,

    // Другие виды отсутствия
    OtherAbsence = 4700,

    // Присутствия и оплата по среднему заработку
    Presence = 5000,

    // Выплаты при увольнении
    Fired = 5100,

    // Доп.доход и прочие начисления
    OtherIncome = 6000,

    // Удержания, долги
    Deduction = 7000,

    // Аванс
    Advance = 9000,

    // Налог на доход
    Tax = 10000
}