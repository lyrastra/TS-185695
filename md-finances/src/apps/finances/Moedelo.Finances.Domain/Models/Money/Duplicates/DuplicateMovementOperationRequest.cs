
namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateMovementOperationRequest : DuplicateOperationRequest
    {
        public int? MovementSettlementAccountId { get; set; }
    }
}