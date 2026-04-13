namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class WorkerInfoCheckDto
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string PassportNumber { get; set; }

        public string SocialInsuranceNumber { get; set; }

        public bool IsForeigner { get; set; }

        public string Inn { get; set; }
        
        public bool ForeignerStatusIsUndefined { get; set; }
    }
}