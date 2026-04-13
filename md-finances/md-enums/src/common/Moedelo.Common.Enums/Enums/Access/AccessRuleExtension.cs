using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Common.Enums.Enums.Access
{
    public static class AccessRuleExtension
    {
        private static List<AccessRule> allRuleList;

        public static List<AccessRule> GetAllRuleList()
        {
            if (allRuleList == null)
            {
                allRuleList = Enum.GetValues(typeof(AccessRule)).Cast<AccessRule>().ToList();
            }

            var ruleList = new List<AccessRule>();
            ruleList.AddRange(allRuleList);

            return ruleList;
        }

        public static List<AccessRule> GetTariffRuleList()
        {
            return new List<AccessRule>
                       {
                           AccessRule.UsnAccountantTariff,
                           AccessRule.IpWithoutWorkersTariff,
                           AccessRule.OooWithoutWorkersTariff,
                           AccessRule.WithWorkersTariff,
                           AccessRule.IpRegistrationTariff,
                           AccessRule.OooRegistrationTariff,
                           AccessRule.AccountantConsultatntTariff,
                           AccessRule.AccountantChamberTariff,
                           AccessRule.SalaryAndPersonalTariff,
                           AccessRule.AccountantConsultantSmallBusinessTariff,
                           AccessRule.AccountantChamberSmallBusinessTariff,
                           AccessRule.SalaryAndPersonalSmallBusinessTariff,
                           AccessRule.DigitalSignTariff,
                           AccessRule.AccountantConsultantMultiuserTariff,
                           AccessRule.OutsourceKnopkaTariff,
                           AccessRule.OutsourceFinguruTariff,
                           AccessRule.ProfOutsourceTariff,
                           AccessRule.OutsourceTariff,
                           AccessRule.PsbBankTariff,
                           AccessRule.IntesaBankTariff,
                           AccessRule.BizPlatform,
                       };
        }

        public static bool HaveAccessEditAdditionalDocuments(this IEnumerable<AccessRule> userFirmRules)
        {
            var accessRules = new[] { AccessRule.EditAllBillsSales, AccessRule.EditPersonalBillsSales };
            return accessRules.Any(userFirmRules.Contains);
        }

        public static bool HaveAccessViewAdditionalDocuments(this IEnumerable<AccessRule> userFirmRules)
        {
            var accessRules = new[] { AccessRule.ViewAllBillsSales, AccessRule.ViewPersonalBillsSales };
            return accessRules.Any(userFirmRules.Contains);
        }

        public static bool CheckRule(this ICollection<AccessRule> userFirmRules, AccessRule accessRule)
        {
            return userFirmRules.Contains(accessRule);
        }

        public static AccessType GetBillsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllBillsSales, AccessRule.EditPersonalBillsSales };
            var ruleForView = new[] { AccessRule.ViewAllBillsSales, AccessRule.ViewPersonalBillsSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetOutgoingStatementsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllStatementsSales, AccessRule.EditPersonalStatementsSales };
            var ruleForView = new[] { AccessRule.ViewAllStatementsSales, AccessRule.ViewPersonalStatementsSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }
        
        public static AccessType GetOutgoingUpdsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllUpdsSales, AccessRule.EditPersonalUpdsSales };
            var ruleForView = new[] { AccessRule.ViewAllUpdsSales, AccessRule.ViewPersonalUpdSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetOutgoingWaybillsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllWaybillsSales, AccessRule.EditPersonalWaybillsSales };
            var ruleForView = new[] { AccessRule.ViewAllWaybillsSales, AccessRule.ViewPersonalWaybillsSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetOutgoingInvoicesAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllInvoicesSales, AccessRule.EditPersonalInvoicesSales };
            var ruleForView = new[] { AccessRule.ViewAllInvoicesSales, AccessRule.ViewPersonalInvoicesSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetAdvanceStatementsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllAdvanceStatementsBuying, AccessRule.EditPersonalAdvanceStatementsBuying };
            var ruleForView = new[] { AccessRule.ViewAllAdvanceStatementsBuying, AccessRule.ViewPersonalAdvanceStatementsBuying };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetIncomingStatementsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllStatementsBuying, AccessRule.EditPersonalStatementsBuying };
            var ruleForView = new[] { AccessRule.ViewAllStatementsBuying, AccessRule.ViewPersonalStatementsBuying };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetIncomingWaybillsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllWaybillsBuying, AccessRule.EditPersonalWaybillsBuying };
            var ruleForView = new[] { AccessRule.ViewAllWaybillsBuying, AccessRule.ViewPersonalWaybillsBuying };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }
        
        public static AccessType GetIncomingUpdsAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllUpdsBuying, AccessRule.EditPersonalUpdsBuying };
            var ruleForView = new[] { AccessRule.ViewAllUpdsBuying, AccessRule.ViewPersonalUpdsBuying };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetIncomingInvoicesAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllInvoicesBuying, AccessRule.EditPersonalInvoicesBuying };
            var ruleForView = new[] { AccessRule.ViewAllInvoicesBuying, AccessRule.ViewPersonalInvoicesBuying };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetRetailReportAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllRetailReportsSales, AccessRule.EditPersonalRetailReportsSales };
            var ruleForView = new[] { AccessRule.ViewAllRetailReportsSales, AccessRule.ViewPersonalRetailReportsSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetAccountingStatementAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.AccessToEditAccountingStatements };
            var ruleForView = new[] { AccessRule.AccessToViewAccountingStatements };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetMiddlemanReportAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditAllMiddlemanReportsSales, AccessRule.EditPersonalMiddlemanReportsSales };
            var ruleForView = new[] { AccessRule.ViewAllMiddlemanReportsSales, AccessRule.ViewPersonalMiddlemanReportsSales };
            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetMoneyAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.EditMoney, AccessRule.SberbankWLTariff };
            var ruleForView = new[] { AccessRule.ViewMoney, AccessRule.SberbankWLTariff };

            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetCashOrderAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.AccessToEditAccountingCash };
            var ruleForView = new[] { AccessRule.AccessToViewAccountingCash };

            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetPaymentOrderAccessType(this ICollection<AccessRule> accessRules)
        {
            var ruleForEdit = new[] { AccessRule.AccessToEditAccountingBank };
            var ruleForView = new[] { AccessRule.AccessToViewAccountingBank };

            return GetAccessType(accessRules, ruleForEdit, ruleForView);
        }

        public static AccessType GetPurseOperationsAccessType(this ICollection<AccessRule> accessRules)
        {
            // у электронных кошельков не заведены собственные права
            return GetPaymentOrderAccessType(accessRules);
        }

        private static AccessType GetAccessType(ICollection<AccessRule> accessRules, ICollection<AccessRule> ruleForEdit, ICollection<AccessRule> ruleForView)
        {
            if (accessRules.Any(ruleForEdit.Contains))
            {
                return AccessType.AccessEdit;
            }

            if (accessRules.Any(ruleForView.Contains))
            {
                return AccessType.AccessView;
            }

            return AccessType.AccessDenied;
        }
    }
}