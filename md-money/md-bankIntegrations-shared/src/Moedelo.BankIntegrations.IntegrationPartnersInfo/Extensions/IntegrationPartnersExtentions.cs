using Moedelo.BankIntegrations.IntegrationPartnersInfo.Attributes;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.IntegrationPartnersInfo.Extensions
{
    public static class IntegrationPartnersExtentions
    {
        /// <summary> Возвращает отображаемое имя интеграционного партнёра </summary>
        public static string PartnerName(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.PartnerName : string.Empty;
        }

        /// <summary> Возвращает имя партнёра в родительном падеже (для текстов типа «подключение Т‑Банка») </summary>
        public static string PartnerNameInGenitive(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.PartnerNameInGenitive : string.Empty;
        }

        /// <summary> Возвращает «нижнее» имя партнёра для поиска в БД (RegexpNameInDb) </summary>
        public static string PartnerLowerName(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.RegexpNameInDb : string.Empty;
        }

        /// <summary> Возвращает список регистрационных номеров банковской лицензии партнёра </summary>
        public static int[] BankLicenseNumbers(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.BankLicenseNumbers : [];
        }

        /// <summary> Возвращает текст о недоступности интеграции с партнёром (показывается пользователю) </summary>
        public static string InaccessibleText(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.InaccessibleText : string.Empty;
        }

        /// <summary> Возвращает имя JS‑функции, которая вызывается при включении интеграции с партнёром </summary>
        public static string PartnerJsFunctionForTurn(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.JsFunctionForTurn : string.Empty;
        }

        /// <summary> Можно ли в принципе подключить партнёра (учитывает как IsCanBeIntegrated, так и InDevelopment) </summary>
        public static bool IsCanBeIntegrated(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            var isCanBeIntegrated = integrationPartnerInfoAttribute != null && integrationPartnerInfoAttribute.IsCanBeIntegrated;
            var inDevelopment = integrationPartnerInfoAttribute != null && integrationPartnerInfoAttribute.InDevelopment;
            return isCanBeIntegrated || inDevelopment;
        }

        /// <summary> Поддерживает ли партнёр валютные счета </summary>
        public static bool HasNonRubAccountsSupport(this IntegrationPartners partner)
        {
            return partnersWhichSupportNonRubAccounts.Contains(partner);
        }
        
        /// <summary> Ищет банковского партнёра по названию банка в строке (устаревший способ поиска) </summary>
        [Obsolete("Метод будет удален. Необходимо искать партнера по регистрационному номеру банка (GetBankIntegrationPartnerByLicenseNumber)")]
        public static IntegrationPartners GetIntegrationPartnerByBankName(string bankName, List<IntegrationPartners> activeIntegrationPartners = null)
        {
            var nameForSearch = bankName.Replace("\"", string.Empty).ToLower();
            return (
                from enumValue in GetIntegrationPartners()
                let integrationAttribute = enumValue.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>()
                where !string.IsNullOrEmpty(integrationAttribute?.RegexpNameInDb) && nameForSearch.Contains(integrationAttribute.RegexpNameInDb.Replace("\"", string.Empty).ToLower())
                orderby
                    integrationAttribute.IsCanBeIntegrated descending,
                    activeIntegrationPartners?.Contains(enumValue) descending,
                    (int)enumValue ascending
                select enumValue
            ).FirstOrDefault();
        }
        
        /// <summary> Ищет интеграционного партнёра, который не является банком, по названию в строке </summary>
        public static IntegrationPartners GetNotBankPartnerByName(string bankName, List<IntegrationPartners> activeIntegrationPartners = null)
        {
            var nameForSearch = bankName.Replace("\"", string.Empty).ToLower();
            return (
                from enumValue in GetIntegrationPartners()
                let integrationAttribute = enumValue.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>()
                where integrationAttribute != null 
                      && !integrationAttribute.IsBank 
                      && !string.IsNullOrEmpty(integrationAttribute?.RegexpNameInDb) 
                      && nameForSearch.Contains(integrationAttribute.RegexpNameInDb.Replace("\"", string.Empty).ToLower())
                orderby
                    integrationAttribute.IsCanBeIntegrated descending,
                    activeIntegrationPartners?.Contains(enumValue) descending,
                    (int)enumValue ascending
                select enumValue
            ).FirstOrDefault();
        }

        /// <summary> Определяет, является ли партнёр банком </summary>
        public static bool IsBank(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null ? integrationPartnerInfoAttribute.IsBank : true;
        }

        /// <summary> Является ли партнёр интеграционным монитором (используется только для мониторинга интеграций) </summary>
        public static bool IsIntegrationMonitor(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            var IsIntegrationMonitor = integrationPartnerInfoAttribute != null && integrationPartnerInfoAttribute.IsIntegrationMonitor;
            return IsIntegrationMonitor;
        }

        /// <summary> Возвращает полный список интеграционных партнёров из перечисления IntegrationPartners </summary>
        public static IntegrationPartners[] GetIntegrationPartners()
        {
            return (IntegrationPartners[])Enum.GetValues(typeof(IntegrationPartners));
        }

        /// <summary> Возвращает атрибут указанного типа для значения перечисления </summary>
        private static TA GetCustomAttribute<TE, TA>(this TE enumVal) where TE : struct where TA : Attribute
        {
            var list = typeof(TE).GetMember(enumVal.ToString())[0].GetCustomAttributes(typeof(TA), false).Cast<TA>().ToList();
            return list.Count <= 0 ? default(TA) : list[0];
        }

        /// <summary> Использует ли партнёр новый UI подключения платёжной системы </summary>
        public static bool UsePaymentSystemIntegrationUi(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute != null && integrationPartnerInfoAttribute.UsePaymentSystemIntegrationUI;
        }

        /// <summary> Возвращает тип интеграции партнёра (банк, платёжный сервис и т.п.) </summary>
        public static TypeIntegration GetTypeIntegration(this IntegrationPartners partner)
        {
            var integrationPartnerInfoAttribute = partner.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>();
            return integrationPartnerInfoAttribute?.TypePartner ?? TypeIntegration.IsUsedMD;
        }

        /// <summary> Возвращает список партнёров, использующих технологию одноразовых паролей (OTP) </summary>
        public static List<IntegrationPartners> GetOtpPartners()
        {
            return (from partners in GetIntegrationPartners()
                    let attr = partners.GetCustomAttribute<IntegrationPartners, IntegrationPartnerInfoAttribute>()
                    where attr?.IsOtp ?? false
                    select partners).ToList();
        }

        /// <summary>
        /// Преобразует коллекцию IntegrationPartners из этой сборки, в коллецию переданного типа.
        /// Метод нужен, чтобы использовать новое расширение со старыми перечислениями
        /// Пример испрользования: IntegrationPartnersExtentions.GetBankPushPartners().GetEnums<IntegrationPartners>();
        /// </summary>
        public static List<T> GetEnums<T>(this IReadOnlyCollection<IntegrationPartners> partners) where T : struct
        {
            return partners.Select(x => (T)(object)x).ToList();
        }

        /// <summary>
        /// Прокси-метод, для вызова методов расширения со старыми перечислениями
        /// Пример использования: IntegrationPartners.SberBank.GetInfo(IntegrationPartnersExtentions.PartnerLowerName)
        /// /// </summary>   
        public static TR GetInfo<T, TR>(this T partner, Func<IntegrationPartners, TR> func) where T : struct
        {
            return func.Invoke((IntegrationPartners)(object)partner);
        }

        /// <summary> Использует ли партнер технологию One Time Password (одноразовые пароли) </summary>
        public static bool UseOtp(this IntegrationPartners partner)
        {
            return GetOtpPartners().Any(x => x == partner);
        }

        /// <summary> Возвращает только банки </summary>
        public static List<IntegrationPartners> GetBankPartners()
        {
            return GetIntegrationPartners()
                .Where(x => x.GetAttribute<IntegrationPartnerInfoAttribute>()?.IsBank == true).ToList();
        }
        
        /// <summary> Возвращает банки, с асинхронным забором выписки </summary>
        public static IReadOnlyCollection<IntegrationPartners> GetBankQueuePartners()
        {
            return BankQueuePartners;
        }

        /// <summary> Возвращает банки‑партнёры, по которым работает схема получения выписок «push» </summary>
        public static IReadOnlyCollection<IntegrationPartners> GetBankPushPartners()
        {
            return new List<IntegrationPartners> 
            {
                IntegrationPartners.NbdBank,
                IntegrationPartners.RaiffeisenBank,
                IntegrationPartners.Skb,
                IntegrationPartners.ModulBank,
                IntegrationPartners.UralsibBankSso,
                IntegrationPartners.Vneshtorg,
            };
        }

        /// <summary> Доступна ли выписка за сегодняшний день для указанного банка‑партнёра </summary>
        public static bool IsTodayStatementAvailable(IntegrationPartners partner)
        {
            return BanksThatAvailableTodayStatements.Contains(partner);
        }
        
        /// <summary> Возвращает банков-партнеров, которым возможно отправить список документов </summary>
        public static List<IntegrationPartners> GetSendAllPaymentPartners()
        {
            return new List<IntegrationPartners>
            {
                IntegrationPartners.Skb,
                IntegrationPartners.UralsibBankSso,
                IntegrationPartners.NbdBank,
                IntegrationPartners.RaiffeisenBank,
                IntegrationPartners.SovComBank,
                IntegrationPartners.AkbarsBankSso,
                IntegrationPartners.AvangardSso,
            };
        }
        
        /// <summary> Возвращает банков-партнеров, по которые перешли на новую схему отправки платежей </summary>
        public static IReadOnlyCollection<IntegrationPartners> GetPartnersWithNewPaymentScheme()
        {
            return
            [
                IntegrationPartners.SdmBankSso,
                IntegrationPartners.RaiffeisenBank,
                IntegrationPartners.AvangardSso,
                IntegrationPartners.MobileTelesystemsBank,
                IntegrationPartners.PointBank,
                IntegrationPartners.NbdBank,
                IntegrationPartners.SovComBank,
                IntegrationPartners.Alfa,
                IntegrationPartners.AkbarsBankSso,
                IntegrationPartners.TinkoffBankSso,
                IntegrationPartners.Rshb,
                IntegrationPartners.OtpBankSso,
                IntegrationPartners.SovComBankWl,
                IntegrationPartners.Skb,
                IntegrationPartners.UralsibBankSso,
                IntegrationPartners.BlancBank,
                IntegrationPartners.SberBank,
                IntegrationPartners.Ingo,
                IntegrationPartners.Vneshtorg,
                IntegrationPartners.WbBankWl,
            ];
        }
        
        /// <summary> Список банков-партнеров, которые поддерживают отправку платежей на валютные счета</summary>
        private static readonly List<IntegrationPartners> partnersWhichSupportNonRubAccounts = new List<IntegrationPartners>
        {
            IntegrationPartners.SberBank,
            IntegrationPartners.RaiffeisenBank,
            IntegrationPartners.TinkoffBankSso,
            IntegrationPartners.ModulBank,
            IntegrationPartners.AvangardSso,
            IntegrationPartners.PointBank,
            IntegrationPartners.BlancBank,
            IntegrationPartners.Alfa,
            IntegrationPartners.UralsibBankSso,
            IntegrationPartners.Rshb,
        };
        
        /// <summary>
        /// Key - партнер
        /// Value - это флаг в таблице dbo.FirmFlag, который нужно положить через dataFix для тестовой фирмы
        /// </summary>
        public static readonly Dictionary<IntegrationPartners, string> DevelopedPartners =
            new Dictionary<IntegrationPartners, string>()
            {
                { IntegrationPartners.WbBankWl, "CanBeIntegratedWithWbBank" },
                { IntegrationPartners.Vneshtorg, "CanBeIntegratedWithVneshtorg" },
            };

        public static List<IntegrationPartners> GetPartnersWhoCanUpdateIntegrationData()
        {
            // список банков, для которых полем IntegrationData управляет приложение BankIntegrations, для остальныех этим полем управляет Adapter конкретного банка
            return partnersWhoCanUpdateIntegrationData;
        }

        /// <summary> Возвращает партнёров, для которых нельзя вручную отключить интеграцию через реквизиты </summary>
        public static List<IntegrationPartners> GetPartnersWhoCantTurnOffIntegrationManualy()
        {
            return partnersWhoCantTurnOffIntegrationManualy;
        }

        /// <summary> Возвращает партнёров, по которым сохраняются ошибки в таблицу IntegrationError </summary>
        public static List<IntegrationPartners> GetPartnersAllowedForSavingErrors()
        {
            return partnersAllowedForSavingErrors;
        }

        /// <summary> Возвращает партнёров, которые отображаются без указания расчётного счёта при подключении интеграции </summary>
        public static List<IntegrationPartners> GetPartnersWhoShowWithOutAccount()
        {
            return partnersWhoShowWithOutAccount;
        }
        
        public static List<IntegrationPartners> GetPartnersWhoForMobile()
        {
            return PartnersWhoForMobile;
        }
        
        /// <summary> Поддерживает ли партнёр отправку инвойсов (сквозных платежей) </summary>
        public static bool CanSendInvoice(this IntegrationPartners partner)
        {
            return PartnersCanSendInvoice.Contains(partner);
        }
        
        /// <summary> Поддерживает ли партнёр прямой (сквозной) платёж из мастеров </summary>
        public static bool CanDirectPayment(this IntegrationPartners partner)
        {
            return PartnersDirectPayment.Contains(partner);
        }
                
        /// <summary> Список банков, которые поддерживают сквозной (прямой) платеж из мастеров </summary>
        public static readonly List<IntegrationPartners> PartnersDirectPayment = new List<IntegrationPartners>
        {
            IntegrationPartners.SberBank
        };

        public static List<IntegrationPartners> GetWhoCanDisableIntegrations() 
        {
            return whoCanDisableIntegrations;
        }
        
        /// <summary> Возвращает, список банков-партнеров, по которым работает интеграция </summary>
        public static List<IntegrationPartners> GetWorkedPartners()
        {
            return new List<IntegrationPartners>
            {
                IntegrationPartners.PsBank,
                IntegrationPartners.ModulBank,
                IntegrationPartners.PointBank,
                IntegrationPartners.BlancBank,
                IntegrationPartners.TinkoffBankSso,
                IntegrationPartners.OtpBankSso,
                IntegrationPartners.SberBank,
                IntegrationPartners.NbdBank,
                IntegrationPartners.RaiffeisenBank,
                IntegrationPartners.SovComBank,
                IntegrationPartners.AvangardSso,
                IntegrationPartners.SdmBankSso,
                IntegrationPartners.ChelindBank,
                IntegrationPartners.MobileTelesystemsBank,
                IntegrationPartners.UralsibBankSso,
                IntegrationPartners.Alfa,
                IntegrationPartners.Rshb,
                IntegrationPartners.Ingo,
                //IntegrationPartners.Vneshtorg,
                //IntegrationPartners.WbBank,
            };
        }

        public static List<IntegrationPartners> GetPartnersWithoutSendPayment()
        {
            return new List<IntegrationPartners>
            {
                IntegrationPartners.Robokassa,
                IntegrationPartners.Evotor
            };
        }
        
        public static IntegrationPartners GetBankIntegrationPartnerByLicenseNumber(
            int? licenseNumber, 
            List<IntegrationPartners> activeIntegrationPartners = null)
        {
            // проверка номера лицензии
            if (!licenseNumber.HasValue || licenseNumber.Value == 0)
                return IntegrationPartners.Undefined;

            // получаем только банки, доступные к интеграции и с валидным списком лицензий
            var partners = GetIntegrationPartners()
                .Where(p => 
                    p.IsBank() &&
                    p.IsCanBeIntegrated() &&
                    p.BankLicenseNumbers() != null &&
                    p.BankLicenseNumbers().Contains(licenseNumber.Value)
                );

            // сортируем: сначала те, что в списке активных, затем по имени/идентификатору (или вашему критерию)
            var sortedPartners = partners
                .OrderByDescending(partner => activeIntegrationPartners?.Contains(partner))
                .ThenBy(partner => partner);

            // возвращаем самого релевантного или null (если ни один не подошёл)
            return sortedPartners.FirstOrDefault();
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
        
        /// <summary> Список банков-партнеров, по которым возможно запросить выписку по схему очередь </summary>
        private static readonly IntegrationPartners[] BankQueuePartners = {
            IntegrationPartners.AlfaBankSso,
            IntegrationPartners.AvangardSso, 
            IntegrationPartners.PointBank,
            IntegrationPartners.BlancBank, 
            IntegrationPartners.Rshb, 
            IntegrationPartners.SovComBankWl,
            IntegrationPartners.WbBankWl
        };
        
        /// <summary> Список банков-партнеров, по которые возможно запросить выписку за текущий день </summary>
        private static readonly HashSet<IntegrationPartners> BanksThatAvailableTodayStatements = new HashSet<IntegrationPartners>()
        {
            IntegrationPartners.PsBank,
            IntegrationPartners.SberBank,
            IntegrationPartners.PointBank,
            IntegrationPartners.ModulBank,
            IntegrationPartners.Alfa,
            IntegrationPartners.TinkoffBankSso
        };
        
        /// <summary> Список банков-партнеров, которые доступны для мобильного приложения </summary>
        private static readonly List<IntegrationPartners> PartnersWhoForMobile = new List<IntegrationPartners>
        {
            IntegrationPartners.SberBank,
            IntegrationPartners.Alfa,
            IntegrationPartners.TinkoffBankSso,
            IntegrationPartners.PointBank
        };
         
        /// <summary> Список банков-партнеров, которые поддерживают сквозной (прямой) платеж </summary>
        private static readonly List<IntegrationPartners> PartnersCanSendInvoice = new List<IntegrationPartners>
        {
            IntegrationPartners.SberBank,
            IntegrationPartners.Alfa,
            IntegrationPartners.TinkoffBankSso,
            IntegrationPartners.PointBank
        };

        /// <summary> Список банков-партнеров, по которым нет возможности отключать интеграцию в ручную через реквизиты </summary>
        private static readonly List<IntegrationPartners> partnersWhoCantTurnOffIntegrationManualy = new List<IntegrationPartners>
        {
            IntegrationPartners.AkbarsBankSso,
            IntegrationPartners.Skb,
            IntegrationPartners.AlfaBankSso,
            IntegrationPartners.SovComBankWl
        };

        /// <summary>
        /// Список банков-партнеров, для которых полем IntegrationData управляет приложение BankIntegrations, для остальныех этим полем управляет Adapter конкретного банка.
        /// Для новых реализаций добавлять в этот список партнера не нужно
        /// </summary>
        private static readonly List<IntegrationPartners> partnersWhoCanUpdateIntegrationData = new List<IntegrationPartners>
        {
                IntegrationPartners.YandexMoney,
                IntegrationPartners.YandexMarket,
                IntegrationPartners.Robokassa,
                IntegrationPartners.PsBank,
                IntegrationPartners.ModulBank,
                IntegrationPartners.Evotor,
                IntegrationPartners.YandexKassa,
                IntegrationPartners.Subtotal,
                IntegrationPartners.LifePay,
                IntegrationPartners.B2B,
                IntegrationPartners.InSales,
                IntegrationPartners.MyCashier,
                IntegrationPartners.Bitrix24B2B,
                IntegrationPartners.Appecs,
                IntegrationPartners.Envybox,
                IntegrationPartners.ModulKassa,
                IntegrationPartners.Ekam,
                IntegrationPartners.NbdBank,
                IntegrationPartners.Bitrix24,
                IntegrationPartners.Kassatka,
                IntegrationPartners.Entera,
                IntegrationPartners.RaiffeisenBank,
                IntegrationPartners.SovComBank,
                IntegrationPartners.SalesapCRM,
                IntegrationPartners.ChelindBank,
                IntegrationPartners.SdmBankSso,
                IntegrationPartners.AvangardSso,
                IntegrationPartners.MobileTelesystemsBank,
                IntegrationPartners.Vneshtorg,
        };

        /// <summary> Список банков-партнеров, по которым сохраняем ошибки в mssql таблицу IntegrationError ошибки </summary>
        private static readonly List<IntegrationPartners> partnersAllowedForSavingErrors = new List<IntegrationPartners>
        {
            IntegrationPartners.SberBank,
            IntegrationPartners.TinkoffBankSso,
            IntegrationPartners.ModulBank,
            IntegrationPartners.Alfa,
            IntegrationPartners.PointBank,
            IntegrationPartners.MobileTelesystemsBank,
            IntegrationPartners.BlancBank,
            IntegrationPartners.UralsibBankSso,
            IntegrationPartners.Rshb,
            IntegrationPartners.OtpBankSso,
            IntegrationPartners.SovComBankWl,
            IntegrationPartners.Ingo,
            IntegrationPartners.Vneshtorg,
            IntegrationPartners.WbBankWl,
        };

        /// <summary> Список банков-партнеров, по которым не требуется счет для подключения интеграции </summary>
        private static readonly List<IntegrationPartners> partnersWhoShowWithOutAccount = new List<IntegrationPartners>()
        {
            IntegrationPartners.Alfa,
            IntegrationPartners.TinkoffBankSso,
            IntegrationPartners.PointBank,
            IntegrationPartners.SberBank
        };

        /// <summary>
        /// При запросах выписок или отправке пп проставляется флаг IsNeedDisableIntegration, 
        /// чтобы интеграция отключилась нужно добавить партнера еще и в whoCanDisableIntegrations
        /// </summary>
        private static readonly List<IntegrationPartners> whoCanDisableIntegrations = new List<IntegrationPartners>
        {
            IntegrationPartners.Alfa,
            IntegrationPartners.SberBank,
            IntegrationPartners.TinkoffBankSso,
            IntegrationPartners.PointBank,
            IntegrationPartners.BlancBank,
            IntegrationPartners.MobileTelesystemsBank,
            IntegrationPartners.Rshb,
            IntegrationPartners.SdmBankSso,
            IntegrationPartners.OtpBankSso,
            IntegrationPartners.SovComBankWl,
            IntegrationPartners.Ingo,
            IntegrationPartners.WbBankWl,
            IntegrationPartners.Vneshtorg,
        };
    }
}
