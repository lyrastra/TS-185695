using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class TradingObjectDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int Ifns { get; set; }

        public string Oktmo { get; set; }

        public DateTime? StopDate { get; set; }
    }
}
