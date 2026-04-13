using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Abstractions.Exceptions
{
    public class OperationMismatchTypeExcepton : Exception
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Тип по текущему процессу обработки
        /// </summary>
        public OperationType ExpectedType { get; set; }

        /// <summary>
        /// Тип который сейчас сохранен в базе
        /// </summary>
        public OperationType ActualType { get; set; }
    }
}
