using Moedelo.Accounting.Domain.Shared.PaymentOrders.Outgoing.BudgetaryPayments;
using Moedelo.Address.ApiClient.Abstractions.legacy;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Address.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Banks;
using Moedelo.Money.PaymentOrders.Business.SettlementAccounts;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites
{
    [InjectAsSingleton(typeof(FirmOrderDetailsGetter))]
    internal class FirmOrderDetailsGetter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;
        private readonly IAddressApiClient addressApiClient;
        private readonly ISettlementAccountsReader settlementAccountReader;
        private readonly IBankReader bankGetter;

        public FirmOrderDetailsGetter(
            IExecutionInfoContextAccessor contextAccessor,
            IFirmRequisitesApiClient firmRequisitesApiClient,
            IAddressApiClient addressApiClient,
            ISettlementAccountsReader settlementAccountReader,
            IBankReader bankGetter)
        {
            this.contextAccessor = contextAccessor;
            this.firmRequisitesApiClient = firmRequisitesApiClient;
            this.addressApiClient = addressApiClient;
            this.settlementAccountReader = settlementAccountReader;
            this.bankGetter = bankGetter;
        }

        /// <inheritdoc />
        public async Task<OrderDetails> GetAsync(int settlementAccountId)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var requisites = await firmRequisitesApiClient.GetAsync(context.FirmId).ConfigureAwait(false);
            if (requisites == null)
            {
                throw new Exception($"Not found firm by Id = {context.FirmId}");
            }

            var settlementAccount = await settlementAccountReader.GetByIdAsync(settlementAccountId).ConfigureAwait(false);
            if (settlementAccount == null)
            {
                throw new Exception($"Not found settlementAccount by Id = {settlementAccountId}");
            }

            var bank = await bankGetter.GetByIdAsync(settlementAccount.BankId).ConfigureAwait(false);
            return new OrderDetails
            {
                IsOoo = requisites.IsOoo,
                Name = GetFullName(requisites),
                Inn = requisites.Inn,
                Kpp = requisites.Kpp,
                SettlementNumber = settlementAccount.Number,
                BankName = bank?.FullNameWithCity,
                BankBik = bank?.Bik,
                BankCorrespondentAccount = bank?.CorrespondentAccount,
                BankCity = bank?.City,
                Okato = requisites.Okato,
                Oktmo = requisites.Oktmo
            };
        }

        public async Task<OrderDetails> GetForBudgetaryPaymentAsync(PaymentOrder paymentOrder)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var requisites = await firmRequisitesApiClient.GetAsync(context.FirmId).ConfigureAwait(false);
            if (requisites == null)
            {
                throw new Exception($"Not found firm by Id = {context.FirmId}");
            }

            var settlementAccount = await settlementAccountReader.GetByIdAsync(paymentOrder.SettlementAccountId).ConfigureAwait(false);
            if (settlementAccount == null)
            {
                throw new Exception($"Not found settlementAccount by Id = {paymentOrder.SettlementAccountId}");
            }

            var nameTask = GetFullNameForBudgetaryPaymentAsync(requisites, paymentOrder.Date);
            var bankTask = bankGetter.GetByIdAsync(settlementAccount.BankId);
            await Task.WhenAll(nameTask, bankTask).ConfigureAwait(false);

            var bank = bankTask.Result;
            var kpp = GetKpp(requisites, paymentOrder);

            return new OrderDetails
            {
                IsOoo = requisites.IsOoo,
                Name = nameTask.Result,
                Inn = requisites.Inn,
                Kpp = kpp,
                SettlementNumber = settlementAccount.Number,
                BankName = bank?.FullNameWithCity,
                BankBik = bank?.Bik,
                BankCorrespondentAccount = bank?.CorrespondentAccount,
                BankCity = bank?.City,
                Okato = requisites.Okato,
                Oktmo = requisites.Oktmo
                //Okato = paymentOrder.OperationType == OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment ? "0" : requisites.Okato,
                //Oktmo = paymentOrder.OperationType == OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment ? "0" : requisites.Oktmo
            };
        }

        private static string GetKpp(FirmRequisitesDto requisites, PaymentOrder paymentOrder)
        {
            if (paymentOrder.Date >= BudgetaryPaymentDates.FormatDate16052025 && paymentOrder.OperationType == OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)
            {
                return "0";
            }

            string kpp = null;
            if (requisites.IsOoo)
            {
                if (!string.IsNullOrEmpty(requisites.Kpp))
                {
                    kpp = requisites.Kpp;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(requisites.Kpp))
                {
                    kpp = "0";
                }
            }
            return kpp;
        }

        public static string GetFullName(FirmRequisitesDto requisites)
        {
            // todo: PaymentSnapshotMapper.GetFirmNameForPaymentOrderAsync (в буккипинге)
            return requisites.IsOoo
                ? requisites.Name
                : $"Индивидуальный предприниматель {GetIpFullName(requisites)}";
        }

        public async Task<string> GetFullNameForBudgetaryPaymentAsync(FirmRequisitesDto requisites, DateTime paymentDate)
        {
            if (requisites.IsOoo)
            {
                return GetFullName(requisites);
            }

            var name = GetIpFullName(requisites);

            if (paymentDate >= BudgetaryPaymentDates.FormatDate16052025)
            {
                return $"{name} (ИП)";
            }

            var context = contextAccessor.ExecutionInfoContext;
            var address = await addressApiClient.GetFirmAddressAsync((int)context.FirmId).ConfigureAwait(true);
            var addressString = GetAddressStringForBudgetaryPayment(requisites, address);

            return $"{name} (ИП) // {addressString} //";
        }

        public static string GetIpFullName(FirmRequisitesDto requisites)
        {
            if (string.IsNullOrEmpty(requisites.IpSurname))
            {
                return string.Empty;
            }

            var fio = requisites.IpSurname.Trim();

            if (string.IsNullOrEmpty(requisites.IpName))
            {
                return fio;
            }

            fio += $" {requisites.IpName.Trim()} ";

            if (string.IsNullOrEmpty(requisites.IpPatronymic))
            {
                return fio;
            }

            fio += $"{requisites.IpPatronymic.Trim()} ";

            return fio;
        }

        public static string GetAddressStringForBudgetaryPayment(FirmRequisitesDto requisites, AddressDto address)
        {
            var stringBuilder = new StringBuilder();

            var region = address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.Region);
            if (region != null && region.TypeName == "г")
            {
                stringBuilder.Append(GetAddressObjectString(region));
            }

            stringBuilder.Append(GetAddressObjectString(address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.District)));
            stringBuilder.Append(GetAddressObjectString(address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.Locality)));
            stringBuilder.Append(GetAddressObjectString(address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.PlanningStructure)));
            stringBuilder.Append(GetAddressObjectString(address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.City)));
            stringBuilder.Append(GetAddressObjectString(address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.SubArea)));
            stringBuilder.Append(GetAddressObjectString(address.AddressObjects.FirstOrDefault(x => x.Level == AddressObjectLevel.Street)));

            const string AddressObjectTemplate = "{0}. {1}";

            var house = requisites.House;
            if (!string.IsNullOrWhiteSpace(house))
            {
                house = string.Format(AddressObjectTemplate, "д", house);
            }

            stringBuilder.Append($"{house}, ");

            var building = requisites.Building;
            if (!string.IsNullOrEmpty(building))
            {
                stringBuilder.Append($"корп. {building}, ");
            }

            var flat = requisites.Flat;
            if (!string.IsNullOrWhiteSpace(flat))
            {
                var flatName = requisites.IsOoo ? "оф" : "кв";
                flat = string.Format(AddressObjectTemplate, flatName, flat);
            }
            stringBuilder.Append(flat);

            return stringBuilder.ToString();
        }

        private static string GetAddressObjectString(AddressObjectDto addressObject)
        {
            if (addressObject == null)
            {
                return string.Empty;
            }
            var format = GetFormatByAbbr(addressObject.TypeName);
            return string.Format(format, addressObject.Name, addressObject.TypeName);
        }

        private static string GetFormatByAbbr(string abbr)
        {
            switch (abbr)
            {
                case "г":
                case "Респ":
                case "ул":
                case "пер":
                case "п":
                case "тер":
                case "х":
                case "ст":
                case "ш":
                case "c":
                    return "{1}. {0}, "; // г. Пенза
                case "обл":
                    return "{0} {1}., "; // Пензенская обл.
                case "пр-т":
                case "проезд":
                case "б-р":
                    return "{1} {0}, ";  // пр-т Победы
                default:
                    return "{0} {1}, "; // Пермский край
            }
        }
    }
}