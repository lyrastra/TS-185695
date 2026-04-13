using System.Collections.Generic;

namespace Moedelo.Common.Enums.Enums.Catalog
{
    public static class BankRegistrationNumberExtention
    {
        private static Dictionary<BankRegistrationNumber, string> Names = new Dictionary<BankRegistrationNumber, string>
        {
            { BankRegistrationNumber.AbsolutBank, "absolut" },
            { BankRegistrationNumber.AgroSouz, "agrosouz" },
            { BankRegistrationNumber.AlfaBank, "alfa" },
            { BankRegistrationNumber.Avangard, "avangard" },
            { BankRegistrationNumber.BBR, "bbr" },
            //{ BankRegistrationNumber.BBR, "binBank" },
            { BankRegistrationNumber.BksBank, "bks" },
            { BankRegistrationNumber.CitiBank, "citiBank" },
            //{ BankRegistrationNumber.GazpromBank, "gazpromBank" },
            //{ BankRegistrationNumber.HomeCredit, "homeCredit" },
            { BankRegistrationNumber.IterKommercBank, "interKommerc" },
            { BankRegistrationNumber.Inteza, "inteza" },
            { BankRegistrationNumber.LokoBank, "lokoBank" },
            { BankRegistrationNumber.ModulBank, "modulBank" },
            //{ BankRegistrationNumber.MoskowskiyIndustrialniyBank, "moskowskiyIndustrialniy" },
            { BankRegistrationNumber.MoskowskyiKreditniyBank, "moskowskyiKreditniy" },
            { BankRegistrationNumber.MosOblBank, "mosOblBank" },
            { BankRegistrationNumber.MtsBank, "mts" },
            { BankRegistrationNumber.NovikomBank, "novikomBank" },
            { BankRegistrationNumber.Otrkitie, "otkritie" },
            { BankRegistrationNumber.OTP, "otp" },
            { BankRegistrationNumber.PochtaBank, "pochta" },
            { BankRegistrationNumber.PromsvyazBank, "promsvyazBank" },
            { BankRegistrationNumber.Raiffaizen, "raiffaizen" },
            { BankRegistrationNumber.RenessansKredit, "renessansKredit" },
            { BankRegistrationNumber.Rnkb, "rnkb" },
            { BankRegistrationNumber.Rosbank, "rosbank" },
            //{ BankRegistrationNumber.RosgosstrahBank, "rosgosstrahBank" },
            { BankRegistrationNumber.RosselhozBank, "rosselhozBank" },
            { BankRegistrationNumber.Rossia, "rossia" },
            //{ BankRegistrationNumber.RossiyskiyKapital, "rossiyskiyKapital" },
            { BankRegistrationNumber.RusskiyStandart, "russkiyStandart" },
            { BankRegistrationNumber.SanktPeterburg, "sanktPeterburg" },
            { BankRegistrationNumber.SbBank, "sbBank" },
            { BankRegistrationNumber.Sberbank, "sberbank" },
            { BankRegistrationNumber.SdmBank, "sdmBank" },
            { BankRegistrationNumber.SkbBank, "skbBank" },
            { BankRegistrationNumber.SmpBank, "smpBank" },
            { BankRegistrationNumber.SovkomBank, "sovkomBank" },
            { BankRegistrationNumber.TinkoffBank, "tinkoffBank" },
            { BankRegistrationNumber.TKB, "tkb" },
            { BankRegistrationNumber.Tochka, "tochka" },
            //{ BankRegistrationNumber.Touch, "touch" },
            { BankRegistrationNumber.Trast, "trast" },
            { BankRegistrationNumber.UniCredit, "uniCredit" },
            { BankRegistrationNumber.Uralsib, "uralsib" },
            //{ BankRegistrationNumber.Uralskiy, "uralskiy" },
            //{ BankRegistrationNumber.VBBR, "vbrr" },
            { BankRegistrationNumber.Vostochniy, "vostochniy" },
            { BankRegistrationNumber.Vozrojdenie, "vozrojdenie" },
            { BankRegistrationNumber.VTB, "vtb" },
        };

        public static string GetIconName(this BankRegistrationNumber bankRegistrationNumber)
        {
            return Names.ContainsKey(bankRegistrationNumber)
                ? Names[bankRegistrationNumber]
                : null;
        }
    }
}
