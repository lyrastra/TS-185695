namespace Moedelo.Infrastructure.Aspose.Cells.Abstraction
{
    public interface IWorksheet
    {
        string WorksheetName { get; set; }
        int WorksheetIndex { get; set; }
        object Model { get; set; }
    }
}