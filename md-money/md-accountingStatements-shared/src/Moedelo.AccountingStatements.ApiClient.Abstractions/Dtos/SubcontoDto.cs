using Moedelo.AccountingStatements.Enums;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos
{
    public class SubcontoDto
    {
        public object Id { get; set; }

        public long? SubcontoId { get; set; }

        public SubcontoType SubcontoType { get; set; }

        public string SubcontoName { get; set; }
    }
}