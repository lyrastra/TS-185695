using Moedelo.AccPostings.Enums;

namespace Moedelo.Money.Providing.Api.Models.AccPostings
{
    public class SubcontoDto
    {
        /// <summary>
        /// Идентификатор субконто
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public SubcontoType Type { get; set; }
    }
}
