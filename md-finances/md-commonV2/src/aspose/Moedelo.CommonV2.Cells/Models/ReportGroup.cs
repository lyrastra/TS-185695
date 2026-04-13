namespace Moedelo.CommonV2.Cells.Models
{
    public class ReportGroup
    {
        public ReportGroup(int firstIndex, int lastIndex, bool isHidden = false)
        {
            FirstIndex = firstIndex;
            LastIndex = lastIndex;
            IsHidden = isHidden;
        }

        public int FirstIndex { get; set; }

        public int LastIndex { get; set; }

        public bool IsHidden { get; set; }
    }
}