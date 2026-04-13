using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Blancbank
{
    public class CompanyDetailsDto
    {
        public string Inn { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string MainOkved { get; set; }

        public string RegistrationAddressLine { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}