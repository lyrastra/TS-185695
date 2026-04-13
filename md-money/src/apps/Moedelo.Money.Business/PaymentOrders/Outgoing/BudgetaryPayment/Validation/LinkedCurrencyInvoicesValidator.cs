using System.Linq;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [InjectAsSingleton(typeof(ILinkedCurrencyInvoicesValidator))]
    internal class LinkedCurrencyInvoicesValidator : ILinkedCurrencyInvoicesValidator
    {
        private readonly IPurchasesCurrencyInvoicesApiClient currencyInvoicesApiClient;

        public LinkedCurrencyInvoicesValidator(IPurchasesCurrencyInvoicesApiClient currencyInvoicesApiClient)
        {
            this.currencyInvoicesApiClient = currencyInvoicesApiClient;
        }

        public async Task ValidateAsync(BudgetaryPaymentSaveRequest request, Kbk kbk)
        {
            var links = request.CurrencyInvoices;
            if (links?.Any() != true)
            {
                return;
            }

            var propName = "CurrencyInvoices";
            if (request.AccountCode != BudgetaryAccountCodes.Nds)
            {
                throw new BusinessValidationException(propName, $"Инвойсы нельзя указывать в платеже с кодом счета {(int)request.AccountCode}");
            }

            if (request.KbkId == null || kbk?.Description.Contains("импорт") != true)
            {
                throw new BusinessValidationException(propName, $"Инвойсы нельзя указывать в платеже с КБК #{request.KbkId}");
            }
            
            if (request.TaxPostings?.ProvidePostingType == ProvidePostingType.ByHand)
            {
                throw new BusinessValidationException(nameof(request.TaxPostings), "Нельзя указывать налоговый учет вручную при наличии связей с инвойсами");
            }

            var firstDuplicateBaseId = links
                .GroupBy(x => x.DocumentBaseId)
                .FirstOrDefault(gr => gr.Count() > 1)?.Key; 
            if (firstDuplicateBaseId != null)
            {
                throw new BusinessValidationException(propName, $"Инвойс  #{firstDuplicateBaseId} указан более одного раза");
            }
            
            var totalLinksSum = links.Sum(x => x.LinkSum);
            if (request.Sum < totalLinksSum)
            {
                throw new BusinessValidationException(propName, $"Сумма связей с инвойсами ({totalLinksSum}) превышает сумму платежа ({request.Sum})");
            }

            var baseIds = links.Select(x => x.DocumentBaseId).ToArray();
            var currencyInvoices = await currencyInvoicesApiClient.GetByBaseIdsAsync(baseIds);
            foreach (var link in links)
            {
                var currencyInvoice = currencyInvoices.FirstOrDefault(d => d.DocumentBaseId == link.DocumentBaseId);
                if (currencyInvoice == null)
                {
                    throw new BusinessValidationException(propName, $"Инвойс  #{link.DocumentBaseId} не найден");
                }
                
                // желательно также проверить на превышение суммы НДС, хоть и некритично (текущий метод не возвращает такие данные)
            }
        }
    }
}