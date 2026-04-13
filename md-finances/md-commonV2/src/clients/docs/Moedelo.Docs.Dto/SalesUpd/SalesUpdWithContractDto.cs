namespace Moedelo.Docs.Dto.SalesUpd
{
    public class SalesUpdWithContractDto : SalesUpdDto
    {
        public SalesUpdDto Document { get; set; }

        public ContractDto Contract { get; set; }

        public class ContractDto
        {
            public long DocumentBaseId { get; set; }

            public int Id { get; set; }
        }
    }
}