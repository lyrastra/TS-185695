using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.Eds.Dto.RequestArchive
{
    public class EdsRequestDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public string FirmInn { get; set; }
        public string Login { get; set; }
        public EdsRequestType Type { get; set; }
        public DateTime CertificateCreateDate { get; set; }
        public DateTime CertificateExpirationDate { get; set; }
        public string FolderName { get; set; }
        public bool IsDocumentsReceived { get; set; }
        public EdsRequestTransferMethod TransferMethod { get; set; }
        public string Comment { get; set; }
        public List<int> Tags { get; set; }
    }
}