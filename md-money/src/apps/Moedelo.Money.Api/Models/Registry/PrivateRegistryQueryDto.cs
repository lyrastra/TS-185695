using System;
using System.Collections.Generic;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.Registry
{
    /// <summary>
    /// Модель для хранения клентского запроса по реестру операций
    /// </summary>
    public class PrivateRegistryQueryDto
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
        [DateValue]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата конца периода
        /// </summary>
        [DateValue]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Возвращает операции, которые изменены или созданы позже указанной даты
        /// </summary>
        [DateValue]
        public DateTime? AfterDate { get; set; }

        /// <summary>
        /// СНО
        /// </summary>
        [EnumValue(EnumType = typeof(TaxationSystemType), AllowNull = true)]
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Возвращает операции по определенному источнику:
        /// 1 - Расчетный счет
        /// 2 - Касса
        /// 3 - Электронный кошелек
        /// </summary>
        [EnumValue(EnumType = typeof(OperationSource), AllowNull = true)]
        public OperationSource? OperationSource { get; set; }

        /// <summary>
        /// Типы операций
        /// </summary>
        public IReadOnlyCollection<OperationType> OperationTypes { get; set; }

        [IdIntValueAttribute]
        public int? ContractorId { get; set; }

        [EnumValue(EnumType = typeof(СontractorQueryType), AllowNull = true)]
        public СontractorQueryType? ContractorType { get; set; }

        /// <summary>
        /// Запрос на номер
        /// </summary>
        [ValidateXss]
        public string Query { get; set; }

        /// <summary>
        /// Идентификаторы платежей
        /// </summary>
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }
    }
}