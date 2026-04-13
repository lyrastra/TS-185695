using Moedelo.Common.Enums.Enums.EdsRequest;

namespace Moedelo.ErptV2.Dto.EdsRequest
{
    public class AdditionalFnsChangeDto
    {
        public string FnsCode { get; set; }
        public string Kpp { get; set; }
        public EdsRequestChangeType ChangeType { get; set; }
    }
}