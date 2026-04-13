namespace Moedelo.SpsV2.Dto.Documents
{
    public class QaInfoRequestDto
    {
        public byte ModuleId { get; set; }

        public int DocumentId { get; set; }

        public int LinkedQaByKeywordsLimit { get; set; }

        public int AnswerWordsLimit { get; set; }
    }
}
