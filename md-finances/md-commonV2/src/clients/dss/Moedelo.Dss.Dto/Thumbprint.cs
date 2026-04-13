using Moedelo.Dss.Dto.Enum;

namespace Moedelo.Dss.Dto
{
    public class Thumbprint
    {
        public string Value { get; set; }//deserealize guid fix

        public ThumbprintStatus Status { get; set; }
    }
}