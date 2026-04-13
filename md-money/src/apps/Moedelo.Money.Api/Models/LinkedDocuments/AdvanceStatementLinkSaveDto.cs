using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class AdvanceStatementLinkSaveDto
    {
        /// <summary>
        /// Идентификатор авансового отчета
        /// </summary>
        [IdLongValue]
        public long DocumentBaseId { get; set; }
    }
}
