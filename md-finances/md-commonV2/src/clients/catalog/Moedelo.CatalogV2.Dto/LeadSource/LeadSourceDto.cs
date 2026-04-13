namespace Moedelo.CatalogV2.Dto.LeadSource
{
    public class LeadSourceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UtmSource { get; set; }

        public int LeadSourceChannelId { get; set; }

        public bool IsDeleted { get; set; }
    }
}