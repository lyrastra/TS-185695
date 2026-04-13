using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.FlcNetCore
{
    public class Request
    {
        public int FirmId { get; }

        public IReadOnlyList<FileDetail> Files { get; }

        public Request(int firmId, IReadOnlyList<FileDetail> files)
        {
            FirmId = firmId;
            Files = files;
        }
    }
}