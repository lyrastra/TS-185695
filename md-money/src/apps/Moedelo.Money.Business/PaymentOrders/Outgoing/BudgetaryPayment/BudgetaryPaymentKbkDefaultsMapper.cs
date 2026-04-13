using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    static class BudgetaryPaymentKbkDefaultsMapper
    {
        private static readonly BudgetaryAccountCodes[] AccountsCodeWithStatusByFirm = new[]
        {
            BudgetaryAccountCodes.Nds,
            BudgetaryAccountCodes.TradingFees,
            BudgetaryAccountCodes.OtherTaxes,
            BudgetaryAccountCodes.Eshn,
            BudgetaryAccountCodes.EnvdForUsn
        };

        public static BudgetaryKbkDefaultsResponse Map(Kbk kbk, TradingObjectDto tradingObject, BudgetaryPeriod budgetaryPeriod, BudgetaryRecipient fundRequisites, FirmRequisitesDto firmRequisites)
        {
            return new BudgetaryKbkDefaultsResponse
            {
                PayerStatus = GetBudgetaryPayerStatus(kbk.AccountCode, kbk.PayerStatus, firmRequisites.IsOoo),
                PaymentBase = kbk.PaymentBase,
                DocNumber = GetBudgetaryDocNumber(kbk, firmRequisites),
                PaymentType = GetBudgetaryType(kbk.KbkPaymentType),
                Recipient = fundRequisites,
                Description = CreateDescription(kbk, budgetaryPeriod, tradingObject, firmRequisites)
            };
        }

        public static BudgetaryPayerStatus GetBudgetaryPayerStatus(BudgetaryAccountCodes kbkAccountCode, BudgetaryPayerStatus payerStatus, bool isOoo)
        {
            if (AccountsCodeWithStatusByFirm.Contains(kbkAccountCode))
            {
                return isOoo ? BudgetaryPayerStatus.Company : BudgetaryPayerStatus.TaxpayerIP.GetActualIpBudgetaryPayerStatus();
            }
            return payerStatus;
        }

        public static string GetBudgetaryDocNumber(Kbk kbk, FirmRequisitesDto firmRequisites)
        {
            if (kbk.DocNumber == null || kbk.DocNumber == "0")
            {
                return null;
            }

            var isKbkAreTaxPayment = kbk.AccountCode.ToString().StartsWith("68");
            var foundationDocNumber = isKbkAreTaxPayment
                ? firmRequisites.PfrEmployer
                : firmRequisites.Snils;
            return $"{kbk.DocNumber}{foundationDocNumber}";
        }

        public static BudgetaryPaymentType GetBudgetaryType(KbkPaymentType paymentType)
        {
            return paymentType == KbkPaymentType.Surcharge
                ? BudgetaryPaymentType.Peni
                : BudgetaryPaymentType.Other;
        }

        public static string CreateDescription(Kbk kbk, BudgetaryPeriod period, TradingObjectDto tradingObject, FirmRequisitesDto firmRequisites)
        {
            var descriptionTemplate = GetDescriptionTemplate(kbk, period, tradingObject);
            if (string.IsNullOrWhiteSpace(descriptionTemplate))
            {
                return string.Empty;
            }

            return string.Format(descriptionTemplate,
                period.AsString().ToLower(), // {0} in description template
                GetNumberByType(kbk.KbkType, firmRequisites), // {1}
                firmRequisites.Tfoms,  // {2}
                GetFormattedTradingObjectNumber(tradingObject) // {3}
                );
        }

        private static string GetDescriptionTemplate(Kbk kbk, BudgetaryPeriod period, TradingObjectDto tradingObject)
        {
            if (tradingObject != null)
            {
                var periodString = period.AsString().ToLower();
                var tradingObjectNumber = GetFormattedTradingObjectNumber(tradingObject);
                switch (kbk.KbkPaymentType)
                {
                    case KbkPaymentType.Payment:
                        return $"#{tradingObjectNumber} Торговый сбор за {periodString}. НДС не облагается";
                    case KbkPaymentType.Surcharge:
                        return $"#{tradingObjectNumber} Уплата пени по торговому сбору за {periodString}. НДС не облагается";
                    case KbkPaymentType.Forfeit:
                        return $"#{tradingObjectNumber} Уплата штрафа по торговому сбору за {periodString}. НДС не облагается";
                }
            }

            return period.Type == BudgetaryPeriodType.NoPeriod
                ? FormatDescriptionForEmptyPeriod(kbk.Purpose)
                : kbk.Purpose;
        }

        private static string GetFormattedTradingObjectNumber(TradingObjectDto tradingObject)
        {
            return tradingObject?.Number.ToString("000") ?? string.Empty;
        }

        private static string FormatDescriptionForEmptyPeriod(string description)
        {
            return Regex.Replace(description, @"\sза (.*?)\.", ".");
        }

        private static string GetNumberByType(KbkType type, FirmRequisitesDto firmRequisites)
        {
            switch (type)
            {
                case KbkType.InsurancePayForEmployees:
                case KbkType.AccumulatePayForEmployees:
                case KbkType.FederalFoms:
                    return firmRequisites.PfrEmployer;
                case KbkType.InsurancePaymentForIp:
                case KbkType.InsuranceAdditionalPayList1:
                case KbkType.InsuranceAdditionalPayList2:
                case KbkType.AccumulatePaymentForIp:
                case KbkType.FederalFomsForIp:
                case KbkType.TerretorialFoms:
                case KbkType.InsuranceForfeitByViolationOfLegislation:
                case KbkType.InsuranceForFailureToDocument:
                case KbkType.InsurancePayOverdraft:
                    return firmRequisites.Pfr;
                case KbkType.FssDisabilityForEmployees:
                case KbkType.FssVoluntaryContributions:
                case KbkType.FssForfeitByViolationOfLegislation:
                case KbkType.FssInjuryForEmployees:
                    return firmRequisites.FssNumber;
                default:
                    return string.Empty;
            }
        }
    }
}
