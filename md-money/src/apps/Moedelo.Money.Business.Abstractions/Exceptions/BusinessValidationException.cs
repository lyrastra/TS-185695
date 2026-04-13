using System;

namespace Moedelo.Money.Business.Abstractions.Exceptions;

public class BusinessValidationException : Exception
{
    public BusinessValidationException(string name, string message)
        : base(message)
    {
        Name = name;
    }

    public string Name { get; }
    
    /// <summary>
    /// Частные случаи валидации
    /// </summary>
    public ValidationFailedReason? Reason { get; set; }
}