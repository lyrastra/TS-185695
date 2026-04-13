using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateYandexOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public int KontragentId { get; set; }
        public string Description { get; set; }
    }
}