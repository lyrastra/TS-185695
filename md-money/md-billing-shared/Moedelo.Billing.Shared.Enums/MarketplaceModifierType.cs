namespace Moedelo.Billing.Shared.Enums;

public enum MarketplaceModifierType : byte
{
    /// <summary>
    /// Отображение нередактируемого логического типа (зелёная галка или красный прочерк)
    /// </summary>
    boolean = 1,

    /// <summary>
    /// Отображение checkbox'а для включения/отключения усуги
    /// </summary>
    checkbox = 2,

    /// <summary>
    /// Отображение ниспадающего меню для выбора опции
    /// </summary>
    select = 3,

    /// <summary>
    /// Отображение счётчика
    /// </summary>
    counter = 4,

    /// <summary>
    /// Отображение нередактируемого текста
    /// </summary>
    text = 5,

    /// <summary>
    /// Отображение поля ввода
    /// </summary>
    input = 6,
}