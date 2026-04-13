using System;

namespace Moedelo.CommonV2.EventBus.Cash
{
	public class EvotorStartUpdateEvent
    {
		public int FirmId { get; set; }

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
