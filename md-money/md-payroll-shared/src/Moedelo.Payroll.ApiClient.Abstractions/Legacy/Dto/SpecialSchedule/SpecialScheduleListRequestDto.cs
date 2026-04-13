using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule
{
    public class SpecialScheduleListRequestDto
    {
        public SpecialScheduleListRequestDto(IReadOnlyCollection<long> specialScheduleids)
        {
            SpecialScheduleIds = specialScheduleids.ToList();
        }

        public IReadOnlyCollection<long> SpecialScheduleIds { get; }
    }
}
