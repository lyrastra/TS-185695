using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class GetWorkerDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Inn { get; set; }

        public string SocialInsuranceNumber { get; set; }

        public bool? IsMale { get; set; }

        public DateTime? DateOfBirth { get; set; }
        
        public DateTime? DateOfStartWork { get; set; }

        public string PassportNumber { get; set; }

        public string PassportAdd { get; set; }

        public DateTime? PassportDate { get; set; }

        public string PassportOfficeCode { get; set; }

        public FounderType FounderType { get; set; }

        public bool IsStaff { get; set; }

        public string TableNumber { get; set; }

        public DateTime? TerminationDate { get; set; }

        public bool IsForeigner { get; set; }
    }
}