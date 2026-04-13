using System;

namespace Moedelo.PayrollV2.Dto.Fss
{
    public class Table1Dto
    {
        public Table1RowDto Row1 { get; set; } = new Table1RowDto();
        public Table1RowDto Row2 { get; set; } = new Table1RowDto();
        public Table1RowDto Row3 { get; set; } = new Table1RowDto();
        public Table1RowDto Row4 { get; set; } = new Table1RowDto();
        public decimal Row5 { get; set; }
        public decimal Row6 { get; set; }
        public decimal Row7 { get; set; }
        public DateTime Row8 { get; set; }
        public decimal Row9 { get; set; }
        public Table2RowDto Row9_1 { get; set; } = new Table2RowDto();
    }
}