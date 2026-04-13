namespace Moedelo.Infrastructure.AspNetCore.HostedServices;

/// <summary>
/// Правила обработки случая, когда выполнение шага заняло времени больше, чем отведённый интервал.
/// Используется в <see cref="RepeatingHostedService"/>
/// </summary>
public enum IntervalOverlapHandlingRule
{
    /// <summary>
    /// Подождать до начала следующего интервала 
    /// </summary>
    WaitForNextInterval = 0,
    /// <summary>
    /// Запустить следующий шаг незамедлительно по завершении предыдущего
    /// </summary>
    RunImmediately = 1
}
