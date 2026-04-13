
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.PurseOperations.Dtos
{
    public class PurseOperationDto
    {
        public int? KontragentId { get; set; }

        public string Comment { get; set; }

        public string Date { get; set; }

        public decimal Sum { get; set; }

        public PurseOperationType PurseOperationType { get; set; }

        public int PurseId { get; set; }
    }
}
