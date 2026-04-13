namespace Moedelo.CommonV2.EventBus.FileStorage;

public class MoveFileToFileStorageApiCommand
{
    public enum SourceBucket
    {
        Scans = 0,
        Contracts = 1
    }
    
    public SourceBucket FileType { get; set; }
    public int FirmId { get; set; }
    public long BaseId { get; set; }
    public string FileName { get; set; }
}
