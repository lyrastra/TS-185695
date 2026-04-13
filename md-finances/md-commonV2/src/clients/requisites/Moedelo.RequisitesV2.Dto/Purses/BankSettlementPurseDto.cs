
namespace Moedelo.RequisitesV2.Dto.Purses
{
    public class BankSettlementPurseDto : PurseDto
    {
        public int BankId { get; set; }

        public string BankInn { get; set; }

        public string BankKpp { get; set; }

        public string RecipientOfFunds { get; set; }

        public string RecipientSettlement { get; set; }
    }
}
