namespace Moedelo.ContractsV2.Dto
{
    public class ContractAutocompleteItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public int KontragentId { get; set; }
        public long? SubcontoId { get; set; }
    }
}