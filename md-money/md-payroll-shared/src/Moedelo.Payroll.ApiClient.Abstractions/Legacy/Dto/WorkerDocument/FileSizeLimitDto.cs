namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerDocument;

public class FileSizeLimitDto
{
    /// <summary>
    /// Лимит на максимальный размер одного загружаемого файла
    /// </summary>
    public long FileSize { get; set; }
    /// <summary>
    /// Лимит на общий максимальный размер загруженных файлов
    /// </summary>
    public long FilesSize { get; set; }
}