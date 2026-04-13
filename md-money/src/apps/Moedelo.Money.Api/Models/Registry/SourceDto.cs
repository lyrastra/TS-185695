using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.Registry
{
    public class SourceDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование источника: номер счета/кассы/платежной системы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Источник: расчетный счет/касса/платежные системы
        /// 1 - Расчетный счет
        /// 2 - Касса
        /// 3 - Электронный кошелек
        /// </summary>
        public OperationSource Type { get; set; }
    }
}
