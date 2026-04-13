using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Patent
{
    internal interface IPatentReader
    {
        Task<bool> IsAnyExists(int year);

        Task<bool> IsAnyExists(DateTime date);

        Task<long?> GetPatentIdByOperationDateAsync(DateTime operationDate);
    }
}