using Aspose.Cells;

namespace Moedelo.CommonV2.Cells.Models.Mergefield
{
    public class RowInfo
    {
        public RowInfo(CellArea cellArea)
        {
            CellArea = cellArea;
        }
        
        public CellArea CellArea { get; private set; }
    }
}