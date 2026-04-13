using System;

namespace Moedelo.Infrastructure.Http.Abstractions.Models
{
    public sealed class HttpQuerySetting
    {
        public HttpQuerySetting(TimeSpan? timeout = null,
            bool setResponseContentIntoException = false)
        {
            var defaultTimeout = new TimeSpan(0, 1, 40);
            Timeout = timeout.GetValueOrDefault(defaultTimeout);
            SetResponseContentIntoException = setResponseContentIntoException;
        }

        public TimeSpan Timeout { get; }

        /// <summary>
        /// Не рассматривать ответ 404 как ошибку
        /// </summary>
        public bool DontThrowOn404 { get; set; }

        /// <summary>
        /// Для неуспешного кода ответа (не 2хх) будет начитано тело ответа для исключений:
        /// <see cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"/> и
        /// <see cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"/>
        /// </summary>
        public bool SetResponseContentIntoException { get; }
    }
}