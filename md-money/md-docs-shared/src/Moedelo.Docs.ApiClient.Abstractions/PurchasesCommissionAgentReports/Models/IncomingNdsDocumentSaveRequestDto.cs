namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;

public class IncomingNdsDocumentSaveRequestDto
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Тип документа. Счёт-фактура или УПД
    /// <remarks>Тип поля является enum-ом другого домена: Moedelo.LinkedDocuments.Enums.LinkedDocumentType </remarks>
    /// </summary>
    public int DocumentType { get; set; }
}