using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto
{
    public class SyntheticAccountDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SyntheticAccountCode Code { get; set; }
    }
}
