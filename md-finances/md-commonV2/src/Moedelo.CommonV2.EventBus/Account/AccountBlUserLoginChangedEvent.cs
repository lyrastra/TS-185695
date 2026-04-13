#nullable enable
using System;
using Moedelo.Common.Enums.Enums.Products;

namespace Moedelo.CommonV2.EventBus.Account;

public class AccountBlUserLoginChangedEvent
{
    public int UserId { get; set; }
    public string NewLogin { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Дополнительные поля. У старых событий может быть не заполнено
    /// </summary>
    public ExtraProperties? ExtraData { get; set; }

    public class ExtraProperties
    {
        /// <summary>
        /// Старый логин пользователя
        /// </summary>
        public string OldLogin { get; set; } = null!;
    
        /// <summary>
        /// Тип WLProductPartition, к которому принадлежал пользователь на момент изменений 
        /// </summary>
        public WLProductPartition WhiteLabelProductPartition { get; set; }

        /// <summary>
        /// Хост, на котором проводились изменения
        /// </summary>
        public string? Host { get; set; }
    }
}
