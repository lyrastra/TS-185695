using System;

namespace Moedelo.HomeV2.Dto.SpecialOffers
{
    public class SpecialOffersDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public string PromoText { get; set; }

        public string PageHeader { get; set; }

        public string PageContent { get; set; }

        public bool ShowForBiz { get; set; }

        public bool ShowForPro { get; set; }

        public bool ShowForPaid { get; set; }

        public short Weight { get; set; }
    }
}
