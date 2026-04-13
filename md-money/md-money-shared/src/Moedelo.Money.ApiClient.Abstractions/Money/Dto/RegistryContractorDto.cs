using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class RegistryContractorDto
    {
        public int? Id { get; set; }
        /// <summary>
        /// Тип Контрагент/Сотрудник/ИП/Прочее
        /// </summary>
        public ContractorType Type { get; set; }
        public string Name { get; set; }
    }
}
