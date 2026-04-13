namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public sealed class KontragentInfoForErptDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Kpp { get; set; }

        public string Inn { get; set; }

        public bool IsDeleted { get; set; }
        
        public bool IsArchived { get; set; }
    }
}
