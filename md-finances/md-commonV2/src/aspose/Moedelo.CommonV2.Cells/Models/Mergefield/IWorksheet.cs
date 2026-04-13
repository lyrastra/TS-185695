namespace Moedelo.CommonV2.Cells.Models.Mergefield
{
    public interface IWorksheet
    {
        string WorksheetName { get; set; }
        int WorksheetIndex { get; set; }
        object Model { get; set; }
    }
}