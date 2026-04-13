using System;

namespace Moedelo.SpsV2.Dto.News
{
    public class MainBuroNewsDto
    {
        public int DocumentId { get; set; }

        public int ModuleId { get; set; }

        public string Docname { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int VoteCount { get; set; }

        public bool IsVoted { get; set; }
    }
}
