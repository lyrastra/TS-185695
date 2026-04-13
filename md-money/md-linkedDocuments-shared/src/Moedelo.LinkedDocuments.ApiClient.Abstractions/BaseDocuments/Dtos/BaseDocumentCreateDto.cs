using System;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos
{
    public class BaseDocumentCreateDto
    {
        public LinkedDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}