using System;

namespace Moedelo.SpsV2.Dto.News
{
    public class DailyBuroNewsDto
    {
        public int DocumentId { get; set; }

        public int ModuleId { get; set; }

        public string AuthorName { get; set; }

        public string AuthorPosition { get; set; }

        public string AuthorImagePath { get; set; }

        public string Rubric { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int ViewCount { get; set; }

        public int VoteCount { get; set; }

        public bool IsVoted { get; set; }
    }
}
