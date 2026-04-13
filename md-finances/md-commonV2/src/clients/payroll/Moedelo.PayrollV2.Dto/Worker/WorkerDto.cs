using System;

namespace Moedelo.PayrollV2.Dto.Worker
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

        public DateTime? VacancyStartDate { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfStartWork { get; set; }

        public string PassportNumber { get; set; }

        public string PassportAdd { get; set; }

        public string SocialInsuranceNumber { get; set; }

        public bool IsForeigner { get; set; }

        public bool IsNotStaff { get; set; }

        public string SubcontoName { get; set; }

        public bool? IsMale { get; set; }

        public string CountryCode { get; set; }

        public string Inn { get; set; }

        public string TableNumber { get; set; }

        public long? ActualAddress { get; set; }

        public double Salary { get; set; }

        public int? WorkerStatus { get; set; }

        public DateTime? PassportDate { get; set; }

        public string PassportOfficeCode { get; set; }

        public string PensionNumber { get; set; }

        public int Experience { get; set; }

        public int PaymentMethod { get; set; }

        public string Phone { get; set; }

        public int? SocialGroupId { get; set; }
    }
}
