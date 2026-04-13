using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Handler.Dto;

namespace Moedelo.Money.Handler.Mappers
{
    internal static class BillToPaymentOrderMapper
    {
        public static PaymentToSupplierResponseDto Map(BillToPaymentOrderModel model)
        {
            return new PaymentToSupplierResponseDto
            {
                Date = model.Date,
                Contractor = new ContractorResponseDto
                {
                    Id = model.RecipientId,
                    Name = model.RecipientName,
                    Form = 1,
                    Inn = model.RecipientInn,
                    Kpp = model.RecipientKpp,
                    SettlementAccount = model.RecipientSettlementAccount,
                    BankName = model.RecipientBankName,
                    BankBik = model.RecipientBankBik
                },
                Contract = new RemoteServiceResponseDto<ContractDto>
                {
                    Status = 0,
                    Data = null
                },
                Sum = model.Sum,
                Description = model.Description
            };
        }
    }
}
