using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi.External.Enums;
using Moedelo.CommonV2.Xss.WebApi.Extensions;

namespace Moedelo.CommonV2.Auth.WebApi.External;

public class MoedeloExternalApiWebApplicationConfigurationSettings
{
    /// <summary>
    /// Режим проверки XSS запросов
    /// </summary>
    public XssValidationMode XssValidationMode { get; set; } = XssValidationMode.RejectSuspiciousRequests;

    /// <summary>
    /// Режим валидации входящих запросов
    /// </summary>
    public ValidationMode ValidationMode { get; set; } = ValidationMode.None;

    /// <summary>
    /// Specifies whether error details, such as exception messages and stack traces, should be included in error messages
    /// </summary>
    public IncludeErrorDetailPolicy IncludeErrorDetailPolicy { get; set; } = IncludeErrorDetailPolicy.LocalOnly;

    /// <summary>
    /// Отбрасывать POST и PUT запросы с пустым телом
    /// </summary>
    public bool RejectEmptyPostAndPutRequests { get; set; } = false;

    /// <summary>
    /// Отбрасывать POST и PUT запросы с пустым телом
    /// </summary>
    public MoedeloExternalApiWebApplicationConfigurationSettings SetRejectEmptyPostAndPutRequests(bool value)
    {
        RejectEmptyPostAndPutRequests = value;
        return this;
    }

    /// <summary>
    /// Режим валидации входящих запросов
    /// </summary>
    public MoedeloExternalApiWebApplicationConfigurationSettings SetXssValidationMode(XssValidationMode mode)
    {
        this.XssValidationMode = mode;
        return this;
    }

    /// <summary>
    /// Включить валидацию входящих запросов
    /// </summary>
    public MoedeloExternalApiWebApplicationConfigurationSettings EnableValidation()
    {
        this.ValidationMode = ValidationMode.Enable | ValidationMode.SuppressValidationErrorLogging;
#if DEBUG
        this.ValidationMode |= ValidationMode.AddDebugInfoIntoResponse;
#endif
        return this;
    }
}
