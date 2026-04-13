using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ElectronicReportsV2.Dto.StatusInfo
{
    /// <summary> Информация об электронной отчетности фирмы </summary>
    public class ElectronicReportsStatusInfoForFirmResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Статус отчетности в ФНС </summary>
        public string FnsReportStatus { get; set; }

        /// <summary> Статус отчтности в ПФР и ФСС </summary>
        public string PfrAndFssReportStatus { get; set; }

        /// <summary> Статус отчетности в Росстат </summary>
        public string RosstatReportStatus { get; set; }

        /// <summary> Статус электронной подписи </summary>
        public string SignatureStatus { get; set; }
        
        /// <summary> Статус электронной подписи enum </summary>
        public SignatureStatus SignatureStatusAsEnum { get; set; }
    }
}