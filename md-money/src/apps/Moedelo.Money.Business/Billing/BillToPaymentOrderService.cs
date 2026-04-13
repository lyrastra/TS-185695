using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Konragents.Enums;
using Moedelo.Money.Handler.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Handler.Services
{
    [InjectAsSingleton(typeof(IBillToPaymentOrderService))]
    public class BillToPaymentOrderService : IBillToPaymentOrderService
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentHistoryExApiClient paymentHistoryExApiClient;
        private readonly IPaymentHistoryApiClient paymentHistoryApiClient;
        private readonly IKontragentsApiClient kontragentsApiClient;
        private readonly IUserClient userClient;

        public BillToPaymentOrderService(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentHistoryExApiClient paymentHistoryExApiClient,
            IPaymentHistoryApiClient paymentHistoryApiClient,
            IKontragentsApiClient kontragentsApiClient,
            IUserClient userClient)
        {
            this.paymentHistoryExApiClient = paymentHistoryExApiClient;
            this.paymentHistoryApiClient = paymentHistoryApiClient;
            this.kontragentsApiClient = kontragentsApiClient;
            this.contextAccessor = contextAccessor;
            this.userClient = userClient;
        }

        public async Task<BillToPaymentOrderModel> GetBillAsync(string billNumber)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var criteria = new PaymentHistoryExRequestDto
            {
                BillNumbers = new string[] { billNumber }
            };

            var billsByNumber = await paymentHistoryExApiClient.GetAsync(criteria);

            if (billsByNumber == null)
            {
                return null;
            }

            var paymentHistoryId = billsByNumber.Select(x => x.PaymentHistoryId).FirstOrDefault();

            var billPositions = await paymentHistoryApiClient.GetPositionsAsync(paymentHistoryId);

            if (billPositions == null)
            {
                return null;
            }

            var billKontragent = await paymentHistoryExApiClient.GetPaymentHistoryExBillDataAsync(paymentHistoryId);

            List<string> KontragentInn = new();
            KontragentInn.Add(billKontragent.PayeeRequisites.Inn);

            var kontragentsDto = await kontragentsApiClient.GetByInnsAsync(context.FirmId, context.UserId, KontragentInn);

            var kontragentId = kontragentsDto.Select(x => x.Id).FirstOrDefault();

            var baseUserInfo = await userClient.GetUserInfoByIdAsync((int)context.UserId);

            if (kontragentId == 0)
            {
                var kontragentDto = new KontragentDto
                {
                    Inn = billKontragent.PayeeRequisites.Inn,
                    Ogrn = billKontragent.PayeeRequisites.Ogrn,
                    FullName = billKontragent.PayeeRequisites.FirmName,
                    ShortName = billKontragent.PayeeRequisites.FirmName,
                    Form = KontragentForm.UL,
                    LegalAddress = billKontragent.PayeeRequisites.Address,
                    ActualAddress = billKontragent.PayeeRequisites.Address,
                    RegistrationAddress = billKontragent.PayeeRequisites.Address,
                    SettlementAccounts = new List<KontragentSettlementAccountDto>
                    {
                        new KontragentSettlementAccountDto
                        {
                            Number = billKontragent.PayeeRequisites.SettlementAccount,
                            BankName = billKontragent.PayeeRequisites.BankName
                        }
                    }
                };

                kontragentId = await kontragentsApiClient.SaveAsync(context.FirmId, context.UserId, kontragentDto);
            }

                return Map(billNumber, billPositions, kontragentId, billKontragent, baseUserInfo);

        }

        private BillToPaymentOrderModel Map(string billNumber,
            List<PaymentPositionDto> billPositions,
            int kontragentId,
            PaymentHistoryExBillDataDto billKontragent,
            BaseUserInfoDto baseUserInfo)
        {
            return new BillToPaymentOrderModel
            {
                Date = DateTime.Now,
                RecipientId = kontragentId,
                RecipientName = billKontragent.PayeeRequisites.FirmName,
                RecipientInn = billKontragent.PayeeRequisites.Inn,
                RecipientKpp = billKontragent.PayeeRequisites.Kpp,
                RecipientSettlementAccount = billKontragent.PayeeRequisites.SettlementAccount,
                RecipientBankName = billKontragent.PayeeRequisites.BankName,
                RecipientBankBik = billKontragent.PayeeRequisites.BankBik,
                Sum = billPositions.Sum(x => x.Price),
                Description = $"{baseUserInfo.Login} Оплата по счету № {billNumber} от {DateTime.Now:dd.MM.yyyy}. НДС не облагается"
            };
        }
    }
}