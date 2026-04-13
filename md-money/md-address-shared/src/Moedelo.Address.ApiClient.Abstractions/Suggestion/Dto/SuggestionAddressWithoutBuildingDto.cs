using Moedelo.Address.ApiClient.Abstractions.Address.Dto;

namespace Moedelo.Address.ApiClient.Abstractions.Suggestion.Dto
{
    public class SuggestionAddressWithoutBuildingDto
    {
        /// <summary>
        /// Адресные объекты
        /// </summary>
        public AddressPartDto[] Parts { get; set; }

        /// <summary>
        /// Полный адрес
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Код региона
        /// </summary>
        public string RegionCode { get; set; }
    }
}
