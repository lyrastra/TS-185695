using System.Collections.Generic;

namespace Moedelo.Common.Enums.Enums.Billing
{
    /// <summary> Распределение тарифов по партнерам </summary>
    public static class TariffByPartnerExtension
    {
        /// <summary> Старые тарифы Сбербанк </summary>
        public static List<Tariff> OldSberbankTariffs => new List<Tariff>
        {
            Tariff.SberbankWithOutWorkers,
            Tariff.SberbankWithWorkers,
            Tariff.SberbankMax,
            Tariff.SberbankZero
        };

        /// <summary> Старый тариф Сбербанк </summary>
        public static bool IsOldSberbankTariff(this Tariff tariff)
        {
            return OldSberbankTariffs.Contains(tariff);
        }

        /// <summary> Новые тарифы Сбербанк </summary>
        public static List<Tariff> NewSberbankTariffs => new List<Tariff>
        {
            Tariff.SberbankTrial,
            Tariff.SberbankBasic,
            Tariff.SberbankPremium
        };

        /// <summary> Новый тариф Сбербанк </summary>
        public static bool IsNewSberbankTariff(this Tariff tariff)
        {
            return NewSberbankTariffs.Contains(tariff);
        }

        /// <summary> Тарифы Сбербанк v1.1 </summary>
        public static List<Tariff> SberbankTariffsV11 => new List<Tariff>
        {
            Tariff.SberbankWithoutWorkersV11,
            Tariff.SberbankWithWorkersV11,
            Tariff.SberbankMaxV11
        };

        /// <summary> Тариф Сбербанк v1.1 </summary>
        public static bool IsSberbankTariffsV11(this Tariff tariff)
        {
            return SberbankTariffsV11.Contains(tariff);
        }

        public static bool IsSberbankAccTariff(this Tariff tariff)
        {
            var sberbankAccTariff = new List<Tariff>
            {
                Tariff.SberbankAccountantWithOutWorkers,
                Tariff.SberbankAccountantWithWorkers,
                Tariff.SberbankAccountantMax,
                Tariff.SberbankSolutionForBusinessAccWithWorkers
            };

            return sberbankAccTariff.Contains(tariff);
        }

        /// <summary> Тарифы MailRu БИЗ </summary>
        public static List<Tariff> MailRuBizTariffs => new List<Tariff>
        {
            Tariff.MailRuWithoutWorkers,
            Tariff.MailRuUpToFive,
            Tariff.MailRuMax
        };

        /// <summary> Тариф MailRu БИЗ </summary>
        public static bool IsMailRuBizTariff(this Tariff tariff)
        {
            return MailRuBizTariffs.Contains(tariff);
        }

        /// <summary> Тарифы MailRu Бюро </summary>
        public static List<Tariff> MailSpsTariffs => new List<Tariff>
        {
            Tariff.OfficeMailRuStandart,
            Tariff.OfficeMailRuPro
        };

        /// <summary> Тариф MailRu Бюро </summary>
        public static bool IsMailRuSpsTariff(this Tariff tariff)
        {
            return MailSpsTariffs.Contains(tariff);
        }

        /// <summary> Тарифы Убер </summary>
        public static List<Tariff> UberTariffs => new List<Tariff>
        {
            Tariff.UberWithoutWorkers
        };

        /// <summary> Тариф Убер </summary>
        public static bool IsUberTariff(this Tariff tariff)
        {
            return UberTariffs.Contains(tariff);
        }

        /// <summary> Тарифы СКБ-Банка </summary>
        public static List<Tariff> SkbbankTariffs => new List<Tariff>
        {
            Tariff.SkbbankProfessionalUsnWithoutWorkers,
            Tariff.SkbbankProfessionalUsnUpToFive,
            Tariff.SkbbankProfessionalUsnMax
        };

        /// <summary> Тариф СКБ-Банка </summary>
        public static bool IsSkbbankTariff(this Tariff tariff)
        {
            return SkbbankTariffs.Contains(tariff);
        }

        /// <summary> Тарифы ОСНО СКБ-Банка </summary>
        public static List<Tariff> SkbbankOsnoTariffs => new List<Tariff>
        {
            Tariff.SkbbankProfessionalOsno
        };

        /// <summary> Тариф ОСНО СКБ-Банка </summary>
        public static bool IsSkbbankOsnoTariff(this Tariff tariff)
        {
            return SkbbankOsnoTariffs.Contains(tariff);
        }
    }
}