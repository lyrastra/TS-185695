using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IChargePaymentDetector))]
    public class ChargePaymentDetector : IChargePaymentDetector
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IChargePaymentsApiClient chargePaymentsApiClient;
        public ChargePaymentDetector(IExecutionInfoContextAccessor contextAccessor, 
                                     IChargePaymentsApiClient chargePaymentsApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.chargePaymentsApiClient = chargePaymentsApiClient;
        }

        public async Task<List<ChargePayment>> DetectAsync(string paymentDescription, decimal paymentSum, int? employeeId)
        {
            var noChargePayment = new List<ChargePayment>{
                new ChargePayment
                {
                    Sum = paymentSum,
                    Description = "Без начисления"
                }};
            if (employeeId == null || string.IsNullOrWhiteSpace(paymentDescription))
                return noChargePayment;

            (string month, string year) = GetPeriod(paymentDescription);
            if (month == string.Empty || year == string.Empty)
                return noChargePayment;

            var chargePayments = await GetChargePaymentAsync(employeeId.Value);
            if (chargePayments == null)
                return noChargePayment;

            var result = ChargePaymentSelection(chargePayments, paymentDescription, month, year, paymentSum);
            if (result == null)
                return noChargePayment;

            return result;
        }

        private List<ChargePayment> ChargePaymentSelection(List<ChargePayment> chargePayments, string paymentDescription, string descriptionMounth, string descriptionYear, decimal paymentSum)
        {
            var result = new List<ChargePayment>();
            const int attemptsСount = 2;
            var attempt = 1;
            var isMatchMainSalary = CheckIsMatchMainSalary(paymentDescription);
            while (attempt <= attemptsСount)
            {
                foreach (var chargePayment in chargePayments)
                {
                    var chargePaymentDescription = chargePayment.Description;
                    var chargePaymentDescriptionData = chargePaymentDescription.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (chargePaymentDescriptionData.Length == 0)
                        continue;
                    var chargePaymentYear = chargePaymentDescriptionData.Last();
                    if (chargePaymentYear != descriptionYear)
                        continue;
                    var chargePaymentMounth = chargePaymentDescriptionData[chargePaymentDescriptionData.Length - 2];
                    if (chargePaymentMounth != descriptionMounth)
                        continue;
                    var chargePaymentType = chargePaymentDescriptionData.First();
                    Regex regex = new Regex(@"([а-я]|[А-Я])+");
                    MatchCollection matches = regex.Matches(chargePaymentType);
                    if (matches.Count == 0)
                    {
                        continue;
                    }
                    chargePaymentType = matches[0].Value;
                    paymentDescription = paymentDescription.ToLower();
                    chargePaymentType = chargePaymentType.ToLower();
                    if (!paymentDescription.Contains(chargePaymentType))
                    {
                        if (attempt == 1)
                            continue;

                        if (chargePaymentType.Contains("оклад"))
                        {
                            if (!isMatchMainSalary)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (paymentSum < chargePayment.Sum)
                        chargePayment.Sum = paymentSum;
                    if (paymentSum > chargePayment.Sum)
                    {
                        result.Add(chargePayment);
                        result.Add(new ChargePayment { Sum = paymentSum - chargePayment.Sum, Description = "Без начисления" });
                        return result;
                    }
                    else
                    {
                        result.Add(chargePayment);
                        return result;
                    }
                }
                attempt++;
            }
            return null;
        }

        private static (string mounth, string year) GetPeriod(string paymentDescription)
        {
            const int MonthLength = 3;
            var months = new List<string>();
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(2).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(3).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(4).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(5).ToLower());
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(6).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(7).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(8).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(9).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(10).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(11).ToLower().Remove(MonthLength));
            months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(12).ToLower().Remove(MonthLength));
            paymentDescription = Regex.Replace(paymentDescription, @"\s+", " ");
            Regex regex = new Regex(@"([а-я]|[А-Я])+\s\d{4,4}\D");
            MatchCollection matches = regex.Matches(paymentDescription);
            if (matches.Count != 1)
            {
                return (string.Empty, string.Empty);
            }
            var result = matches.Select(m => m.Value)
                    ?.Select(str => str.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                    ?.Where(pd => pd.Length == 2)?.FirstOrDefault();
            var mounth = result[0];
            if (mounth.Length < 3)
            {
                return (string.Empty, string.Empty);
            }
            mounth = mounth.Substring(0, MonthLength).ToLower();
            mounth = months.FirstOrDefault(m => m == mounth);
            if (mounth == null)
            {
                return (string.Empty, string.Empty);
            }
            var year = result[1].Length == 4 ? result[1] : result[1].Substring(0, result[1].Length - 1);
            return (mounth, year);
        }

        private async Task<List<ChargePayment>> GetChargePaymentAsync(int employeeId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var unbound = await chargePaymentsApiClient.GetUnboundForWorkerAsync(context.FirmId, context.UserId, null, employeeId, WorkerPaymentType.Employee);
            if (unbound == null || unbound.WorkerChargePayments.Count == 0 || unbound.WorkerChargePayments.ToList()[0].ChargePayments.Count == 0)
            {
                return null;
            }
            var chargePaymentsBunch = unbound.WorkerChargePayments.Select(wcp => wcp.ChargePayments.
                                 Select(cp => new ChargePayment
                                 {
                                     ChargeId = cp.ChargeId,
                                     Description = cp.Description,
                                     Sum = cp.Sum
                                 }).ToList()).ToList();
            var chargePayments = new List<ChargePayment>();
            foreach (var payments in chargePaymentsBunch)
            {
                chargePayments.AddRange(payments);
            }
            return chargePayments;
        }

        private bool CheckIsMatchMainSalary(string paymentDescription)
        {
            paymentDescription = paymentDescription.ToLower();
            if (paymentDescription.Contains("аванс"))
            {
                return true;
            }
            paymentDescription = Regex.Replace(paymentDescription, @"\s+", " ");
            Regex regex = new Regex(@"зарплат(а|ы|у)|Заработн(ая|ой|ую) плат(а|ы|у)");
            MatchCollection matches = regex.Matches(paymentDescription);
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
