using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.Registry
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
        [EnumValue]
        public ContractorType Type { get; set; }
        public string Name { get; set; }
    }
}
