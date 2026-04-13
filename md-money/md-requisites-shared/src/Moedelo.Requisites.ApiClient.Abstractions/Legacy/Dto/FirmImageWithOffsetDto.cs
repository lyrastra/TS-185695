using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legasy.Dto
{
    public class FirmImageWithOffsetDto
    {
        public byte[] Content { get; set; }
        public byte OffsetPct { get; set; } = 0;
    }
}
