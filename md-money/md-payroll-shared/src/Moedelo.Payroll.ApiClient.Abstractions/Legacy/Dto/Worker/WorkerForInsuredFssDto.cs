using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerForInsuredFssDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Inn { get; set; }

        public string Snils { get; set; }

        public string Phone { get; set; }

        public bool? IsMale { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PassportNumber { get; set; }

        public string PassportAdd { get; set; }

        public DateTime? PassportDate { get; set; }

        public DateTime? DateOfStartWork { get; set; }

        public DateTime? TerminationDate { get; set; }

        public bool IsForeigner { get; set; }

        public bool IsExpert { get; set; }

        public bool IsNotStaff { get; set; }

        public string CountryCode { get; set; }

        public string ForeignerAddress { get; set; }

        public WorkerForInsuredFssPaymentData PaymentData { get; set; }

        public WorkerForInsuredFssAddressData AddressData { get; set; }

        public List<NdflStatusHistoryDto> NdflStatusHistories { get; set; }

        public List<ForeignerStatusHistoryDto> ForeignerStatusHistories { get; set; }
    }
    
    public class WorkerForInsuredFssPaymentData
    {
        public PaymentMethodType PaymentMethod { get; set; }

        public int? BankId { get; set; }
        
        public string AccountNum { get; set; }
    }
}
