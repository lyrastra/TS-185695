using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Moedelo.InfrastructureV2.Mvc.Internals;

/// <summary>
/// Адаптированная копия класса из webapi
/// </summary>
[XmlRoot("Error")]
internal sealed class HttpError : Dictionary<string, object>
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Http.HttpError" /> class.</summary>
    public HttpError()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Http.HttpError" /> class containing error message <paramref name="message" />.</summary>
    /// <param name="message">The error message to associate with this instance.</param>
    public HttpError(string message)
        : this()
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Http.HttpError" /> class for <paramref name="exception" />.</summary>
    /// <param name="exception">The exception to use for error information.</param>
    /// <param name="includeErrorDetail">true to include the exception information in the error; false otherwise</param>
    public HttpError(Exception exception, bool includeErrorDetail)
        : this()
    {
        _ = exception ?? throw new ArgumentNullException(nameof(exception));
        Message = SRResources.ErrorOccurred;
        if (!includeErrorDetail)
        {
            return;
        }
        Add(HttpErrorKeys.ExceptionMessageKey, exception.Message);
        Add(HttpErrorKeys.ExceptionTypeKey, exception.GetType().FullName ?? exception.GetType().Name);
        Add(HttpErrorKeys.StackTraceKey, exception.StackTrace);

        if (exception.InnerException == null)
        {
            return;
        }
        Add(HttpErrorKeys.InnerExceptionKey, new HttpError(exception.InnerException, includeErrorDetail));
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Http.HttpError" /> class for <paramref name="modelState" />.</summary>
    /// <param name="modelState">The invalid model state to use for error information.</param>
    /// <param name="includeErrorDetail">true to include exception messages in the error; false otherwise</param>
    public HttpError(ModelStateDictionary modelState, bool includeErrorDetail)
        : this()
    {
        _ = modelState ?? throw new ArgumentNullException(nameof(modelState));
        if (modelState.IsValid)
        {
            throw new Exception("Объект должен создаваться только для модели не прошедшей валидацию");
        }

        Message = SRResources.BadRequest;
        var httpError = new HttpError();
        foreach (KeyValuePair<string, ModelState> keyValuePair in modelState)
        {
            var key = keyValuePair.Key;
            var errors = keyValuePair.Value.Errors;
            if (errors != null && errors.Count > 0)
            {
                IEnumerable<string> array = errors.Select<ModelError, string>((Func<ModelError, string>) (error =>
                {
                    if (includeErrorDetail && error.Exception != null)
                        return error.Exception.Message;
                    return !string.IsNullOrEmpty(error.ErrorMessage) ? error.ErrorMessage : SRResources.ErrorOccurred;
                })).ToArray<string>();
                httpError.Add(key, array);
            }
        }
        Add(HttpErrorKeys.ModelStateKey, httpError);
    }

    internal HttpError(string message, string messageDetail)
        : this(message)
    {
        _ = messageDetail ?? throw new ArgumentNullException(nameof(messageDetail));
        Add(HttpErrorKeys.MessageDetailKey, messageDetail);
    }

    /// <summary>Gets or sets the high-level, user-visible message explaining the cause of the error. Information carried in this field should be considered public in that it will go over the wire regardless of the <see cref="T:System.Web.Http.IncludeErrorDetailPolicy" />. As a result care should be taken not to disclose sensitive information about the server or the application.</summary>
    /// <returns>The high-level, user-visible message explaining the cause of the error. Information carried in this field should be considered public in that it will go over the wire regardless of the <see cref="T:System.Web.Http.IncludeErrorDetailPolicy" />. As a result care should be taken not to disclose sensitive information about the server or the application.</returns>
    public string Message
    {
        get => GetPropertyValue<string>(HttpErrorKeys.MessageKey) ?? "";
        set => this[HttpErrorKeys.MessageKey] = value;
    }

    /// <summary>Gets the <see cref="P:System.Web.Http.HttpError.ModelState" /> containing information about the errors that occurred during model binding.</summary>
    /// <returns>The <see cref="P:System.Web.Http.HttpError.ModelState" /> containing information about the errors that occurred during model binding.</returns>
    public HttpError? ModelState => GetPropertyValue<HttpError>(HttpErrorKeys.ModelStateKey);

    /// <summary>Gets or sets a detailed description of the error intended for the developer to understand exactly what failed.</summary>
    /// <returns>A detailed description of the error intended for the developer to understand exactly what failed.</returns>
    public string MessageDetail
    {
        get => GetPropertyValue<string>(HttpErrorKeys.MessageDetailKey) ?? "";
        set => this[HttpErrorKeys.MessageDetailKey] = value;
    }

    /// <summary>Gets or sets the message of the <see cref="T:System.Exception" /> if available.</summary>
    /// <returns>The message of the <see cref="T:System.Exception" /> if available.</returns>
    public string ExceptionMessage
    {
        get => GetPropertyValue<string>(HttpErrorKeys.ExceptionMessageKey) ?? "";
        set => this[HttpErrorKeys.ExceptionMessageKey] = value;
    }

    /// <summary>Gets or sets the type of the <see cref="T:System.Exception" /> if available.</summary>
    /// <returns>The type of the <see cref="T:System.Exception" /> if available.</returns>
    public string ExceptionType
    {
        get => GetPropertyValue<string>(HttpErrorKeys.ExceptionTypeKey) ?? "";
        set => this[HttpErrorKeys.ExceptionTypeKey] = value;
    }

    /// <summary>Gets or sets the stack trace information associated with this instance if available.</summary>
    /// <returns>The stack trace information associated with this instance if available.</returns>
    public string StackTrace
    {
        get => GetPropertyValue<string>(HttpErrorKeys.StackTraceKey) ?? "";
        set => this[HttpErrorKeys.StackTraceKey] = value;
    }

    /// <summary>Gets the inner <see cref="T:System.Exception" /> associated with this instance if available.</summary>
    /// <returns>The inner <see cref="T:System.Exception" /> associated with this instance if available.</returns>
    public HttpError? InnerException => GetPropertyValue<HttpError>(HttpErrorKeys.InnerExceptionKey);

    /// <summary>Gets a particular property value from this error instance.</summary>
    /// <returns>A particular property value from this error instance.</returns>
    /// <param name="key">The name of the error property.</param>
    /// <typeparam name="TValue">The type of the property.</typeparam>
    public TValue? GetPropertyValue<TValue>(string key) where TValue : class
    {
        return TryGetValue(key, out var obj)
            ? obj as TValue
            : default (TValue);
    }
}
