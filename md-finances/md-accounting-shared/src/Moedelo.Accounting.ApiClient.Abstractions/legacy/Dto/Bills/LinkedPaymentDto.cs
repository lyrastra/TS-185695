using System;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills
{
    public class LinkedPaymentDto
    {
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public long Id { get; set; }
    }
}