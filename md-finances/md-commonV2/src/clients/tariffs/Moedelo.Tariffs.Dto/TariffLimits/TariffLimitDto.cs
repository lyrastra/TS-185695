using Moedelo.Common.Enums.Enums.Tools;

namespace Moedelo.Tariffs.Dto.TariffLimits
{
    public class TariffLimitDto
    {
        public int TariffId { get; set; }
        public ToolType ToolType { get; set; }
        public int MaxUsageCount { get; set; }
    }
}