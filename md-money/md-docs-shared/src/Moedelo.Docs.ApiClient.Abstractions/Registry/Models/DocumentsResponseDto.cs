using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.Registry.Models
{
    public class DocumentsResponseDto
    {
        /// <summary>
        /// Начало смещения выборки
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Количество строк в выборке
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Количество строк в выборке
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Документы
        /// </summary>
        public IReadOnlyCollection<DocumentResponseDto> Documents { get; set; }
    }
}