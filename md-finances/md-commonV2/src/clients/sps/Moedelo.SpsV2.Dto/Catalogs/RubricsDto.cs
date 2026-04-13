namespace Moedelo.SpsV2.Dto.Catalogs
{
    public class RubricsDto
    {
        public string Title { get; set; }

        public bool IsFolder { get; set; }

        public string Key { get; set; }

        public bool Expand { get; set; }

        public bool IsLazy { get; set; }

        public string Module { get; set; }

        public int QaCount { get; set; }

        public string QaTextCount { get; set; }

        public int? StatusType { get; set; }
    }
}
