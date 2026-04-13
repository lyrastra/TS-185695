using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateRoboAndSapeOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }
    }
}