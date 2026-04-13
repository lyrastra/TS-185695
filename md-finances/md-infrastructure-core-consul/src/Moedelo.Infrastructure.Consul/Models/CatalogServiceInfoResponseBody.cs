namespace Moedelo.Infrastructure.Consul.Models;

/// <summary>
/// Тело ответа на вызов Consul api /v1/catalog/service/{ServiceName} 
/// </summary>
internal struct CatalogServiceInfoResponseBody
{
    // ReSharper disable once InconsistentNaming
    public string ServiceID { get; set; } 
}
