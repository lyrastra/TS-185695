using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Models.Money.Autocomplete;
using Moedelo.Finances.Public.ClientData.Autocomplete;

namespace Moedelo.Finances.Public.Controllers.Money.Operations
{
    [RoutePrefix("Money/Operations/RefundPayments")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MoneyOperationsRefundPaymentsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IPaymentAutocompleteService paymentAutocompleteService;

        public MoneyOperationsRefundPaymentsController(
            IUserContext userContext,
            IPaymentAutocompleteService paymentAutocompleteService)
        {
            this.userContext = userContext;
            this.paymentAutocompleteService = paymentAutocompleteService;
        }

        /// <summary>
        /// Возвращает список платежей по возвратам 
        /// </summary>
        /// /// <param name="request">Критерии выборки</param>
        [HttpGet]
        [Route("Autocomplete")]
        public async Task<IHttpActionResult> GetAutocompleteAsync([FromUri] RefundPaymentAutocompleteRequest request)
        {
            var result = await paymentAutocompleteService.GetByCriterionAsync(
                userContext,
                new PaymentAutocompleteCriterion
                {
                    Query = request.Query,
                    KontragentId = request.KontragentId,
                    OperationTypes = new List<OperationType>
                    {
                        OperationType.PaymentOrderOutgoingReturnToBuyer,
                        OperationType.CashOrderOutgoingReturnToBuyer
                    },
                    RetailRefundBaseId = request.RetailRefundBaseId,
                    Offset = request.Offset,
                    Limit = request.Limit,
                    ExcludeAccountCodes = request.ExcludeAccountCodes
                }).ConfigureAwait(false);

            return Frame(result.List.Select(payment => new RefundPaymentAutocompleteItemClientData
                    {
                        Id = payment.DocumentBaseId,
                        KontragentId = payment.KontragentId,
                        KontragentName = payment.KontragentName,
                        Type = payment.DocumentType,
                        Number = payment.Number,
                        Date = payment.Date,
                        Sum = payment.Sum
                    }
                ).ToList(),
                result.Offset,
                result.Limit,
                result.TotalCount);
        }
    }
}