using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class RegistryOperationSourceDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование источника: номер счета/кассы/эл.кошелека
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Источник: расчетный счет/касса/эл.кошелек
        /// </summary>
        public OperationSource Type { get; set; }
    }
}
