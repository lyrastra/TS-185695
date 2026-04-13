using System;

namespace Moedelo.Docs.Dto.Docs
{
    public class ForgottenDocumentDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime ForgottenDocumentDate { get; set; }
        public string ForgottenDocumentNumber { get; set; }
    }
}
