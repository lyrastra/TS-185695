namespace Moedelo.OfficeV2.Dto.Egr.Search
{
    public class ActivityGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public bool IsActive => Count > 0;
    }
}
