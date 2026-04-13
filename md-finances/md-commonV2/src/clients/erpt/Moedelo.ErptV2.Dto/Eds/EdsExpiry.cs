using System;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsExpiry
    {
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired { get; set; }
        public TimeSpan TimeBeforeExpiry { get; set; }
        public bool IsProlongationAble { get; set; }
    }
}