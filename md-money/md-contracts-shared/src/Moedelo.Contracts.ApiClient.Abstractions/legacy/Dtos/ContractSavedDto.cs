namespace Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos
{
    public class ContractSavedDto
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public long? SubcontoId { get; set; }
    }
}