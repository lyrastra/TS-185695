using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(BillLinksValidator))]
    internal sealed class BillLinksValidator
    {
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;

        public BillLinksValidator(
            IBaseDocumentReader baseDocumentReader,
            LinkedDocumentPaidSumReader paidSumReader)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.paidSumReader = paidSumReader;
        }

        public async Task ValidateAsync(long paymentBaseId, IReadOnlyCollection<BillLinkSaveRequest> billLinks)
        {
            var documentIds = billLinks.Select(x => x.BillBaseId).ToArray();
            await DocumentLinksDuplicateValidator.Validate(documentIds);

            var billBaseDocuments = (await baseDocumentReader.GetByIdsAsync(documentIds))
                .Where(x => x.Type == LinkedDocumentType.Bill)
                .ToArray();

            var baseDocumentsById = billBaseDocuments.ToDictionary(x => x.Id);
            var paidSums = await paidSumReader.GetPaidSumsForIncomingPaymentAsync(paymentBaseId, billBaseDocuments, new[] { LinkedDocumentType.Bill });

            var i = 0;
            foreach (var bill in billLinks)
            {
                if (baseDocumentsById.TryGetValue(bill.BillBaseId, out var baseDocument) == false)
                {
                    throw new BusinessValidationException($"Bills[{i}].DocumentBaseId", $"Не найден счёт с базовым ид {bill.BillBaseId}");
                }
                paidSums.TryGetValue(bill.BillBaseId, out var paidSum);
                var accessibleSum = baseDocument.Sum - paidSum;
                if (accessibleSum < bill.LinkSum)
                {
                    throw new BusinessValidationException($"Bills[{i}].Sum", $"Сумма счёта с базовым ид {bill.BillBaseId} доступная к оплате меньше, чем сумма указаная к оплате");
                }
                i++;
            }
        }
    }
}
