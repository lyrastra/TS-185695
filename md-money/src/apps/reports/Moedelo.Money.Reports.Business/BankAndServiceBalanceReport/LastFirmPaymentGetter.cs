using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Billing.Abstractions.Legacy.Interfaces.Tariffs;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Reports.Business.BankAndServiceBalanceReport.Models;
using Moedelo.Money.Reports.Business.Extensions;
using TariffDto = Moedelo.Billing.Abstractions.Legacy.Dto.Tariffs.TariffDto;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport
{
    [InjectAsSingleton(typeof(LastFirmPaymentGetter))]
    internal class LastFirmPaymentGetter
    {
        private readonly IPaymentHistoryApiClient paymentHistoryApiClient;
        private readonly IPriceListApiClient priceListApiClient;
        private readonly ITariffApiClient tariffApiClient;

        public LastFirmPaymentGetter(
            IPaymentHistoryApiClient paymentHistoryApiClient,
            IPriceListApiClient priceListApiClient,
            ITariffApiClient tariffApiClient)
        {
            this.paymentHistoryApiClient = paymentHistoryApiClient;
            this.priceListApiClient = priceListApiClient;
            this.tariffApiClient = tariffApiClient;
        }

        public async Task<IReadOnlyCollection<LastFirmPayment>> GetAsync(DateTime reportDate)
        {
            var payments = await paymentHistoryApiClient.GetByCriteriaAsync(new PaymentHistoryRequestDto
            {
                ExcludePaymentMethods = new[] { "trial" },
                Success = true,
                IsRefund = false,
                StartDateBefore = reportDate,
                ExpirationDateAfter = reportDate
            });

            var paymentIds = payments.Select(x => x.Id).ToArray();
            var positionsByPaymentId = await GetPositionsByPaymentIdsAsync(paymentIds);

            var priceListIds = payments
                .Select(x => x.PriceListId)
                .Distinct()
                .ToArray();
            var priceLists = await priceListApiClient
                .GetByIdsAsync(priceListIds);

            var tariffIds = priceLists
                .Select(x => x.TariffId)
                .Distinct()
                .ToArray();
            var tariffs = await tariffApiClient.GetListAsync(tariffIds);

            var priceListById = priceLists.ToDictionary(x => x.Id);
            var tariffById = tariffs.ToDictionary(x => x.Id);

            return payments
                .Select(x => MapPaidFirm(x, positionsByPaymentId, priceListById, tariffById))
                .Where(x => x != null)
                .GroupBy(x => x.FirmId)
                .Select(r => r
                    .OrderByDescending(x => x.ExpirationDate)
                    .ThenByDescending(x => x.PaymentId)
                    .First())
                .ToArray();
        }

        private async Task<IReadOnlyDictionary<int, PaymentPositionDto[]>> GetPositionsByPaymentIdsAsync(
            IReadOnlyCollection<int> paymentIds)
        {
            var result = new Dictionary<int, PaymentPositionDto[]>();
            
            var paymentIdsGroups = DividerIntoGroups.Divide(paymentIds, 10_000);
            foreach (var paymentIdsGroup in paymentIdsGroups)
            {
                var positionsByPaymentId = await paymentHistoryApiClient.GetPositionsAsync(paymentIdsGroup);

                foreach (var (paymentId, positions) in positionsByPaymentId)
                {
                    result.Add(paymentId, positions.ToArray());
                }
            }

            return result;
        }

        private LastFirmPayment MapPaidFirm(
            PaymentHistoryDto payment,
            IReadOnlyDictionary<int, PaymentPositionDto[]> positionsByPaymentId,
            IReadOnlyDictionary<int, PriceListDto> priceListById,
            IReadOnlyDictionary<int, TariffDto> tariffById)
        {
            if (payment.ExpirationDate == null)
            {
                return null;
            }

            var positions = positionsByPaymentId.GetValueOrDefault(payment.Id);

            positions = positions?
                .Where(x => !x.Name.Contains("опция"))
                .ToArray();

            var positionProducts = positions?
                .Select(x => x.ProductCode)
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToArray() ?? Array.Empty<string>();
            if (positionProducts.Any())
            {
                return new LastFirmPayment
                {
                    FirmId = payment.FirmId,
                    Tariff = string.Join(", ", positions.Select(x => x.Name)),
                    Product = GetProductName(positionProducts),
                    PaymentId = payment.Id,
                    ExpirationDate = payment.ExpirationDate.Value
                };
            }

            var priceList = priceListById.GetValueOrDefault(payment.PriceListId);
            if (priceList == null)
            {
                return null;
            }

            var tariff = tariffById.GetValueOrDefault(priceList.TariffId);
            if (tariff == null)
            {
                return null;
            }

            if (tariff.Platform != "ACC")
            {
                return null;
            }

            return new LastFirmPayment
            {
                FirmId = payment.FirmId,
                Tariff = priceList.Name,
                Product = GetProductName(new[] { tariff.Group }),
                PaymentId = payment.Id,
                ExpirationDate = payment.ExpirationDate.Value
            };
        }

        private string GetProductName(IReadOnlyCollection<string> productCodes)
        {
            var outsourceProductCodes = new[] { "OUT", "OUT-ACC", "OUT-BIZ" };

            return productCodes.Any(x => outsourceProductCodes.Contains(x))
                ? "АУТ"
                : "ИБ";
        }       
    }
}
