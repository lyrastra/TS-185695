using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.HomeV2.Client.Phone.Models
{
    public class FirmsPhonesRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }
        public IReadOnlyCollection<PhoneTypes> OnlyTypes { get; set; }
    }
}