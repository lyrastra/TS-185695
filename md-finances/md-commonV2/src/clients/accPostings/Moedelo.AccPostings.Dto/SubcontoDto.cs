using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccPostings.Dto
{
    public class SubcontoDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SubcontoType Type { get; set; }

        /// <summary>
        /// Признак того, что субконто является разделяемым (общим) между всеми фирмами
        /// т.е. не имеет привязки к конкретной фирме
        /// В большинстве случаев вы можете игнорировать это поле. Оно введено для поддержания обратной совместимости
        /// с кодом, в котором до сих пор сохранение субконто ведётся не через Moedelo.AccPostings.Client.ISubcontoClient
        /// </summary>
        public bool IsCommon { get; set; }
    }
}