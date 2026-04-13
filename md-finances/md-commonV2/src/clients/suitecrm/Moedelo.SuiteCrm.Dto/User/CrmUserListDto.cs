using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.User
{
    public class CrmUserListDto
    {
        /// <summary> Список ошибок </summary>
        public IReadOnlyCollection<CrmUserDto> Items { get; set; }

        /// <summary> Общее количество </summary>
        public int TotalCount { get; set; }
    }
}
