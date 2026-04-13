using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Dto
{
    public class RegistryOperationSourceDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование источника: номер счета/кассы/платежной системы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Источник: банк/касса/платежные системы
        /// </summary>
        public OperationSource Type { get; set; }
    }
}
