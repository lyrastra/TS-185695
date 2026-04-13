using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.TaxationSystems;
using Moedelo.Money.Providing.Business.TaxPostings;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using LinkedDocumentType = Moedelo.LinkedDocuments.Enums.LinkedDocumentType;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.TaxPostings
{
    [InjectAsSingleton(typeof(PaymentFromCustomerTaxPostingsProvider))]
    internal class PaymentFromCustomerTaxPostingsProvider
    {
        private readonly TaxationSystemReader taxationSystemReader;
        private readonly FirmRequisitesReader requisitesReader;
        private readonly TaxPostingsRemover taxPostingsRemover;
        private readonly UsnPostingsSaver usnPostingsSaver;
        private readonly PatentPostingsSaver patentPostingsSaver;
        private readonly IBaseDocumentsCommandWriter commandWriter;

        private static readonly LinkedDocumentType[] LinkedDocumentTypesAllowedToChangeTaxStatus = new[]
        {
            LinkedDocumentType.Statement,
            LinkedDocumentType.Waybill
        };

        public PaymentFromCustomerTaxPostingsProvider(
            TaxationSystemReader taxationSystemReader,
            FirmRequisitesReader requisitesReader,
            TaxPostingsRemover taxPostingsRemover,
            UsnPostingsSaver usnPostingsSaver,
            PatentPostingsSaver patentPostingsSaver,
            IBaseDocumentsCommandWriter commandWriter)
        {
            this.taxationSystemReader = taxationSystemReader;
            this.requisitesReader = requisitesReader;
            this.taxPostingsRemover = taxPostingsRemover;
            this.usnPostingsSaver = usnPostingsSaver;
            this.patentPostingsSaver = patentPostingsSaver;
            this.commandWriter = commandWriter;
        }

        public async Task ProvideAsync(PaymentFromCustomerTaxPostingsProvideRequest request)
        {
            if (request.IsBadOperationState
                || request.IsManualTaxPostings) // обработается командой на кастомные проводки
            {
                return;
            }

            var taxSystem = await taxationSystemReader.GetByYearAsync(request.Date.Year);
            if (taxSystem == null)
            {
                return;
            }

            await taxPostingsRemover.DeleteAsync(request.DocumentBaseId);

            if (taxSystem.IsUsn && request.TaxationSystemType != TaxationSystemType.Patent)
            {
                var taxPostingsGenerateRequest = GetGenerationModel(request);
                var usnPostingsResponse = PaymentFromCustomerUsnPostingsGenerator.Generate(taxPostingsGenerateRequest);

                // ПЕРЕЗАПИСЫВАЕМ проводки платежа
                await usnPostingsSaver.OverwriteAsync(request.DocumentBaseId, usnPostingsResponse.Postings);
                // устанавливаем НУ-статусы платежа и связанных док-тов
                await SetTaxStatusesAsync(request, usnPostingsResponse);

                return;
            }

            if (taxSystem.IsUsn && request.TaxationSystemType == TaxationSystemType.Patent)
            {
                var taxPostingsGenerateRequest = GetGenerationModel(request);
                var usnPostingsResponse = PaymentFromCustomerUsnPostingsGenerator.Generate(taxPostingsGenerateRequest);

                await patentPostingsSaver.OverwriteAsync(request.DocumentBaseId, usnPostingsResponse.Postings.Select(MapToPatent).ToArray());
                await SetTaxStatusesAsync(request, usnPostingsResponse);

                return;
            }

            if (taxSystem.IsOsno && request.TaxationSystemType != TaxationSystemType.Patent)
            {
                if (!await requisitesReader.IsOooAsync())
                {
                    // для ИП-ОСНО расчет проводок и установка статуса происходит отдельно
                    await taxPostingsRemover.DeleteAsync(request.DocumentBaseId);
                    return;
                }
                
                // ООО ОСНО: нет проводок, но нужно установить НУ-статус
                await SetDocumentTaxStatusAsync(request, new OsnoTaxPostingsResponse(Moedelo.TaxPostings.Enums.TaxPostingStatus.NotTax));
            }

            if (taxSystem.IsOsno && request.TaxationSystemType == TaxationSystemType.Patent)
            {
                var taxPostingsGenerateRequest = GetGenerationModel(request);
                var osnoPatentPostingsResponse = PaymentFromCustomerOsnoPatentPostingsGenerator.Generate(taxPostingsGenerateRequest);

                await patentPostingsSaver.OverwriteAsync(request.DocumentBaseId, osnoPatentPostingsResponse.Postings);
                await SetTaxStatusesAsync(request, osnoPatentPostingsResponse);
                return;
            }
            
            await taxPostingsRemover.DeleteAndUnsetTaxStatusAsync(request.DocumentBaseId);
        }

        private async Task SetTaxStatusesAsync<T>(
            PaymentFromCustomerTaxPostingsProvideRequest request, 
            TaxPostingsResponse<T> result) where T: ITaxPosting, IDocumentId, IRelatedDocumentBaseIds
        {
            await SetDocumentTaxStatusAsync(request, result);
            await SetRelatedDocumentsTaxStatusesAsync(request, result);
        }

        private async Task SetRelatedDocumentsTaxStatusesAsync<T>(
            PaymentFromCustomerTaxPostingsProvideRequest request,
            TaxPostingsResponse<T> result) where T : ITaxPosting, IDocumentId, IRelatedDocumentBaseIds
        {
            var relatedBaseIds = result.Postings
                .SelectMany(x => x.RelatedDocumentBaseIds)
                // исключаем "основной" док-т: уже выставили статус
                .Where(baseId => baseId != request.DocumentBaseId)
                .Distinct()
                .ToArray();

            if (!relatedBaseIds.Any())
            {
                return;
            }

            // связанным документам, участвующим в проводках, выставляем статус (если не выставлен)
            await request.BaseDocuments
                .Where(baseDocument => LinkedDocumentTypesAllowedToChangeTaxStatus.Contains(baseDocument.Type)
                                       && relatedBaseIds.Contains(baseDocument.Id)
                                       && baseDocument.TaxStatus != TaxPostingStatus.Yes
                                       && baseDocument.TaxStatus != TaxPostingStatus.ByLinkedDocument)
                .RunParallelAsync(
                    baseDocument => commandWriter.WriteAsync(new SetTaxStatusCommand
                    {
                        Id = baseDocument.Id,
                        TaxStatus = TaxPostingStatus.ByLinkedDocument
                    }));
        }

        private Task SetDocumentTaxStatusAsync<T>(
            PaymentFromCustomerTaxPostingsProvideRequest request,
            ITaxPostingsResponse<T> result) where T : ITaxPosting
        {
            // НУ статус платежа
            return commandWriter.WriteAsync(new SetTaxStatusCommand
            {
                Id = request.DocumentBaseId,
                TaxStatus = (TaxPostingStatus) result.TaxStatus
            });
        }

        private PaymentFromCustomerPostingsBusinessModel GetGenerationModel(PaymentFromCustomerTaxPostingsProvideRequest request)
        {
            return new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = (Requisites.Enums.TaxationSystems.TaxationSystemType)request.TaxationSystemType,
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                KontragentId = request.Kontragent.Id,
                KontragentName = request.Kontragent.Name,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                MediationNdsSum = request.MediationNdsSum,
                IsMediation = request.IsMediation,
                MediationCommissionSum = request.MediationCommissionSum,
                DocumentLinks = request.DocumentLinks,
                Statements = request.Statements,
                Waybills = request.Waybills,
                Upds = request.Upds,
            };
        }

        private PatentTaxPosting MapToPatent(UsnTaxPosting model)
        {
            return new PatentTaxPosting
            {
                Date = model.Date,
                Description = model.Description,
                DocumentId = model.DocumentId,
                RelatedDocumentBaseIds = model.RelatedDocumentBaseIds,
                Sum = model.Sum
            };
        }
    }
}
