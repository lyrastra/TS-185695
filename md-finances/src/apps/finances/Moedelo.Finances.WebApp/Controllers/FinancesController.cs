using Moedelo.AccountV2.Client.Account;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Account;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.CommonApiV2.Client.FirmFlags;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.Utils.ServerUrl;
using Moedelo.RequisitesV2.Client.FirmTaxationSystem;
using Moedelo.RequisitesV2.Client.Phones;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Moedelo.Finances.WebApp.Controllers
{
    public class FinancesController : BaseContoroller
    {
        private readonly IUserContext userContext;
        private readonly IServerUriService serverUriService;
        private readonly IFirmTaxationSystemClient taxationSystemClient;
        private readonly IAccountApiClient accountApiClient;
        private readonly IFirmFlagsApiClient firmFlagsApiClient;
        private readonly IPhoneClient phoneClient;

        public FinancesController(
            IUserContext userContext,
            IServerUriService serverUriService,
            IFirmTaxationSystemClient taxationSystemClient,
            IAccountApiClient accountApiClient,
            IFirmFlagsApiClient firmFlagsApiClient,
            IPhoneClient phoneClient)
        {
            this.userContext = userContext;
            this.serverUriService = serverUriService;
            this.taxationSystemClient = taxationSystemClient;
            this.accountApiClient = accountApiClient;
            this.firmFlagsApiClient = firmFlagsApiClient;
            this.phoneClient = phoneClient;
        }

        public async Task<ActionResult> Index()
        {
            var hasAccessToFinances = await userContext.HasAnyRuleAsync(AccessRule.AccessToViewAccountingBank, AccessRule.AccessToViewAccountingCash).ConfigureAwait(false);
            if (hasAccessToFinances == false)
            {
                return Redirect($"{serverUriService.GetBaseUrl(Request.Url)}/Main");
            }

            var getTaxationSystemsTask = GetTaxationSystemsAsync();
            var canViewPostingsTask = userContext.HasAllRuleAsync(AccessRule.ViewPostings);
            var getAccessRulesTask = GetAccessRuleModelAsync();
            var contextExtraData = userContext.GetContextExtraDataAsync();
            await Task.WhenAll(getTaxationSystemsTask, canViewPostingsTask, getAccessRulesTask, contextExtraData).ConfigureAwait(false);

            @ViewBag.TaxationSystems = JsonConvert.SerializeObject(getTaxationSystemsTask.Result, Formatting.Indented);
            @ViewBag.CanViewPosting = canViewPostingsTask.Result;

            @ViewBag.AccessRule = JsonConvert.SerializeObject(getAccessRulesTask.Result, Formatting.Indented);
            @ViewBag.IsOoo = contextExtraData.Result?.IsOoo;
            var isProfOutsource = await accountApiClient.GetAccountByUserIdAsync(userContext.UserId).ConfigureAwait(false);
            @ViewBag.IsProfOutsource = isProfOutsource?.Type == AccountType.ProfOutsource;

            return View();
        }

        public ActionResult PaymentImportRules()
        {
            return View();
        }

        public ActionResult Example()
        {
            @ViewBag.UserGuid = ToGuid(userContext.UserId);
            return View();
        }

        public async Task<ActionResult> ExampleEgrip()
        {
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var phones = await phoneClient.GetAllAsync(userContext.FirmId).ConfigureAwait(false);
            var phone = phones.FirstOrDefault(x => x.Type == PhoneTypes.Requisites) ?? phones.FirstOrDefault(x => x.Type == PhoneTypes.FromRegistrationForm);

            @ViewBag.UserGuid = ToGuid(userContext.UserId);
            @ViewBag.Inn = contextExtraData.Inn;
            @ViewBag.Email = contextExtraData.Login;
            @ViewBag.Phone = phone.Number;

            return View();
        }

        public static Guid ToGuid(int value)
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        private async Task<object> GetAccessRuleModelAsync()
        {
            var access = new AccessMode();

            var hasAccessToEditAccountingBank = userContext.HasAllRuleAsync(AccessRule.AccessToEditAccountingBank);
            var hasAccessToViewAccountingBank = userContext.HasAllRuleAsync(AccessRule.AccessToViewAccountingBank);
            var hasAccessToEditAccountingCash = userContext.HasAllRuleAsync(AccessRule.AccessToEditAccountingCash);
            var hasAccessToViewAccountingCash = userContext.HasAllRuleAsync(AccessRule.AccessToViewAccountingCash);
            await Task.WhenAll(hasAccessToEditAccountingBank, hasAccessToViewAccountingBank, hasAccessToEditAccountingCash, hasAccessToViewAccountingCash).ConfigureAwait(false);

            if (hasAccessToEditAccountingBank.Result)
            {
                access.AccessToBank = TypeAccessRule.AccessEdit;
            }
            else if (hasAccessToViewAccountingBank.Result)
            {
                access.AccessToBank = TypeAccessRule.AccessView;
            }
            else access.AccessToBank = TypeAccessRule.AccessDenied;

            if (hasAccessToEditAccountingCash.Result)
            {
                access.AccessToCash = TypeAccessRule.AccessEdit;
            }
            else if (hasAccessToViewAccountingCash.Result)
            {
                access.AccessToCash = TypeAccessRule.AccessView;
            }
            else access.AccessToCash = TypeAccessRule.AccessDenied;

            return access;
        }

        private async Task<object> GetTaxationSystemsAsync()
        {
            var list = await taxationSystemClient.GetAsync(userContext.FirmId, userContext.UserId).ConfigureAwait(false);
            return list.Select(ts => new
            {
                ts.StartYear,
                ts.EndYear,
                ts.IsUsn,
                ts.IsEnvd,
                ts.IsOsno,
                ts.UsnType,
                ts.UsnSize
            }).ToList();
        }
    }

    public enum TypeAccessRule
    {
        AccessDenied,
        AccessView,
        AccessEdit,
    }

    public class AccessMode
    {
        public TypeAccessRule AccessToBank { get; set; }
        public TypeAccessRule AccessToCash { get; set; }
    }
}
