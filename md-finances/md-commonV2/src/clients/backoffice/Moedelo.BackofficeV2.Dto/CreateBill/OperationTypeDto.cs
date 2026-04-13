using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BackofficeV2.Dto.CreateBill
{
    public class OperationTypeDto
    {
        public string Name { get; set; }

        public BillingOperationType Type { get; set; }
    }
}
