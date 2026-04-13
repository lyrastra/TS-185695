using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerDocument;

public class DeleteFromStorageRequestDto
{
    public IReadOnlyCollection<string> StorageFileIds { get; set; }
}