using Moedelo.Common.Enums.Enums.Money;

namespace Moedelo.Money.Dto
{
    public class ContractorDto
    {
        public int? Id { get; set; }
        /// <summary>
        /// Тип 
        /// 1  - Контрагент
        /// 2  - Сотрудник
        /// 3  - ИП
        /// 99 - Прочее
        /// </summary>
        public ContractorType Type { get; set; }
        public string Name { get; set; }
    }
}