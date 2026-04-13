using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Common.Enums.Enums.Billing
{
    public static class TariffExtension
    {
        public static Tariff BlockedTariff => Tariff.Block;

        public static Tariff OldWithoutWorkersTariff
        {
            get { return Tariff.WithoutWorkers; }
        }

        private static Tariff[] freeTariffs =
        {
            Tariff.IpFree,
            Tariff.OooFree
        };

        private static readonly IReadOnlyCollection<Tariff> tariffsWithTestData = new[]
        {
            Tariff.AccountantChamber,
            Tariff.SalaryAndPersonal
        };

        public static bool IsRegistrationOooWizardTariff(this Tariff tariff)
        {
            return Tariff.OooRegistration == tariff;
        }

        public static bool IsRegistrationIpWizardTariff(this Tariff tariff)
        {
            return Tariff.IpRegistration == tariff;
        }

        public static bool IsFreeTariff(this Tariff tariff)
        {
            return freeTariffs.Contains(tariff);
        }

        public static bool IsRegistrationWizardTariff(this Tariff tariff)
        {
            return Tariff.IpRegistration == tariff
                   || Tariff.OooRegistration == tariff;
        }

        private static readonly HashSet<Tariff> oldWithoutWorkersTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.OooWithoutWorkers,
            Tariff.IpWithoutWorkers,
            Tariff.WithoutWorkers,
            Tariff.UsnZero,
            Tariff.Kiddy,
            Tariff.SberbankZero,
            Tariff.MailRuWithoutWorkers,
            Tariff.NewBusiness,
            Tariff.UsnWithoutWorkers,
            Tariff.OpenningUsnWithoutWorkers,
            Tariff.SberbankWithOutWorkers,
            Tariff.UsnWithoutWorkersPlus,
            Tariff.MailRuWithoutWorkers,
            Tariff.ProfessionalUsnWithoutWorkers,
            Tariff.UberWithoutWorkers,
            Tariff.ProfessionalUsnWithoutWorkersPlus,
            Tariff.SkbbankProfessionalUsnWithoutWorkers,
            Tariff.SberbankWithoutWorkersV11,
            Tariff.RnkbUsnEnvdWithoutWorkers,
            Tariff.SberbankWithoutWorkersV13
        });

        public static bool IsWithoutWorkers(this Tariff tariff)
        {
            return oldWithoutWorkersTariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldProTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.AccountantChamber,
            Tariff.UsnAccountant,
            Tariff.SalaryAndPersonal,
            Tariff.AccountantChamberSmallBusiness,
            Tariff.SalaryAndPersonalSmallBusiness,
            Tariff.ProfessionalUsn,
            Tariff.ProfessionalOsno,
            Tariff.SpsStandartOsno,
            Tariff.SpsProSingleUserOsno,
            Tariff.Office,
            Tariff.OfficePro,
            Tariff.OfficePartner,
            Tariff.OfficeLite,
            Tariff.OfficeStart,
            Tariff.OfficeLegist,
            Tariff.OfficeMailRuStandart,
            Tariff.OfficeMailRuPro,
            Tariff.OfficeCheckContragent,
            Tariff.OfficeCheckContragentPlus,
            Tariff.OfficeProPlus,
            Tariff.CaseLookStandart,
            Tariff.CaseLookPro,
            Tariff.CaseLookProPlus,
            Tariff.OfficeFreemium,
            Tariff.OfficeStandartCheckPerson,
            Tariff.OfficeProCheckPerson,
            Tariff.OfficeCheckKontragentAndCheckPerson,
            Tariff.SkbbankProfessionalOsno,
            Tariff.CompoundSpsTariff
        });

        public static bool IsProTariff(this Tariff tariff)
        {
            return oldProTariffs.Contains(tariff) || tariff.IsAccountantConsultant();
        }

        private static readonly HashSet<Tariff> oldOutsourcingTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.OutsourcingStart,
            Tariff.OutsourcingStartPlus,
            Tariff.OutsourcingMicroBusiness,
            Tariff.OutsourcingMicroBusinessPlus,
            Tariff.OutsourcingSmallBusiness,
            Tariff.OutsourcingSmallBusinessPlus,
            Tariff.OutsourcingIndividual,
            Tariff.StartOsno,
            Tariff.StartOsnoPlus,
            Tariff.MicroBusinessOsno,
            Tariff.MicroBusinessOsnoPlus,
            Tariff.SmallBusinessOsno,
            Tariff.SmallBusinessOsnoPlus,
            Tariff.StartIp6,
            Tariff.StartIp15,
            Tariff.StartPlus,
            Tariff.MicroBusiness,
            Tariff.MicroBusinessPlus,
            Tariff.SmallBusiness,
            Tariff.SmallBusinessPlus,
            Tariff.Finguru,
            Tariff.Knopka,
            Tariff.OutsourceRegistration
        });

        [Obsolete("Определяй это по значениям dbo.Tariff::ProductGroup и dbo.Tariff::ProductPlatform")]
        public static bool IsOutsourcingTariff(this Tariff tariff)
        {
            return oldOutsourcingTariffs.Contains(tariff);
        }

        public static bool IsNotTariff(this Tariff tariff)
        {
            return Tariff.Any == tariff;
        }

        public static bool IsBlockedTariff(this Tariff tariff)
        {
            return Tariff.Block == tariff;
        }

        private static readonly HashSet<Tariff> oldAccountantConsultantTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.AccountantConsultatnt,
            Tariff.AccountantConsultantSmallBusiness,
            Tariff.DigitalSign,
            Tariff.AccountantConsultant5Users,
            Tariff.AccountantConsultant10Users,
            Tariff.AccountantConsultant50Users,
            Tariff.BuhSmallBusiness,
            Tariff.BuhStandart,
            Tariff.BuhPro,
            Tariff.SpsSmallBusiness,
            Tariff.SpsStandart,
            Tariff.SpsStandartNew,
            Tariff.SpsProSingleUser,
            Tariff.SpsPro5Users,
            Tariff.SpsPro10Users,
            Tariff.SpsPro50Users,
            Tariff.SpsProSingleUserOsno,
            Tariff.SpsStandartOsno,
            Tariff.SpsStandartPlus,
            Tariff.NewYear
        });

        public static bool IsAccountantConsultant(this Tariff tariff)
        {
            return oldAccountantConsultantTariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldAccountingSystemTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.ProfessionalUsn,
            Tariff.ProfessionalOsno,
            Tariff.SpsStandartOsno,
            Tariff.SpsProSingleUserOsno,
            Tariff.ProfessionalUsnWithoutWorkers,
            Tariff.ProfessionalUsnUpToFive,
            Tariff.ProfessionalUsnMax,
            Tariff.ProfessionalUsnWithoutWorkersPlus,
            Tariff.ProfessionalUsnUpToFivePlus,
            Tariff.ProfessionalUsnMaxPlus,
            Tariff.SkbbankProfessionalOsno
        });

        public static bool IsUsnAccountant(this Tariff tariff)
        {
            return tariff == Tariff.UsnAccountant
                   || oldAccountingSystemTariffs.Contains(tariff);
        }

        [Obsolete("Места, где используется эта функция, потенциально неверно работают")]
        public static Tariff GetOldDefaultTariff(bool isOoo, bool isEmployerMode)
        {
            return isEmployerMode
                ? Tariff.WithWorkers
                : isOoo
                    ? Tariff.OooWithoutWorkers
                    : Tariff.IpWithoutWorkers;
        }

        public static Tariff GetTariffForRegistrationWizard(bool isOoo)
        {
            return isOoo ? Tariff.OooRegistration : Tariff.IpRegistration;
        }

        public static List<Tariff> SberbankTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankWithOutWorkers,
                    Tariff.SberbankWithWorkers,
                    Tariff.SberbankMax,
                    Tariff.SberbankOutsourcing,
                    Tariff.SberbankZero,
                    Tariff.SberbankTrial,
                    Tariff.SberbankBasic,
                    Tariff.SberbankPremium,
                    Tariff.SberbankWithoutWorkersV11,
                    Tariff.SberbankWithWorkersV11,
                    Tariff.SberbankMaxV11,
                    Tariff.SberbankAccountantWithOutWorkers,
                    Tariff.SberbankAccountantWithWorkers,
                    Tariff.SberbankAccountantMax,
                    Tariff.SberbankSolutionForBusinessAccWithWorkers,
                    Tariff.SberbankIp6WithoutWorkers,
                    Tariff.SberbankAccountantIp6WithoutWorkers,
                    Tariff.SberbankAccountantOsnoEnvdV12,
                    Tariff.SberbankAccountantMaxOsnoV13,
                    Tariff.SberbankAccountantWithoutWorkersV13,
                    Tariff.SberbankAccountantWithWorkersV13,
                    Tariff.SberbankAccountantMaxV13,
                    Tariff.SberbankWithoutWorkersV13,
                    Tariff.SberbankWithWorkersV13,
                    Tariff.SberbankMaxV13,
                    Tariff.SberbankAccountantMaxV14,
                    Tariff.SberbankAccountantMaxOsnoV14,
                    Tariff.SberbankAccountantWithWorkersV14,
                    Tariff.SberbankAccountantWithoutWorkersV14,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlaceV14,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlaceV14,
                    Tariff.SberbankAccountantMaxV15,
                    Tariff.SberbankAccountantMaxOsnoV15,
                    Tariff.SberbankAccountantWithWorkersV15,
                    Tariff.SberbankAccountantWithoutWorkersV15,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlaceV15,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlaceV15,
                    Tariff.SberbankAccountantMaxPromo,
                    Tariff.SberbankAccountantMaxOsnoPromo,
                    Tariff.SberbankAccountantWithWorkersPromo,
                    Tariff.SberbankAccountantWithoutWorkersPromo,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlacePromo,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlacePromo
                };
            }
        }
        
        public static List<Tariff> SberbankPromoTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankAccountantMaxPromo,
                    Tariff.SberbankAccountantMaxOsnoPromo,
                    Tariff.SberbankAccountantWithWorkersPromo,
                    Tariff.SberbankAccountantWithoutWorkersPromo,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlacePromo,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlacePromo
                };
            }
        }

        public static List<Tariff> SberbankBizMboTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankWithoutWorkersV11,
                    Tariff.SberbankWithWorkersV11,
                    Tariff.SberbankMaxV11,
                    Tariff.SberbankWithoutWorkersV13,
                    Tariff.SberbankWithWorkersV13,
                    Tariff.SberbankMaxV13
                };
            }
        }

        public static List<Tariff> SberbankAccMboTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankAccountantWithOutWorkers,
                    Tariff.SberbankAccountantWithWorkers,
                    Tariff.SberbankAccountantMax,
                    Tariff.SberbankAccountantOsnoEnvdV12,
                    Tariff.SberbankAccountantMaxOsnoV13,
                    Tariff.SberbankAccountantWithoutWorkersV13,
                    Tariff.SberbankAccountantWithWorkersV13,
                    Tariff.SberbankAccountantMaxV13,
                    Tariff.SberbankWithoutWorkersV13,
                    Tariff.SberbankWithWorkersV13,
                    Tariff.SberbankMaxV13,
                    Tariff.SberbankAccountantMaxV14,
                    Tariff.SberbankAccountantMaxOsnoV14,
                    Tariff.SberbankAccountantWithWorkersV14,
                    Tariff.SberbankAccountantWithoutWorkersV14,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlaceV14,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlaceV14,
                    Tariff.SberbankAccountantMaxV15,
                    Tariff.SberbankAccountantMaxOsnoV15,
                    Tariff.SberbankAccountantWithWorkersV15,
                    Tariff.SberbankAccountantWithoutWorkersV15,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlaceV15,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlaceV15,
                    Tariff.SberbankAccountantMaxPromo,
                    Tariff.SberbankAccountantMaxOsnoPromo,
                    Tariff.SberbankAccountantWithWorkersPromo,
                    Tariff.SberbankAccountantWithoutWorkersPromo,
                    Tariff.SberbankAccountantWithWorkersAndMarketPlacePromo,
                    Tariff.SberbankAccountantWithoutWorkersAndMarketPlacePromo
                };
            }
        }

        public static List<Tariff> SberbankBizBipTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankIp6WithoutWorkers
                };
            }
        }

        public static List<Tariff> SberbankAccBipTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankAccountantIp6WithoutWorkers
                };
            }
        }

        public static List<Tariff> SberbankAccRdbTariffs
        {
            get
            {
                return new List<Tariff>
                {
                    Tariff.SberbankSolutionForBusinessAccWithWorkers
                };
            }
        }

        /// <summary>
        /// Сбербанк тариф
        /// </summary>
        [Obsolete("Используй вместо этого проверку на наличие у тарифа права AccessRule.SberbankTariff")]
        public static bool IsSberbankTariff(this Tariff tariff)
        {
            return SberbankTariffs.Contains(tariff);
        }

        /// <summary> Сбербанк тариф с ограничением</summary>
        public static bool IsSberbankTariffWithRestrictions(this Tariff tariff)
        {
            return new List<Tariff>
                {
                    Tariff.SberbankTrial,
                    Tariff.SberbankBasic,
                    Tariff.SberbankPremium
                }.Contains(tariff);
        }
        
        /// <summary> Сбербанк тарифы премиум или максимальные </summary>
        public static bool IsSberbankTariffsPremiumOrMax(this Tariff tariff)
        {
            return new List<Tariff>
            {
                Tariff.SberbankPremium,
                Tariff.SberbankMaxV11,
                Tariff.SberbankAccountantMax,
                Tariff.SberbankAccountantOsnoEnvdV12,
                Tariff.SberbankMaxV13,
                Tariff.SberbankAccountantMaxV13,
                Tariff.SberbankAccountantMaxOsnoV13,
                Tariff.SberbankAccountantMaxV14,
                Tariff.SberbankAccountantMaxOsnoV14,
                Tariff.SberbankAccountantMaxV15,
                Tariff.SberbankAccountantMaxOsnoV15,
                Tariff.SberbankAccountantMaxPromo,
                Tariff.SberbankAccountantMaxOsnoPromo
            }.Contains(tariff);
        }

        private static readonly HashSet<Tariff> glavuchetTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.GlavuchetBiz,
            Tariff.GlavuchetUsnEnvd,
            Tariff.GlavuchetOsno,
            Tariff.SberbankOutsourcing
        });
        /// <summary>
        /// Тариф главучет
        /// </summary>
        public static bool IsGlavuchetTariff(this Tariff tariff)
        {
            return glavuchetTariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldAccUsnEnvdTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.ProfessionalUsnWithoutWorkers,
            Tariff.ProfessionalUsnUpToFive,
            Tariff.ProfessionalUsnMax,
            Tariff.ProfessionalUsnWithoutWorkersPlus,
            Tariff.ProfessionalUsnUpToFivePlus,
            Tariff.ProfessionalUsnMaxPlus
        });

        [Obsolete]
        public static bool IsAccUsnEnvdTariff(this Tariff tariff)
        {
            return oldAccUsnEnvdTariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldAccUsnEnvdTariffsFrom062017Tariffs = new HashSet<Tariff>(new[]
        {
            Tariff.ProfessionalUsnWithoutWorkersFrom062017,
            Tariff.ProfessionalUsnUpToFiveFrom062017,
            Tariff.ProfessionalUsnMaxFrom062017,
            Tariff.ProfessionalUsnWithoutWorkersPlusFrom062017,
            Tariff.ProfessionalUsnUpToFivePlusFrom062017,
            Tariff.ProfessionalUsnMaxPlusFrom062017,
            Tariff.ProfessionalUsnSubtotalMax,
            Tariff.ProductAccountingWithoutWorkers,
            Tariff.ProductAccountingUpToFive,
            Tariff.ProductAccountingMax,
            Tariff.Premium,
            Tariff.PremiumCheckContragent,
            Tariff.PremiumPlus,
            Tariff.PremiumCheckContragentPlus,
            Tariff.Comfort,
            Tariff.ComfortCheckContragent,
            Tariff.ComfortPlus,
            Tariff.ComfortCheckContragentPlus,
            Tariff.Econom,
            Tariff.EconomCheckContragent,
            Tariff.EconomPlus,
            Tariff.EconomCheckContragentPlus,
            Tariff.PremiumManagementAccounting,
            Tariff.PremiumManagementAccountingPlus
        });

        public static bool IsAccUsnEnvdTariffFrom062017(this Tariff tariff)
        {
            return oldAccUsnEnvdTariffsFrom062017Tariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldUsnEnvdTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.UsnWithoutWorkers,
            Tariff.UsnUpToFive,
            Tariff.UsnMax,
            Tariff.UsnWithoutWorkersPlus,
            Tariff.UsnUpToFivePlus,
            Tariff.UsnMaxPlus,
            Tariff.UsnMaxDemo
        });

        public static bool IsUsnEnvdTariff(this Tariff tariff)
        {
            return oldUsnEnvdTariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldUsnEnvdTariffsFrom062017Tariffs = new HashSet<Tariff>(new[]
        {
            Tariff.UsnWithoutWorkersFrom062017,
            Tariff.UsnUpToFiveFrom062017,
            Tariff.UsnMaxFrom062017,
            Tariff.UsnWithoutWorkersPlusFrom062017,
            Tariff.UsnUpToFivePlusFrom062017,
            Tariff.UsnMaxPlusFrom062017,
            Tariff.UsnSubtotalMax
        });

        public static bool IsUsnEnvdTariffFrom062017(this Tariff tariff)
        {
            return oldUsnEnvdTariffsFrom062017Tariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldUsnEnvdWithoutWorkersTariffs = new HashSet<Tariff>(new[]
        {
            Tariff.UsnWithoutWorkers,
            Tariff.UsnWithoutWorkersPlus,
            Tariff.UsnWithoutWorkersFrom062017,
            Tariff.UsnWithoutWorkersPlusFrom062017,
            Tariff.ProfessionalUsnWithoutWorkers,
            Tariff.ProfessionalUsnWithoutWorkersPlus,
            Tariff.ProfessionalUsnWithoutWorkersFrom062017,
            Tariff.ProfessionalUsnWithoutWorkersPlusFrom062017,
            Tariff.ProductAccountingWithoutWorkers
        });

        public static bool IsUsnEnvdWithoutWorkersTariff(this Tariff tariff)
        {
            return oldUsnEnvdWithoutWorkersTariffs.Contains(tariff);
        }

        private static readonly HashSet<Tariff> oldProfessionalOsnoTariffsFrom062017Tariffs = new HashSet<Tariff>(new[]
        {
            Tariff.ProfessionalOsnoFrom062017,
            Tariff.ProfessionalOsnoPlusFrom062017,
            Tariff.ProfessionalOsnoSubtotalMax,
            Tariff.SkbbankProfessionalOsno,
            Tariff.ProductAccountingOsnoMax,
            Tariff.OsnoEnvdUpTo5Workers,
            Tariff.PremiumOsno,
            Tariff.PremiumOsnoCheckContragent,
            Tariff.PremiumOsnoPlus,
            Tariff.PremiumOsnoCheckContragentPlus,
            Tariff.PremiumOsnoManagementAccounting,
            Tariff.PremiumOsnoManagementAccountingPlus
        });

        public static bool IsProfessionalOsnoTariffFrom062017(this Tariff tariff)
        {
            return oldProfessionalOsnoTariffsFrom062017Tariffs.Contains(tariff);
        }

        public static bool IsCashTariffForNewUser(this Tariff tariff)
        {
            return Tariff.MoedeloCashNewUser == tariff;
        }

        public static bool IsCashTariffWithAccess(this Tariff tariff)
        {
            return Tariff.UsnMaxCash == tariff
                || Tariff.AccountingUsnMaxCash == tariff
                || Tariff.OsnoMaxCash == tariff;
        }

        public static bool IsCashTariffOutsource(this Tariff tariff)
        {
            return Tariff.BusinessWentAndCash == tariff
                || Tariff.OutsorcePersonalAccountantCashV11 == tariff;
        }

        public static bool IsSubtotal2017(this Tariff tariff)
        {
            return Tariff.UsnSubtotalUpToFive == tariff
                   || Tariff.ProfessionalUsnSubtotalUpToFive == tariff;
        }

        public static readonly IReadOnlyCollection<int> compoundTariffIds = new[]
        {
            (int)Tariff.CompoundTariff,
            (int)Tariff.CompoundBizTariff,
            (int)Tariff.CompoundSpsTariff
        };

        /// <summary>
        /// это один из тарифов, в которые мапятся платежи из нового биллинга
        /// </summary>
        public static bool IsTariffCompound(this int tariffId)
        {
            return compoundTariffIds.Contains(tariffId);
        }

        public static bool IsTariffWithTestData(this Tariff tariff)
        {
            return tariffsWithTestData.Contains(tariff) || tariff.IsUsnAccountant();
        }

        public static bool IsOldWithoutWorkersTariff(this Tariff tariff)
        {
            return OldWithoutWorkersTariff == tariff;
        }
    }
}