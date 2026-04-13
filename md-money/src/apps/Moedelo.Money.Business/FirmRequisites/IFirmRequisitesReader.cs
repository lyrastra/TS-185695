using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.FirmRequisites
{
    internal interface IFirmRequisitesReader
    {
        Task<bool> IsOooAsync();

        Task<DateTime?> GetRegistrationDateAsync();
    }
}
