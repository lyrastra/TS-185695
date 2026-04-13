using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Business.Abstractions
{
    public interface IFirmRequisitesService
    {
        Task<DateTime> GetFirmRegistrationAsync();
    }
}