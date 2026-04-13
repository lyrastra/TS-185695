using System;

namespace Moedelo.CommonV2.EventBus.Cash
{
	public class YandexMovementsRequestedEvent
	{
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public int FirmId { get; set; }
	}
}
