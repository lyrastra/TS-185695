using System;

namespace Moedelo.CommonV2.Auth.WebApi.External.Enums;

[Flags]
public enum ValidationMode
{
    /// <summary>
    /// Всё отключено
    /// </summary>
    None = 0,
    /// <summary>
    /// Включить валидацию
    /// </summary>
    Enable = 1 << 0,
    /// <summary>
    /// Подавить логирование ошибок валидации
    /// </summary>
    SuppressValidationErrorLogging = 1 << 1,
    /// <summary>
    /// Добавлять debug-информацию в ответ (используй только для отладки)
    /// </summary>
    AddDebugInfoIntoResponse = 1 << 2
}
