using System;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FiasFormatAutocomplete
{
    public class StreetAutocompleteFiasFormatResponse
    {
        /// <summary>
        /// Полное название улицы, например ул Лениногорская
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Guid улицы
        /// </summary>
        public Guid Guid { get; set; }
    }
}
