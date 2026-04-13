using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.TaxPostings.Enums;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands.IpOsno;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands.Osno;
using Moedelo.TaxPostings.Kafka.Abstractions.Usn.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands.Patent;
using PatentTaxPosting = Moedelo.TaxPostings.Kafka.Abstractions.Commands.Patent.PatentTaxPosting;
using Moedelo.Money.Business.PaymentOrders.Providing;

namespace Moedelo.Money.Business.TaxPostings
{
    [InjectAsSingleton(typeof(ICustomTaxPostingsSaver))]
    internal sealed class CustomTaxPostingsSaver : ICustomTaxPostingsSaver
    {
        private readonly IUsnTaxPostingsCommandWriter usnTaxPostingsCommandWriter;
        private readonly IOsnoTaxPostingsCommandWriter osnoTaxPostingsCommandWriter;
        private readonly IIpOsnoTaxPostingsCommandWriter ipOsnoTaxPostingsCommandWriter;
        private readonly IPatentTaxPostingsCommandWriter patentTaxPostingsCommandWriter;

        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;

        private readonly ITaxPostingsRemover taxPostingsRemover;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;

        public CustomTaxPostingsSaver(
            IUsnTaxPostingsCommandWriter usnTaxPostingsCommandWriter,
            IOsnoTaxPostingsCommandWriter osnoTaxPostingsCommandWriter,
            IIpOsnoTaxPostingsCommandWriter ipOsnoTaxPostingsCommandWriter,
            IPatentTaxPostingsCommandWriter patentTaxPostingsCommandWriter,
            IFirmRequisitesReader firmRequisitesReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            ITaxPostingsRemover taxPostingsRemover,
            PaymentOrderProvidingStateSetter providingStateSetter)
        {
            this.usnTaxPostingsCommandWriter = usnTaxPostingsCommandWriter;
            this.osnoTaxPostingsCommandWriter = osnoTaxPostingsCommandWriter;
            this.ipOsnoTaxPostingsCommandWriter = ipOsnoTaxPostingsCommandWriter;
            this.patentTaxPostingsCommandWriter = patentTaxPostingsCommandWriter;
            this.firmRequisitesReader = firmRequisitesReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.taxPostingsRemover = taxPostingsRemover;
            this.providingStateSetter = providingStateSetter;
        }

        public async Task OverwriteAsync(CustomTaxPostingsOverwriteRequest overwriteRequest)
        {
            // на условие опираются Other*OutsourceProcessor-ы: учесть, если возникнет необходимость поменять логику 
            if (overwriteRequest.Postings?.ProvidePostingType != Enums.ProvidePostingType.ByHand)
            {
                return;
            }

            var isOooTask = firmRequisitesReader.IsOooAsync();
            var taxationSystemTask = taxationSystemTypeReader.GetFullByYearAsync(overwriteRequest.DocumentDate.Year);

            await Task.WhenAll(isOooTask, taxationSystemTask);

            var isOoo = isOooTask.Result;
            var taxationSystem = taxationSystemTask.Result;
            var taxationSystemType = overwriteRequest.TaxationSystemType;

            if (taxationSystem.IsUsn)
            {
                var providingStateId = await providingStateSetter.SetCustomTaxPostingsStateAsync(overwriteRequest.DocumentBaseId);
                await OverwriteUsnAsync(
                    overwriteRequest.DocumentBaseId,
                    overwriteRequest.DocumentDate,
                    overwriteRequest.DocumentNumber,
                    overwriteRequest.Postings.UsnTaxPostings,
                    providingStateId);
                return;
            }

            if (taxationSystem.IsOsno && isOoo)
            {
                await OverwriteOsnoAsync(
                    overwriteRequest.DocumentBaseId,
                    overwriteRequest.DocumentDate,
                    overwriteRequest.Postings.OsnoTaxPostings);
                return;
            }

            if (taxationSystem.IsOsno && !isOoo && taxationSystemType == TaxationSystemType.Patent)
            {
                await OverwritePatentAsync(
                    overwriteRequest.DocumentBaseId,
                    overwriteRequest.DocumentDate,
                    overwriteRequest.Postings.PatentTaxPostings);
                return;
            }

            if (taxationSystem.IsOsno && !isOoo)
            {
                await OverwriteIpOsnoAsync(
                    overwriteRequest.DocumentBaseId,
                    overwriteRequest.DocumentDate,
                    overwriteRequest.DocumentNumber,
                    overwriteRequest.Description,
                    overwriteRequest.Postings.IpOsnoTaxPosting);
                return;
            }
        }

