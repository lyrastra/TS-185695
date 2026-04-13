namespace Moedelo.CommonV2.Audit.Management.Domain
{
    /// <summary>
    /// Переключатели, регулирующие сбор данных audit trail
    /// </summary>
    public sealed class AuditTrailToggles
    {
        /// <summary>
        /// Общий переключатель "Собирать данные audit trail"
        /// Если равен false, то сбор отключен для всех типов данных  
        /// </summary>
        public bool IsGloballyEnabled { get; internal set; }
        /// <summary>
        /// Переключатель "Собирать данные по входящим api запросам"
        /// </summary>
        public bool IsApiHandlerEnabled { get; internal set; }
        /// <summary>
        /// Переключатель "Собирать данные по изходящим api запросам"
        /// </summary>
        public bool IsOutgoingHttpRequestEnabled { get; internal set; }
        /// <summary>
        /// Переключатель "Собирать данные по запросам в БД"
        /// </summary>
        public bool IsDbQueryEnabled { get; internal set; }
        /// <summary>
        /// Переключатель "Собирать данные особого типа (internal code)"
        /// </summary>
        public bool IsInternalCodeEnabled { get; internal set; }
    }
}
