using System.Collections.Generic;

namespace Moedelo.Accounts.Abstractions.Dto.Users
{
    public class SearchUserRequestDto
    {
        public IReadOnlyCollection<int> AccountIds { get; set; }
    }
}