        private Task OverwriteUsnAsync(long documentBaseId, DateTime documentDate, string documentNumber,
            IReadOnlyCollection<UsnTaxPosting> postings, long providingStateId)
        {
            var usnPostings = MapToUsnCommand(documentBaseId, documentDate, documentNumber, postings, providingStateId);
            return usnTaxPostingsCommandWriter.WriteOverwriteAsync(usnPostings);
        }

        private Task OverwriteOsnoAsync(long documentBaseId, DateTime documentDate,
            IReadOnlyCollection<Domain.TaxPostings.OsnoTaxPosting> postings)
        {
            var osnoPostings = MapToOsnoCommand(documentBaseId, documentDate, postings);
            return osnoTaxPostingsCommandWriter.WriteAsync(osnoPostings);
        }

        private async Task OverwriteIpOsnoAsync(long documentBaseId, DateTime documentDate, string documentNumber,
            string description, IpOsnoTaxPosting posting)
        {
            await taxPostingsRemover.DeletePatentPostingsAsync(documentBaseId);
            var osnoPostings = MapToIpOsnoCommand(documentBaseId, documentDate, documentNumber, description, posting);
            await ipOsnoTaxPostingsCommandWriter.WriteAsync(osnoPostings);
        }

        private Task OverwritePatentAsync(long documentBaseId, DateTime documentDate,
            IReadOnlyCollection<Domain.TaxPostings.PatentTaxPosting> postings)
        {
            var patentPostings = MapToPatentCommand(documentBaseId, documentDate, postings);
            return patentTaxPostingsCommandWriter.WriteAsync(patentPostings);
        }

        private static OverwriteUsnTaxPostings MapToUsnCommand(long documentBaseId, DateTime documentDate,
            string documentNumber, IReadOnlyCollection<UsnTaxPosting> postings, long providingStateId)
        {
            return new OverwriteUsnTaxPostings
            {
                DocumentBaseId = documentBaseId,
                DocumentDate = documentDate,
                DocumentNumber = documentNumber,
                Postings = postings.Select(posting =>
                    new Moedelo.TaxPostings.Kafka.Abstractions.Usn.Models.UsnTaxPosting
                    {
                        Date = documentDate,
                        Sum = posting.Sum,
                        Direction = posting.Direction,
                        Description = posting.Description,
                        RelatedDocumentBaseIds = new[] {documentBaseId}
                    }).ToArray(),
                TaxStatus = postings.Count > 0
                    ? TaxPostingStatus.ByHand
                    : TaxPostingStatus.No,
                ProvidingStateId = providingStateId
            };
        }

        private static OverwriteOsnoTaxPostingsCommand MapToOsnoCommand(long documentBaseId, DateTime documentDate,
            IReadOnlyCollection<Domain.TaxPostings.OsnoTaxPosting> postings)
        {
            return new OverwriteOsnoTaxPostingsCommand
            {
                DocumentBaseId = documentBaseId,
                DocumentDate = documentDate,
                Postings = postings.Select(posting =>
                    new Moedelo.TaxPostings.Kafka.Abstractions.Commands.Osno.OsnoTaxPosting
                    {
                        Date = documentDate,
                        Sum = posting.Sum,
                        Direction = posting.Direction,
                        Type = posting.Type,
                        Kind = posting.Kind,
                        NormalizedCostType = posting.NormalizedCostType
                    }).ToArray(),
                TaxStatus = postings.Count > 0
                    ? TaxPostingStatus.ByHand
                    : TaxPostingStatus.No
            };
        }

        private static OverwriteIpOsnoTaxPostingsCommand MapToIpOsnoCommand(
            long documentBaseId,
            DateTime documentDate,
            string documentNumber,
            string description,
            IpOsnoTaxPosting posting)
        {
            return new OverwriteIpOsnoTaxPostingsCommand
            {
                DocumentBaseId = documentBaseId,
                PaymentNumber = documentNumber,
                PaymentDescription = description,
                Date = documentDate,
                Sum = posting?.Sum ?? 0m,
                Direction = posting?.Direction ?? 0
            };
        }

        private static OverwritePatentTaxPostingsCommand MapToPatentCommand(
            long documentBaseId,
            DateTime documentDate,
            IReadOnlyCollection<Domain.TaxPostings.PatentTaxPosting> postings)
        {
            return new OverwritePatentTaxPostingsCommand
            {
                DocumentBaseId = documentBaseId,
                DocumentDate = documentDate,
                Postings = postings.Select(posting => new PatentTaxPosting
                {
                    Date = documentDate,
                    Sum = posting.Sum,
                    Description = posting.Description,
                    RelatedDocumentBaseIds = new[] {documentBaseId}
                }).ToArray(),
                TaxStatus = postings.Count > 0
                    ? TaxPostingStatus.ByHand
                    : TaxPostingStatus.No
            };
        }
    }
}