using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class RegistryQuery
    {
        /// <summary>
        /// Количество пропущенных записей сначала
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Количество возвращаемых записей
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата конца периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Возвращает операции, которые изменены или созданы позже указанной даты
        /// </summary>
        public DateTime? AfterDate { get; set; }
        
        /// <summary>
        /// СНО
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Возвращает операции по определенному источнику:
        /// 1 - Расчетный счет
        /// 2 - Касса
        /// 3 - Электронный кошелек
        /// </summary>
        public OperationSource? OperationSource { get; set; }
        
        /// <summary>
        /// Типы операций
        /// </summary>
        public IReadOnlyCollection<OperationType> OperationTypes { get; set; }
        
        public int? ContractorId { get; set; }
        public ContractorType? ContractorType { get; set; }

        /// <summary>
        /// Запрос на номер и дату
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Идентификаторы платежей
        /// </summary>
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }
    }
}
