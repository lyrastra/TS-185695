namespace Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;

/// <summary>
/// Класс для использования при документировании возвращаемого типа для метода mvc контроллера,
/// который возвращает значение, используя <see cref="ApiDataResult"/>
/// <example>
/// <code>
/// ProducesResponseType(typeof(ApiDataResponseType&lt;SomeResponseDto&gt;), (int)HttpStatusCode.OK)
/// </code>
/// </example>
/// </summary>
/// <typeparam name="TResponse">тип возвращаемого значения</typeparam>
public abstract class ApiDataResponseType<TResponse>
{
    // ReSharper disable once InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Значение, которое будет размещено в поле <c>data</c> JSON-ответа.
    /// </summary>
    public TResponse data { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
