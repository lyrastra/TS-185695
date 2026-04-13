using Moedelo.CommonV2.Cells.Enums;

namespace Moedelo.CommonV2.Cells.Models.Mergefield
{
    public class ColumnInfo
    {
        public string Name { get; set; }
        
        public int Index { get; set; }
        
        public InsertDataType DataType { get; set; }
    }
}