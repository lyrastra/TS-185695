using System;

namespace Moedelo.HomeV2.Dto.ClientSource
{
    public class ClientSourceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TrialCardPrefix { get; set; }

        public bool IsTrialCardSupported { get; set; }

        public bool IsPromoCodeSupported { get; set; }

        [Obsolete("need delete using")]
        public bool IsInternetSource { get; set; }

        [Obsolete("need delete using")]
        public bool CanDelete { get; set; }
    }
}
