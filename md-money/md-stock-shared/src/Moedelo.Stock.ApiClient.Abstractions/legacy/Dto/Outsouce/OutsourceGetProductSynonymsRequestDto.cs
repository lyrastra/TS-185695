using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceGetProductSynonymsRequestDto
    {
        public int FirmId { get; set; }

        public long NomenclatureId { get; set; }

        /// <summary>
        /// Список слов для поиска синонимов
        /// </summary>
        public IReadOnlyCollection<string> Words { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }
    }
}
