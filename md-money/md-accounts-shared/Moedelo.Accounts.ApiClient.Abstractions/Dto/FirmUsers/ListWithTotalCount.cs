using System.Collections.Generic;

namespace Moedelo.Accounts.Abstractions.Dto.FirmUsers;

public class ListWithTotalCount<T>
{
    public List<T> Items { get; set; }

    public int TotalCount { get; set; }
}