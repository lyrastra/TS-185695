namespace Moedelo.Money.Business.Abstractions.Exceptions;

public enum ValidationFailedReason
{
    /// <summary>
    /// Закрытый период
    /// </summary>
    ClosedPeriod = 0,
    
    /// <summary>
    /// Тип операции недоступен для выбора
    /// </summary>
    OperationTypeNotAllowed = 1
}