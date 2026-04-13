using System;

namespace Moedelo.Eds.Dto.EdsStatus
{
    public sealed class RevokedEdsInfo
    {
        public bool IsCurrentEdsRevoked { get; set; }
        public DateTime RevokeDate { get; set; }

        public DateTime? GetRevokeDate() => IsCurrentEdsRevoked ? (DateTime?)RevokeDate : null;
    }
}