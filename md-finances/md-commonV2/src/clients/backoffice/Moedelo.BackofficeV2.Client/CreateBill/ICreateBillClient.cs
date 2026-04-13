using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.CreateBill;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.CreateBill
{
    public interface ICreateBillClient : IDI
    {
        Task CreateAsync(BillCreationUserContextRequestDto clientData);
    }
}