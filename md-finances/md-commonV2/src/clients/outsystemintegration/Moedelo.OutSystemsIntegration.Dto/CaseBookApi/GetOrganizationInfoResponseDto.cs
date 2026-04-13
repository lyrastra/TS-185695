using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class GetOrganizationInfoResponseDto
    {
        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public string Okpo { get; set; }

        public string EnglishName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsError { get; set; }

        public string CaseBookVersion { get; set; }

        public string DataVersion { get; set; }
        
        public string Oktmo { get; set; }
        
        public string Okfs { get; set; }
    }
}
