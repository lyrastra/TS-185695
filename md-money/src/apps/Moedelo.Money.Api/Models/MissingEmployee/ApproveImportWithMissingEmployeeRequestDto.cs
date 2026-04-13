
namespace Moedelo.Money.Api.Models.MissingEmployee
{
    public class ApproveImportWithMissingEmployeeRequestDto
    {
        public int EmployeeId { get; set; }
        public long[] PaymentBaseIds { get; set; }
    }
}