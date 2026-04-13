using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Surf;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Surf
{
    public interface IPaymentOrderTo1cDocumentConfirmer : IDI
    {
        Task<List<SurfObject>> ConfirmAsync(int userId);
    }
}
