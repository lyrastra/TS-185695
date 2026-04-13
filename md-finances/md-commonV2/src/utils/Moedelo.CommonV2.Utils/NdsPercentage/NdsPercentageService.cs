using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Utils.NdsPercentage
{
    [InjectAsSingleton]
    public class NdsPercentageService : INdsPercentageService
    {
        private const string key20percent = "Nds20PercentRateStartDate";
        private const string key22percent = "Nds22PercentRateStartDate";
        private const decimal _18Percent = 18;
        private const decimal _20Percent = 20;
        private const decimal _22Percent = 22;

        private readonly SettingValue nds20PercentRateStartDate;
        private readonly SettingValue nds22PercentRateStartDate;

        public NdsPercentageService(ISettingRepository settingRepository)
        {
            nds20PercentRateStartDate = settingRepository.Get(key20percent);
            nds22PercentRateStartDate = settingRepository.Get(key22percent);
        }

        public decimal GetCurrentRate()
        {
            return _22Percent;
        }

        public decimal GetRateForDate(DateTime date)
        {
            if (date.Date >= GetNds22PercentTransitionDate())
            {
                return _22Percent;
            }

            if (date.Date >= GetNds20PercentTransitionDate())
            {
                return _20Percent;
            }

            return _18Percent;
        }

        public Dictionary<DateTime, decimal> GetRateForDates(List<DateTime> dates)
        {
            var transitionDate = GetNds20PercentTransitionDate();

            return dates.Distinct().ToDictionary(x => x.Date, v => v.Date >= transitionDate ? _20Percent : _18Percent);
        }

        private DateTime GetNdsTransitionDateFromSetting(
            SettingValue settingValue)
        {
            var stringDate = settingValue.Value;
            if (string.IsNullOrWhiteSpace(stringDate))
            {
                throw new Exception($"Строковое представление даты по ключу {settingValue.Name} не найдено");
            }

            if (!DateTime.TryParseExact(stringDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var date))
            {
                throw new Exception($"Не удалось получить дату из строкового представления '{stringDate}'");
            }

            return date.Date;
        }

        private DateTime GetNds20PercentTransitionDate()
        {
            return GetNdsTransitionDateFromSetting(nds20PercentRateStartDate);
        }

        private DateTime GetNds22PercentTransitionDate()
        {
            return GetNdsTransitionDateFromSetting(nds22PercentRateStartDate);
        }
    }
}