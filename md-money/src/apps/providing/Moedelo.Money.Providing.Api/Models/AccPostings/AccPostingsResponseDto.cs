using System.Collections.Generic;

namespace Moedelo.Money.Providing.Api.Models.AccPostings
{
    public class AccPostingsResponseDto
    {
        /// <summary>
        /// Проводки по связанным документам
        /// </summary>
        public IReadOnlyCollection<LinkedDocumentAccPostingsDto> LinkedDocuments { get; set; }

        /// <summary>
        /// Проводки
        /// </summary>
        public IReadOnlyCollection<AccPostingDto> Postings { get; set; }
    }
}
