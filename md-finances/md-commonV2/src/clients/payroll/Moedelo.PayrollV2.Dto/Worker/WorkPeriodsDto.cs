using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.PayrollV2.Dto.Worker
{
    public class WorkPeriodsDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfStartWork { get; set; }
        public DateTime? TerminationDate { get; set; }
    }
}
