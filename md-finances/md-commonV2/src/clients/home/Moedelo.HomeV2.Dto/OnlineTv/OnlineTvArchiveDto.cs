using System;

namespace Moedelo.HomeV2.Dto.OnlineTv
{
    public class OnlineTvArchiveDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Image { get; set; }

        public string PreviewImage { get; set; }

        public int CategoryId { get; set; }

        public DateTime StartDateTime { get; set; }

        public string StartDate { get; set; }

        public string Link { get; set; }
    }
}
