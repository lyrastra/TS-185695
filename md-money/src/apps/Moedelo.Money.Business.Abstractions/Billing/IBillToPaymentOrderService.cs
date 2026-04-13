using Moedelo.Money.Handler.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Handler.Services
{
    public interface IBillToPaymentOrderService
    {
        Task<BillToPaymentOrderModel> GetBillAsync(string billNumber);
    }
}