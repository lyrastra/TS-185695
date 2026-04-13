using System;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AccessExtensions
{
    public static class UserContextAccessExtensions
    {
        public static Task<AccessType> GetBillsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllBillsSales, AccessRule.EditPersonalBillsSales };
            var ruleForView = new[] { AccessRule.ViewAllBillsSales, AccessRule.ViewPersonalBillsSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetBillsAccessType(this IUserContext userContext)
        {
            return userContext.GetBillsAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetOutgoingStatementsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllStatementsSales, AccessRule.EditPersonalStatementsSales };
            var ruleForView = new[] { AccessRule.ViewAllStatementsSales, AccessRule.ViewPersonalStatementsSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetOutgoingStatementsAccessType(this IUserContext userContext)
        {
            return userContext.GetOutgoingStatementsAccessTypeAsync().Result;
        }
        
        public static Task<AccessType> GetOutgoingUpdsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllUpdsSales, AccessRule.EditPersonalUpdsSales };
            var ruleForView = new[] { AccessRule.ViewAllUpdsSales, AccessRule.ViewPersonalUpdSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetOutgoingUpdsAccessType(this IUserContext userContext)
        {
            return userContext.GetOutgoingUpdsAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetOutgoingWaybillsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllWaybillsSales, AccessRule.EditPersonalWaybillsSales };
            var ruleForView = new[] { AccessRule.ViewAllWaybillsSales, AccessRule.ViewPersonalWaybillsSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetOutgoingWaybillsAccessType(this IUserContext userContext)
        {
            return userContext.GetOutgoingWaybillsAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetOutgoingInvoicesAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllInvoicesSales, AccessRule.EditPersonalInvoicesSales };
            var ruleForView = new[] { AccessRule.ViewAllInvoicesSales, AccessRule.ViewPersonalInvoicesSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetOutgoingInvoicesAccessType(this IUserContext userContext)
        {
            return userContext.GetOutgoingInvoicesAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetAdvanceStatementsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllAdvanceStatementsBuying, AccessRule.EditPersonalAdvanceStatementsBuying };
            var ruleForView = new[] { AccessRule.ViewAllAdvanceStatementsBuying, AccessRule.ViewPersonalAdvanceStatementsBuying };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetAdvanceStatementsAccessType(this IUserContext userContext)
        {
            return userContext.GetAdvanceStatementsAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetIncomingStatementsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllStatementsBuying, AccessRule.EditPersonalStatementsBuying };
            var ruleForView = new[] { AccessRule.ViewAllStatementsBuying, AccessRule.ViewPersonalStatementsBuying };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetIncomingStatementsAccessType(this IUserContext userContext)
        {
            return userContext.GetIncomingStatementsAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetIncomingWaybillsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllWaybillsBuying, AccessRule.EditPersonalWaybillsBuying };
            var ruleForView = new[] { AccessRule.ViewAllWaybillsBuying, AccessRule.ViewPersonalWaybillsBuying };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetIncomingWaybillsAccessType(this IUserContext userContext)
        {
            return userContext.GetIncomingWaybillsAccessTypeAsync().Result;
        }
        
        public static Task<AccessType> GetIncomingUpdsAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllUpdsBuying, AccessRule.EditPersonalUpdsBuying };
            var ruleForView = new[] { AccessRule.ViewAllUpdsBuying, AccessRule.ViewPersonalUpdsBuying };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetIncomingUpdsAccessType(this IUserContext userContext)
        {
            return userContext.GetIncomingUpdsAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetIncomingInvoicesAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllInvoicesBuying, AccessRule.EditPersonalInvoicesBuying };
            var ruleForView = new[] { AccessRule.ViewAllInvoicesBuying, AccessRule.ViewPersonalInvoicesBuying };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetIncomingInvoicesAccessType(this IUserContext userContext)
        {
            return userContext.GetIncomingInvoicesAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetRetailReportAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllRetailReportsSales, AccessRule.EditPersonalRetailReportsSales };
            var ruleForView = new[] { AccessRule.ViewAllRetailReportsSales, AccessRule.ViewPersonalRetailReportsSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetRetailReportAccessType(this IUserContext userContext)
        {
            return userContext.GetRetailReportAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetAccountingStatementAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.AccessToEditAccountingStatements };
            var ruleForView = new[] { AccessRule.AccessToViewAccountingStatements };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetAccountingStatementAccessType(this IUserContext userContext)
        {
            return userContext.GetAccountingStatementAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetMiddlemanReportAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditAllMiddlemanReportsSales, AccessRule.EditPersonalMiddlemanReportsSales };
            var ruleForView = new[] { AccessRule.ViewAllMiddlemanReportsSales, AccessRule.ViewPersonalMiddlemanReportsSales };
            
            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetMiddlemanReportAccessType(this IUserContext userContext)
        {
            return userContext.GetMiddlemanReportAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetMoneyAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.EditMoney, AccessRule.SberbankWLTariff };
            var ruleForView = new[] { AccessRule.ViewMoney, AccessRule.SberbankWLTariff };

            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetMoneyAccessType(this IUserContext userContext)
        {
            return userContext.GetMoneyAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetCashOrderAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.AccessToEditAccountingCash };
            var ruleForView = new[] { AccessRule.AccessToViewAccountingCash };

            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetCashOrderAccessType(this IUserContext userContext)
        {
            return userContext.GetCashOrderAccessTypeAsync().Result;
        }

        public static Task<AccessType> GetPaymentOrderAccessTypeAsync(this IUserContext userContext)
        {
            var ruleForEdit = new[] { AccessRule.AccessToEditAccountingBank };
            var ruleForView = new[] { AccessRule.AccessToViewAccountingBank };

            return GetAccessTypeAsync(userContext, ruleForEdit, ruleForView);
        }
        
        [Obsolete("Use Async")]
        public static AccessType GetPaymentOrderAccessType(this IUserContext userContext)
        {
            return userContext.GetPaymentOrderAccessTypeAsync().Result;
        }
        
        public static Task<AccessType> GetPurseOperationsAccessTypeAsync(this IUserContext userContext)
        {
            // у электронных кошельков не заведены собственные права
            return GetPaymentOrderAccessTypeAsync(userContext);
        }

        [Obsolete("Use Async")]
        public static AccessType GetPurseOperationsAccessType(this IUserContext userContext)
        {
            return userContext.GetPurseOperationsAccessTypeAsync().Result;
        }

        private static async Task<AccessType> GetAccessTypeAsync(IUserContext context, AccessRule[] ruleForEdit, AccessRule[] ruleForView)
        {
            if (await context.HasAnyRuleAsync(ruleForEdit).ConfigureAwait(false))
            {
                return AccessType.AccessEdit;
            }

            if (await context.HasAnyRuleAsync(ruleForView).ConfigureAwait(false))
            {
                return AccessType.AccessView;
            }

            return AccessType.AccessDenied;
        }
    }
}