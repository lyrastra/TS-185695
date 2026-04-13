using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Dss.Dto
{
    public class DssRegistrationErrorDto
    {
        public string Type { get; }

        public string Error { get; }

        protected DssRegistrationErrorDto() { }

        public DssRegistrationErrorDto(string type, string error)
        {
            Type = type;
            Error = error;
        }
    }
}
