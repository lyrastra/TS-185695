using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.Phones
{
    public class PhoneDto
    {
        public string Number { get; set; }

        public PhoneTypes Type { get; set; }

        public bool IsConfirmed { get; set; }
    }
}