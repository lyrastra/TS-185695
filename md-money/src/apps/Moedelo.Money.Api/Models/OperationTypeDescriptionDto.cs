using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models
{
    public class OperationTypeDescriptionDto
    {
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Источник
        /// 1 - расчетный счет
        /// 2 - касса
        /// 3 - электронный кошелек
        /// </summary>
        public MoneySourceType SourceType { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
