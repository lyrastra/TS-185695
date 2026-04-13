using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Api.Mappers
{
    public static class LinkedDocumentsMapper
    {
        public static BillLinkDto[] MapBills(IReadOnlyCollection<BillLink> bills)
        {
            return bills?.Select(Map).ToArray();
        }

        public static DocumentLinkDto[] MapDocuments(IReadOnlyCollection<DocumentLink> documentLinks)
        {
            return documentLinks?.Select(Map).ToArray();
        }

        public static BillLinkSaveRequest[] MapBillLinks(IReadOnlyCollection<BillLinkSaveDto> links)
        {
            return links?.Select(link => new BillLinkSaveRequest
            {
                BillBaseId = link.DocumentBaseId,
                LinkSum = link.Sum
            }).ToArray() ?? Array.Empty<BillLinkSaveRequest>();
        }

        public static DocumentLinkSaveRequest[] MapDocumentLinks(IReadOnlyCollection<DocumentLinkSaveDto> links)
        {
            return links?.Select(link => new DocumentLinkSaveRequest
            {
                DocumentBaseId = link.DocumentBaseId,
                LinkSum = link.Sum
            }).ToArray() ?? Array.Empty<DocumentLinkSaveRequest>();
        }

        private static ContractDto MapContract(ContractLink contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new ContractDto
            {
                DocumentBaseId = contract.DocumentBaseId,
                Number = contract.Number,
                Date = contract.Date,
                Kind = contract.ContractKind
            };
        }

        public static RemoteServiceResponseDto<ContractDto> MapContractResponse(RemoteServiceResponse<ContractLink> response)
        {
            return new RemoteServiceResponseDto<ContractDto>
            {
                Data = MapContract(response.Data),
                Status = response.Status
            };
        }

        public static RemoteServiceResponseDto<IReadOnlyCollection<BillLinkDto>> MapBillsResponse(RemoteServiceResponse<IReadOnlyCollection<BillLink>> response)
        {
            return new RemoteServiceResponseDto<IReadOnlyCollection<BillLinkDto>>
            {
                Data = MapBills(response.Data),
                Status = response.Status
            };
        }

        public static RemoteServiceResponseDto<IReadOnlyCollection<DocumentLinkDto>> MapDocumentsResponse(RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> response)
        {
            return new RemoteServiceResponseDto<IReadOnlyCollection<DocumentLinkDto>>
            {
                Data = MapDocuments(response.Data),
                Status = response.Status
            };
        }

        public static BillLinkSaveRequest[] MapBillLinks(IReadOnlyCollection<Kafka.Abstractions.Models.BillLink> links)
        {
            return links?.Select(link => new BillLinkSaveRequest
            {
                BillBaseId = link.BillBaseId,
                LinkSum = link.LinkSum
            }).ToArray() ?? Array.Empty<BillLinkSaveRequest>();
        }

        public static DocumentLinkSaveRequest[] MapDocumentLinks(IReadOnlyCollection<Kafka.Abstractions.Models.DocumentLink> links)
        {
            return links?.Select(link => new DocumentLinkSaveRequest
            {
                DocumentBaseId = link.DocumentBaseId,
                LinkSum = link.LinkSum
            }).ToArray() ?? Array.Empty<DocumentLinkSaveRequest>();
        }

        private static BillLinkDto Map(BillLink bill)
        {
            return new BillLinkDto
            {
                DocumentBaseId = bill.DocumentBaseId,
                Date = bill.Date,
                Number = bill.Number,
                DocumentSum = bill.BillSum,
                Sum = bill.LinkSum,
                PaidSum = bill.PaidSum
            };
        }

        private static DocumentLinkDto Map(DocumentLink document)
        {
            return new DocumentLinkDto
            {
                DocumentBaseId = document.DocumentBaseId,
                Type = document.Type,
                Date = document.Date,
                Number = document.Number,
                DocumentSum = document.DocumentSum,
                Sum = document.LinkSum,
                PaidSum = document.PaidSum
            };
        }

        public static RemoteServiceResponseDto<decimal?> MapReserveResponse(RemoteServiceResponse<decimal?> reserveResponse)
        {
            return new RemoteServiceResponseDto<decimal?>
            {
                Data = reserveResponse.Data,
                Status = reserveResponse.Status
            };
        }
    }
}
