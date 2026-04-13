#nullable enable
using System;

namespace Moedelo.CommonV2.EventBus.Requisities;

/// <summary>
/// Событие об изменения у фирмы данных fss
/// </summary>
public sealed class FirmFssInfoChangedEvent
{
    /// <summary>
    /// Тип события
    /// </summary>
    public EventTypes EventType { get; set; }

    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Автор изменений
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Момент времени, когда произошло событие
    /// </summary>
    public DateTime EventTimestamp { get; set; }

    /// <summary>
    /// Новое значение данных.
    /// Заполняется только если <see cref="EventType"/> равен <see cref="EventTypes.Updated"/>
    /// </summary>
    public FssInfo? Data { get; set; }

    public enum EventTypes
    {
        /// <summary>
        /// Данные были обновлены
        /// </summary>
        Updated = 0,

        /// <summary>
        /// Данные были удалены
        /// </summary>
        Deleted = 1
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class FssInfo
    {
        public string FssNumber { get; set; }
        public string Code { get; set; }
        public string SubordinationCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Bik { get; set; }
        public string SettlementAccount { get; set; }
        public string UnifiedSettlementAccount { get; set; }
        public string RecipientName { get; set; }
        public string Okato { get; set; }
        public string Oktmo { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
