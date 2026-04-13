using System;
using Moedelo.Common.Enums.Enums.FirmActivityOffers;

namespace Moedelo.FirmActivityOffers.Client.Dto
{
    public class AddActivityRequestDto
    {
        public DateTime? Date { get; set; }
        public ActivityType Type { get; set; }
        public long? EntityId { get; set; }
        public object Value { get; set; }
    }
}
