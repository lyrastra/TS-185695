using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;

namespace Moedelo.CommonV2.UserContext.BillingContext;

[InjectPerWebRequest(typeof(IFirmBillingContext))]
public class FirmBillingContext : IFirmBillingContext
{
    private readonly IAuditContext auditContext;
    private readonly IFirmBillingContextCachingReader reader;
    private Lazy<Task<IFirmBillingContextData>> lazyDataTask;
        
    public FirmBillingContext(IAuditContext auditContext, IFirmBillingContextCachingReader reader)
    {
        this.auditContext = auditContext;
        this.reader = reader;
        Invalidate();
    }


    public async Task<IFirmBillingContextData> GetDataAsync()
    {
        try
        {
            var data = await lazyDataTask.Value.ConfigureAwait(false);
            
            return data;
        }
        catch
        {
            Invalidate();
            throw;
        }
    }

    public void Invalidate()
    {
        lazyDataTask = new Lazy<Task<IFirmBillingContextData>>(() =>
            reader.GetAsync(auditContext.FirmId.GetValueOrDefault(), auditContext.UserId.GetValueOrDefault()));
    }
}