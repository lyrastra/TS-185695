namespace Moedelo.SuiteCrm.Dto.Activity
{
    public class ActivityResponseDto
    {
        public int ProcessedObjects { get; set; }

        public int Failed { get; set; }

        public double MinutesForWork { get; set; }
    }
}