namespace Moedelo.Common.Http.Generic.Abstractions;

public sealed class HttpQuerySetting
{
    public static readonly TimeSpan DefaultTimeout = new (0, 1, 40);
    
    public HttpQuerySetting(TimeSpan? timeout = null,
        bool setResponseContentIntoException = false)
    {
        Timeout = timeout ?? DefaultTimeout;
        SetResponseContentIntoException = setResponseContentIntoException;
    }

    public TimeSpan Timeout { get; }
    
    public bool DontThrowOn404 { get; set; }

    /// <summary>
    /// Для неуспешного кода ответа (не 2хх) будет начитано тело ответа для некоторых исключений:
    /// <see cref="Moedelo.Common.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"/> и
    /// <see cref="Moedelo.Common.Http.Abstractions.Exceptions.HttpRequestValidationException"/>
    /// </summary>
    public bool SetResponseContentIntoException { get; }
}