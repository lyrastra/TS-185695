namespace Moedelo.Common.Enums.Enums.Integration
{
    /// <summary> Тип уведомления по интегрированным ошибкам </summary>
    public enum IntegrationNotificationErrorType
    {
        /// <summary> Ошибка по умолчанию </summary>
        Info = 0,
        /// <summary> Ошибка с предупреждением </summary>
        Warning = 1,
        /// <summary> Ошибка критическая </summary>
        Error = 2
    }
}