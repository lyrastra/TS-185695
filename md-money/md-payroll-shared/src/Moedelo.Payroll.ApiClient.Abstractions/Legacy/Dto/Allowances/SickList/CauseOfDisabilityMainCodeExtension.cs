namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public static class CauseOfDisabilityMainCodeExtension
    {
        public static bool IsSanatoriumVoucherMainCode(this CauseOfDisabilityMainCode? code)
        {
            return code is CauseOfDisabilityMainCode.Code08;
        }

        public static bool IsCareForRelativeType(this CauseOfDisabilityMainCode? code)
        {
            return code != null && (code == CauseOfDisabilityMainCode.Code03 ||
                                    code == CauseOfDisabilityMainCode.Code09 ||
                                    code == CauseOfDisabilityMainCode.Code12 ||
                                    code == CauseOfDisabilityMainCode.Code13 ||
                                    code == CauseOfDisabilityMainCode.Code14 ||
                                    code == CauseOfDisabilityMainCode.Code15);
        }

        public static bool IsProfTraumaOrIllness(this CauseOfDisabilityMainCode? code)
        {
            return code != null &&
                   (code == CauseOfDisabilityMainCode.Code04 || code == CauseOfDisabilityMainCode.Code07);
        }
        
        public static bool IsStationaryType(this CauseOfDisabilityMainCode code)
        {
            return code == CauseOfDisabilityMainCode.Code09 || code == CauseOfDisabilityMainCode.Code12 ||
                   code == CauseOfDisabilityMainCode.Code13 || code == CauseOfDisabilityMainCode.Code14 ||
                   code == CauseOfDisabilityMainCode.Code15;
        }
    }
}
