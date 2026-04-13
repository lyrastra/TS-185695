#nullable enable
using Moedelo.Accounting.Domain.Shared.PaymentOrders.Outgoing.BudgetaryPayments;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentDescriptor))]
    class UnifiedBudgetaryPaymentDescriptor : IUnifiedBudgetaryPaymentDescriptor
    {
        private readonly IReadOnlyCollection<Kbk> kbksFromDB;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ITradingObjectApiClient tradingObjectApiClient;
        private readonly IPatentApiClient patentApiClient;
        
        //спека https://confluence.mdtest.org/pages/viewpage.action?pageId=87729001
        private readonly List<Description> descriptionRecords;
        private record Description(KbkType KbkType, string Text, string Kbk);
        
        public UnifiedBudgetaryPaymentDescriptor(
            IKbkReader bkkReader,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ITradingObjectApiClient tradingObjectApiClient,
            IPatentApiClient patentApiClient)
        {
            kbksFromDB = bkkReader.GetAllAsync().Result;
            descriptionRecords = GetDescriptionRecords();
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.tradingObjectApiClient = tradingObjectApiClient;
            this.patentApiClient = patentApiClient;
        }
        
        public async Task<string> GetDescription(IReadOnlyCollection<UnifiedBudgetarySubPaymentForDescription> subPayments, DateTime paymentDate)
        {
            string defaultDescription = paymentDate >= BudgetaryPaymentDates.FormatDate16052025
                ? "ЕНП. НДС не облагается"
                : "Единый налоговый платеж. НДС не облагается";
            if (subPayments.Count == 0)
                return defaultDescription;

            var result = paymentDate >= BudgetaryPaymentDates.FormatDate16052025
                ? "ЕНП. "
                : string.Empty;
            const string ending = "НДС не облагается";
            foreach (var subPayment in subPayments)
            {
                if (IsEmpty(subPayment.Period))
                    continue;

                var record = FindDescriptionRecord(subPayment);
                if (record == null)
                    continue;

                var newText = await GetPaymentText(subPayment, record);

                if ((result + newText + ending).Length > 210)
                    break;

                result = result + newText;
            }
            return result == string.Empty ? defaultDescription : result + ending;
        }

        public async Task<IReadOnlyCollection<UnifiedBudgetarySubPaymentForDescription>> GetSubPayments(string description)
        {
            var result = new List<UnifiedBudgetarySubPaymentForDescription>();
            const string separator = ";";
            var descriptions = description.Split(separator);
            foreach (var subPaymentText in descriptions)
            {
                var (descriptionText, periodText, sumText) = ParsingSubPaymentDescription(subPaymentText);
                var (kbkId, kbkNumber, tradingObjectNumber, patentNumber) = GetKbkInfo(descriptionText);
                if (kbkId == null)
                    continue;

                var period = GetPeriod(periodText);
                var sum = GetSum(sumText);

                result.Add( new UnifiedBudgetarySubPaymentForDescription 
                { 
                    KbkId = kbkId.Value, 
                    Period = period ?? new BudgetaryPeriod { Type = BudgetaryPeriodType.NoPeriod}, 
                    Sum = sum ?? 0,
                    PatentId = await GetPatentId(patentNumber),
                    TradingObjectId = await GetTradingObjectId(tradingObjectNumber),
                });
            }

            return result;
        }
        
        private static bool IsEmpty(BudgetaryPeriod? period)
        {
            if (period == null)
            {
                return true;
            }

            return period.Type == BudgetaryPeriodType.None || period.Year == 0 ||
                   (period.Type != BudgetaryPeriodType.Year && period.Number == 0);
        }

        private string FormationPeriodString(BudgetaryPeriod period)
        {
            var result = string.Empty;
            if (period.Type == BudgetaryPeriodType.NoPeriod)
                return result;
            return GetDesignation(period.Type) + "." + GetDesignation(period.Number) + "." + period.Year + "г." ;
        }

        private static string GetDesignation(BudgetaryPeriodType type)
        {
            return type switch
            {
                BudgetaryPeriodType.Year => "ГД",
                BudgetaryPeriodType.HalfYear => "ПЛ",
                BudgetaryPeriodType.Quarter => "КВ",
                BudgetaryPeriodType.Month => "МС",
                BudgetaryPeriodType.Date => "ДТ",
                _ => string.Empty,
            };
        }

        private string GetDesignation(int number)
        {
            return number < 10 ? "0" + number.ToString() : number.ToString();
        }

        private string GetDesignation(decimal sum)
        {
            return String.Format("{0:0.############}", sum) + "р.";
        }

        private (string, string, string) ParsingSubPaymentDescription(string description)
        {
            try
            {
                var sumText = string.Empty;
                var periodText = string.Empty;
                var descriptionText = description;

                Regex regexSum = new Regex(@"\s[0-9]+(\.|,)?[0-9]*р.");
                Regex regexPeriod = new Regex(@"\s(МС|КВ|ГД)\.[0-1][0-9]\.[0-9]{4}г.");

                MatchCollection matchesSum = regexSum.Matches(description);
                MatchCollection matchesPeriod = regexPeriod.Matches(description);

                if (matchesSum.Count == 1)
                    sumText = matchesSum[0].ToString();

                if (matchesPeriod.Count == 1)
                    periodText = matchesPeriod[0].ToString();

                if (!string.IsNullOrEmpty(sumText))
                    descriptionText = descriptionText.Replace(sumText, "");

                if (!string.IsNullOrEmpty(periodText))
                    descriptionText = descriptionText.Replace(periodText, "");

                return (descriptionText.Trim(), periodText.Trim(), sumText.Trim());
            }
            catch 
            {
                return (string.Empty, string.Empty, string.Empty);
            }
        }

        private const string EnpPrefix = "ЕНП.";

        private (int?, string?, string?, string?) GetKbkInfo(string description)
        {
            description = description.Trim();

            if (description.StartsWith(EnpPrefix, StringComparison.OrdinalIgnoreCase))
                description = description.Substring(EnpPrefix.Length).TrimStart();

            if (string.IsNullOrWhiteSpace(description))
                return (null, null, null, null);

            var tradingObjectNumber = TryGetTradingObjectNumber(description);
            if (!string.IsNullOrEmpty(tradingObjectNumber))
                description = description.Replace(tradingObjectNumber, "").Trim();

            var patentNumber = TryGetPatentNumber(description);
            if (!string.IsNullOrEmpty(patentNumber))
                description = description.Replace(patentNumber, "").Trim();

            var recordByDescription = descriptionRecords.FirstOrDefault(d => d.Text == description);
            if (recordByDescription == null)
                return (null, null, tradingObjectNumber, patentNumber);

            var result = kbksFromDB.FirstOrDefault(kbk => kbk.Number == recordByDescription.Kbk && kbk.KbkType == recordByDescription.KbkType);
            if (result == null)
                return (null, null, tradingObjectNumber, patentNumber);

            return (result.Id, result.Number, tradingObjectNumber, patentNumber);
        }

        private BudgetaryPeriod? GetPeriod(string periodText)
        {
            periodText = periodText.Trim();
            periodText = periodText.Replace("г.", "");
            if (string.IsNullOrWhiteSpace(periodText))
                return null;

            var periodPieces = periodText.Split('.');
            if (periodPieces.Length != 3)
                return null;
            var periodTypeText = periodPieces[0];

            BudgetaryPeriodType? periodType = periodTypeText switch
            {
                "МС" => BudgetaryPeriodType.Month,
                "КВ" => BudgetaryPeriodType.Quarter,
                "ГД" => BudgetaryPeriodType.Year,
                _ => null,
            };
            if (periodType == null)
                return null;
            int periodNumber;
            if (!int.TryParse(periodPieces[1], out periodNumber))
                return null;
            if (periodNumber != 0 && periodType == BudgetaryPeriodType.Year)
                return null;
            if (periodNumber == 0 && periodType != BudgetaryPeriodType.Year)
                return null;
            int periodYear;
            if (!int.TryParse(periodPieces[2], out periodYear))
                return null;
            if (periodYear < 2000)
                return null;

            return new BudgetaryPeriod { Type = periodType.Value, Number = periodNumber, Year = periodYear };
        }

        private decimal? GetSum(string sumText)
        {
            sumText = sumText.Trim();
            sumText = sumText.Replace("р.", "");
            sumText = sumText.Replace(".", ",");
            if (string.IsNullOrWhiteSpace(sumText))
                return null;
            decimal sum;
            if (!decimal.TryParse(sumText, out sum))
                return null;
            return sum;
        }

        private Description? FindDescriptionRecord(UnifiedBudgetarySubPaymentForDescription subPayment)
        {
            var kbkFromDb = kbksFromDB.FirstOrDefault(kbk => kbk.Id == subPayment.KbkId);
            
            if (kbkFromDb == null)
            {
                return null;
            }

            var recordByNumberAndType = descriptionRecords
                .FirstOrDefault(description => description.Kbk == kbkFromDb.Number && description.KbkType == kbkFromDb.KbkType);
            
            return recordByNumberAndType;
        }

        private async Task<string> GetPaymentText(UnifiedBudgetarySubPaymentForDescription subPayment, Description record)
        {
            var separator = " ";
            string newText;
            if (record.Text == "Торговый сбор")
            {
                var tradingObjectNumber = await GetTradingObjectNumber(subPayment.TradingObjectId);
                newText = record.Text + separator + tradingObjectNumber + separator + GetDesignation(subPayment.Sum) + separator + FormationPeriodString(subPayment.Period) + "; ";
            }
            else if (record.Text == "Патент")
            {
                var patentNumber = await GetPatentNumber(subPayment.PatentId);
                newText = record.Text + separator + patentNumber + separator + GetDesignation(subPayment.Sum) + separator + FormationPeriodString(subPayment.Period) + "; ";
            }
            else
                newText = record.Text + separator + GetDesignation(subPayment.Sum) + separator + FormationPeriodString(subPayment.Period) + "; ";
            return newText;
        }

        private async Task<string> GetTradingObjectNumber(int? tradingObjectId)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            try
            {
                var tradingObject = await tradingObjectApiClient.GetByIdAsync(
                    context.FirmId,
                    context.UserId,
                    tradingObjectId ?? throw new ArgumentNullException(nameof(tradingObjectId)));

                if (tradingObject == null || tradingObject.Id != tradingObjectId.Value)
                {
                    throw new Exception("Торговый объект не найден");
                }

                return tradingObject.Number.ToString();
            }
            catch (Exception)
            {
                throw new BusinessValidationException("Платёж по КБК 18210505010021000110 Торговый сбор", $"Не найден торговый объект с идентификатором {tradingObjectId}");
            }
        }

        private async Task<string> GetPatentNumber(long? patentId)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            try
            {
                var patent = await patentApiClient.GetWithoutAdditionalDataByIdAsync(
                    context.FirmId,
                    context.UserId,
                    patentId ?? throw new ArgumentNullException(nameof(patentId)));

                if (patent == null || patent.Id != patentId.Value)
                {
                    throw new Exception("Патент не найден");
                }
                return patent.Code;
            }
            catch (Exception)
            {
                throw new BusinessValidationException("Платёж за патент. КБК 18210504010021000110", $"Ошибка при получении данных патента с идентификатором {patentId}"); ;
            }
        }

        private async Task<int?> GetTradingObjectId(string? tradingObjectNumber)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;

            if (string.IsNullOrWhiteSpace(tradingObjectNumber)) 
                return null;

            try
            {
                var tradingObjects = await tradingObjectApiClient.GetShortAsync(context.FirmId, context.UserId);
                if (tradingObjects == null || tradingObjects.Length == 0)
                    return null;

                if(int.TryParse(tradingObjectNumber, out int intParseTradingObjectNumber))
                {
                    if (!(intParseTradingObjectNumber > 0))
                        return null;

                    var searchTradingObjects = tradingObjects.Where(to => to.Number == intParseTradingObjectNumber).ToList();
                    if (searchTradingObjects.Count == 0)
                        return null;

                    return searchTradingObjects.OrderByDescending(to => to.Id).First().Id;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<long?> GetPatentId(string? patentNumber)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            if (string.IsNullOrWhiteSpace(patentNumber))
                return null;

            try
            {
                var patents = await patentApiClient.GetWithoutAdditionalDataAsync(context.FirmId, context.UserId);
                if (patents == null || patents.Length == 0)
                    return null;

                var searchPatents = patents.Where(p => p.Code == patentNumber).ToList();
                if (searchPatents.Count == 0)
                    return null;

                return searchPatents.OrderByDescending(p => p.StartDate).First().Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string? TryGetTradingObjectNumber(string description)
        {
            const int expectedPositionsCount = 3;

            if (description.Contains("Торговый сбор"))
            {
                var strings = description.Split(" ");

                return strings.Length == expectedPositionsCount ? strings[expectedPositionsCount-1] : string.Empty;
            }
            return null;
        }

        private static string? TryGetPatentNumber(string description)
        {
            const int expectedPositionCount = 2;

            if (description.Contains("Патент"))
            {
                var strings = description.Split(" ");

                return strings.Length == expectedPositionCount ? strings[expectedPositionCount-1] : string.Empty;
            }

            return null;
        }

        private static List<Description> GetDescriptionRecords()
        {
            return new List<Description>
            {
                //Налог (по доходам ИП) КБК 18210102010011000110
                new Description(KbkType: KbkType.Ndfl, Text: "НДФЛ", Kbk: "18210102010011000110"),
                
                //НДФЛ (нал. агент/по доходам ИП, свыше 5 млн руб) КБК 18210102080011000110
                new Description(KbkType: KbkType.NdflOver5Millions, Text: "НДФЛ нал. агент, по доходам ИП, свыше 5 млн руб", Kbk: "18210102080011000110"),
                
                //НДФЛ (с див. до 5 млн. руб.) КБК 18210102130011000110
                new Description(KbkType: KbkType.NdflDividends, Text: "НДФЛ с див. до 5 млн. руб.", Kbk: "18210102130011000110"),
                
                //НДФЛ (с див. свыше. 5 млн. руб.) КБК 18210102140011000110
                new Description(KbkType: KbkType.NdflDividendsOver5Millions, Text: "НДФЛ с див. свыше. 5 млн. руб.", Kbk: "18210102140011000110"),
                
                //Взнос КБК 18210201000011000160
                new Description(KbkType: KbkType.InsuranceContribution, Text: "Взносы за сотрудников", Kbk: "18210201000011000160"),
                
                //Фикс. взносы (ИП) в пределах дохода КБК 18210202000011000160
                new Description(KbkType: KbkType.InsurancePaymentForIp, Text: "Фикс. взносы (ИП) в пределах дохода", Kbk: "18210202000011000160"),
                
                //Фикс. взносы ИП (1%) КБК 18210203000011000160
                new Description(KbkType: KbkType.InsurancePayOverdraft, Text: "Фикс. взносы ИП 1%", Kbk: "18210203000011000160"),
                new Description(KbkType: KbkType.InsurancePayOverdraft, Text: "Фикс.взносы ИП 1%", Kbk: "18210203000011000160"),
                
                //Налог (6 %) КБК 18210501011011000110
                new Description(KbkType: KbkType.DeclarationUsn6, Text: "УСН 6%", Kbk: "18210501011011000110"),
                
                //Налог (15 %) КБК 18210501021011000110
                new Description(KbkType: KbkType.DeclarationUsn15, Text: "УСН 15%", Kbk: "18210501021011000110"),
                
                //Налог (минимальный) КБК 18210501021011000110
                new Description(KbkType: KbkType.DeclarationUsnProfitOutgoMinTax, Text: "УСН. Минимальный налог", Kbk: "18210501021011000110"),
                
                //Налог (по доходам ИП) КБК 18210102020011000110
                new Description(KbkType: KbkType.NdflForIp, Text: "Налог НДФЛ по доходам ИП", Kbk: "18210102020011000110"),
                
                //Налог КБК 18210505010021000110
                new Description(KbkType: KbkType.SalesTax, Text: "Торговый сбор", Kbk: "18210505010021000110"),
                
                //Налог (реализация в РФ) КБК 18210301000011000110
                new Description(KbkType: KbkType.NdsTax, Text: "Налог НДС", Kbk: "18210301000011000110"),
                
                //Налог (в ФБ) КБК 18210101011011000110
                new Description(KbkType: KbkType.ProfitTaxFederal, Text: "Налог на прибыль в ФБ", Kbk: "18210101011011000110"),
                
                //Налог (в бюджет субъектов) КБК 18210101012021000110
                new Description(KbkType: KbkType.ProfitTaxLocal, Text: "Налог на прибыль в бюджет субъектов", Kbk: "18210101012021000110"),
                
                //Налог КБК 18210604011021000110
                new Description(KbkType: KbkType.TransportTax, Text: "Транспортный налог", Kbk: "18210604011021000110"),
                
                //Налог (Патент) КБК 18210504010021000110
                new Description(KbkType: KbkType.Patent, Text: "Патент", Kbk: "18210504010021000110"),
                
                //Налог КБК 18210602010021000110
                new Description(KbkType: KbkType.PropertyTax, Text: "Налог на имущество", Kbk: "18210602010021000110"),
                
                //Налог (Москва, СПб, Севастополь) КБК 18210606031031000110
                new Description(KbkType: KbkType.TaxOnLand03Moscow, Text: "Земельный налог", Kbk: "18210606031031000110"),
                
                //НДФЛ (нал. агент до 2,4 млн. руб.) КБК 18210102010011000110
                new Description(KbkType: KbkType.NdflBelow2_4Millions, Text: "НДФЛ нал. агент до 2,4 млн. руб.", Kbk: "18210102010011000110"),
                
                //НДФЛ (нал. агент с 2,4 до 5 млн. руб.) КБК 18210102080011000110
                new Description(KbkType: KbkType.NdfFrom2_4To5Millions, Text: "НДФЛ нал. агент с 2,4 до 5 млн. руб.", Kbk: "18210102080011000110"),
                
                //НДФЛ (нал. агент с 5 до 20 млн. руб.) КБК 18210102150011000110
                new Description(KbkType: KbkType.NdflFrom5To20Millions, Text: "НДФЛ нал. агент с 5 до 20 млн. руб.", Kbk: "18210102150011000110"),
                
                //НДФЛ (нал. агент с 20 до 50 млн. руб.) КБК 18210102160011000110
                new Description(KbkType: KbkType.NdflFrom20To50Millions, Text: "НДФЛ нал. агент с 20 до 50 млн. руб.", Kbk: "18210102160011000110"),
                
                //НДФЛ (нал. агент свыше 50 млн. руб.) КБК 18210102170011000110
                new Description(KbkType: KbkType.NdflOver50Millions, Text: "НДФЛ нал. агент свыше 50 млн. руб.", Kbk: "18210102170011000110"),
                
                //НДФЛ для РК и северной: НДФЛ (нал. агент до 5 млн. руб.) КБК 18210102210011000110
                new Description(KbkType: KbkType.NdflRKandNorthBelow5Millions, Text: "НДФЛ для РК и северной: НДФЛ нал. агент до 5 млн. руб.", Kbk: "18210102210011000110"),
                
                //НДФЛ для РК и северной: НДФЛ (нал. агент свыше 5 млн. руб.) КБК 18210102230011000110
                new Description(KbkType: KbkType.NdflRKandNorthOver5Millions, Text: "НДФЛ для РК и северной: НДФЛ нал. агент свыше 5 млн. руб.", Kbk: "18210102230011000110"),
                
                //НДФЛ (с див. до 2,4 млн. руб.) КБК 18210102130011000110
                new Description(KbkType: KbkType.NdflDividendsBelow2_4Millions, Text: "НДФЛ с див. до 2,4 млн. руб.", Kbk: "18210102130011000110"),
                
                //НДФЛ (с див. свыше 2,4 млн. руб) КБК 18210102140011000110
                new Description(KbkType: KbkType.NdflDividendsOver2_4Millions, Text: "НДФЛ с див. свыше 2,4 млн. руб.", Kbk: "18210102140011000110"),
                
                //НДФЛ (по доходам ИП до 2,4 млн. руб.) КБК 18210102020011000110
                new Description(KbkType: KbkType.NdflBelow2_4MillionsForIp, Text: "НДФЛ по доходам ИП до 2,4 млн. руб.", Kbk: "18210102020011000110"),
                
                //НДФЛ (по доходам ИП с 2,4 до 5 млн. руб.) КБК 18210102021011000110
                new Description(KbkType: KbkType.NdflFrom2_4to5MillionsForIp, Text: "НДФЛ по доходам ИП с 2,4 до 5 млн. руб.", Kbk: "18210102021011000110"),
                
                //НДФЛ (по доходам ИП с 5 до 20 млн. руб.) КБК 18210102022011000110
                new Description(KbkType: KbkType.NdflFrom5to20MillionsForIp, Text: "НДФЛ по доходам ИП с 5 до 20 млн. руб.", Kbk: "18210102022011000110"),
                
                //НДФЛ (по доходам ИП с 20 до 50 млн. руб.) КБК 18210102023011000110
                new Description(KbkType: KbkType.NdflFrom20to50MillionsForIp, Text: "НДФЛ по доходам ИП с 20 до 50 млн. руб.", Kbk: "18210102023011000110"),
                
                //НДФЛ (по доходам ИП свыше 50 млн. руб.) 18210102024011000110
                new Description(KbkType: KbkType.NdflOver50MillionsForIp, Text: "НДФЛ по доходам ИП свыше 50 млн. руб.", Kbk: "18210102024011000110"),
            };
        }
    }
}