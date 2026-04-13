using System;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos
{
    public class BaseDocumentUpdateDto
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}