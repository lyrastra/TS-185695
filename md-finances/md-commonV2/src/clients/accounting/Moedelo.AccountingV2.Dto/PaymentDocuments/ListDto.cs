using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class ListDto<T>
    {
        public List<T> Items { get; set; }
    }
}
