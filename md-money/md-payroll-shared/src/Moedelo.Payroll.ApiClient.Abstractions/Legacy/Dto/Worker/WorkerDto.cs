using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerDto
    {
        public int Id { get; set; }

        public long? SubcontoId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string Vacancy { get; set; }

        public string Inn { get; set; }
        
        public PaymentMethodType PaymentMethod { get; set; }

        public bool IsNotStaff { get; set; }
        
        public string SocialInsuranceNumber { get; set; }

        public string TableNumber { get; set; }

        public bool? IsMale { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PassportNumber { get; set; }

        public string PassportAdd { get; set; }

        public DateTime? PassportDate { get; set; }

        public string PassportOfficeCode { get; set; }

        public DateTime? DateOfStartWork { get; set; }

        public int Experience { get; set; }

        public double Salary { get; set; }

        public string CountryCode { get; set; }

        public long? ActualAddress { get; set; }

        public DateTime? TerminationDate { get; set; }
    }
}
