using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.ReceiptStatement.ApiClient.Abstractions;
using Moedelo.ReceiptStatement.ApiClient.Abstractions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;

namespace Moedelo.Money.Providing.Business.Estate
{
    [InjectAsSingleton(typeof(ReceiptStatementReader))]
    internal class ReceiptStatementReader
    {
        private readonly IReceiptStatementApiClient receiptStatementApiClient;

        public ReceiptStatementReader(IReceiptStatementApiClient receiptStatementApiClient)
        {
            this.receiptStatementApiClient = receiptStatementApiClient;
        }

        public async Task<Estate.Models.ReceiptStatement[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<Estate.Models.ReceiptStatement>();
            }

            var receiptStatementsDto = await baseIds
                .SelectParallelAsync(baseId => receiptStatementApiClient.GetByBaseIdAsync(baseId));

            return receiptStatementsDto.Select(Map).ToArray();
        }

        private static Estate.Models.ReceiptStatement Map(ReceiptStatementDto dto)
        {
            return dto != null
                ? new Estate.Models.ReceiptStatement
                {
                    DocumentBaseId = dto.DocumentBaseId,
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.SumWithNds
                }
                : null;
        }
    }
}
