using System;

namespace Moedelo.ErptV2.Dto.FnsNeformalDocuments
{
    public class FnsNeformalDocumentsIonDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public DateTime DateBegin { get; set; }
        public int Year { get; set; }
        public int Type { get; set; }
        public string TypeDoc { get; set; }
        public string TaxCode { get; set; }
        public string Kpp { get; set; }
    }
}