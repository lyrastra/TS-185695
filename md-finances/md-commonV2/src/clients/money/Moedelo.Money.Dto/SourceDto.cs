using Moedelo.Common.Enums.Enums.Money;

namespace Moedelo.Money.Dto
{
    public class SourceDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование источника: номер счета/кассы/платежной системы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Источник: расчетный счет/касса/платежные системы
        /// 1 - Расчетный счет
        /// 2 - Касса
        /// 3 - Платежные системы
        /// </summary>
        public PaymentSources Type { get; set; }
    }
}