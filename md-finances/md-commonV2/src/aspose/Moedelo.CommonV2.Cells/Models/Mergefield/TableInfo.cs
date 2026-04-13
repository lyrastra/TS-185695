using System;
using System.Collections.Generic;
using Aspose.Cells;

namespace Moedelo.CommonV2.Cells.Models.Mergefield
{
    public class TableInfo
    {
        public TableInfo(CellArea cellArea)
        {
            this.CellArea = cellArea;
        }
        
        public CellArea CellArea { get; private set; }
        
        public List<RowInfo> Rows { get; } = new List<RowInfo>();
        
        public List<ColumnInfo> Columns { get; set; }
        
        public TableOptions Options { get; set; }

        public void AddRowInfo(RowInfo info)
        {
            Rows.Add(info);
            ChangeCellAreaEndRow(info.CellArea.EndRow);
        }
        
        private void ChangeCellAreaEndRow(int endRow)
        {
            if (endRow < CellArea.StartRow)
            {
                throw new Exception("CellArea EndRow can not be less then the StartRow");
            }

            var cellArea = CellArea;
            cellArea.EndRow = endRow;

            CellArea = cellArea;
        }
    }
}