using System.Collections.Generic;

namespace Moedelo.CommonV2.Cells.Models
{
    public class Color
    {
        public Color(int firstColumnIndex, int lastColumnIndex, List<int> rows)
        {
            FirstColumnIndex = firstColumnIndex;
            LastColumnIndex = lastColumnIndex;
            Rows = rows;
        }

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex { get; set; }

        public List<int> Rows { get; set; }
    }
}