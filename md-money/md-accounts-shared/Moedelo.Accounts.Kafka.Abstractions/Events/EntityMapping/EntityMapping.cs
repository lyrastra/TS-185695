using System;

namespace Moedelo.Accounts.Kafka.Abstractions.Events.EntityMapping
{
    public class EntityMapping
    {
        /// <summary>
        /// Идентификатор записи-источника (в самой сущности м.б. int)
        /// </summary>
        public long SourceId { get; set; }
        
        /// <summary>
        /// Идентификатор записи, созданной на основе источника 
        /// </summary>
        public long TargetId { get; set; }
        
        /// <summary>
        /// Фирма-источник
        /// </summary>
        public int FirmId { get; set; }
        
        /// <summary>
        /// Тип копируемой сущности
        /// </summary>
        public EntityType EntityType { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}