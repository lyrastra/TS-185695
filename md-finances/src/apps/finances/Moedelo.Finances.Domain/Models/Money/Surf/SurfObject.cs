using System;

namespace Moedelo.Finances.Domain.Models.Money.Surf
{
    public class SurfObject
    {
        public int FirmId { get; set; }

        public long DocumentBaseId { get; set; }

        public string OpType { get; set; }

        public string OpPurpose { get; set; }

        public string KontragentInn { get; set; }

        public bool IsProprietaryInn { get; set; }

        public bool IsBankInn { get; set; }

        public string RecvAcc { get; set; }

        public bool IsProprietaryRecvAcc { get; set; }

        public string SndrAcc { get; set; }

        public bool IsProprietarySndrAcc { get; set; }

        public string RecvBank { get; set; }

        public string SndrBank { get; set; }

        public decimal OpSum { get; set; }

        public DateTime OpDate { get; set; }

        public string SourceFileId { get; set; }

        public int Direction { get; set; }

        public string OKVED { get; set; }

        public string ToStrCvs()
        {
            return
                $"{FirmId};{DocumentBaseId};{OpType};{OpPurpose};{KontragentInn};{(IsProprietaryInn ? 1 : 0)};{(IsBankInn ? 1 : 0)};{RecvAcc};{(IsProprietaryRecvAcc ? 1 : 0)};{SndrAcc};{(IsProprietarySndrAcc ? 1 : 0)};{RecvBank};{SndrBank};{OpSum:##.00};{OpDate:s};{Direction};{OKVED};{SourceFileId}";
        }
    }
}
