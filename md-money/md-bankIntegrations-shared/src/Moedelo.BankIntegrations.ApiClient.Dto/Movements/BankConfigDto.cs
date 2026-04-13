namespace Moedelo.BankIntegrations.Dto.Movements
{
    public class BankConfigDto
    {
        public int MovementDaysPeriod { get; set; }

        public int DayOffsetIfEndDayToday { get; set; }
    }
}
