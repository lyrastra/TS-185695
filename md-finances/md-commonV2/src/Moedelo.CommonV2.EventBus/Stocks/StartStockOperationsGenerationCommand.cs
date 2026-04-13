namespace Moedelo.CommonV2.EventBus.Stocks;

/// <summary>
/// Команда "Запустить генерацию данных для Движения"
/// </summary>
public class StartStockOperationsGenerationCommand
{
    /// <summary>
    /// Идентификатор фирмы, для которой надо рассчитать данные
    /// </summary>
    public int FirmId { get; set; }
    /// <summary>
    /// Идентификатор пользователя, заказавшего расчёт данных
    /// </summary>
    public int UserId { get; set; }
}
