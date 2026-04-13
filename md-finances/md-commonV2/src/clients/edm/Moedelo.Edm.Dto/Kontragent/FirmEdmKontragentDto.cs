using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Edm.Dto.Kontragent
{
    public class FirmEdmKontragentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Inn { get; set; }
        public string Guid { get; set; }
        public string EdmSystemName { get; set; }
        public bool CanReset { get; set; }
        public string ModifyDate { get; set; }
    }
}
