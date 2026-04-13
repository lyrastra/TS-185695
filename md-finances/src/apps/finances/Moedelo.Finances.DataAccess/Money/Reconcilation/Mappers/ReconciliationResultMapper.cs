using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.Finances.DataAccess.Money.Reconcilation.Mappers
{
    public static class ReconciliationResultMapper
    {
        public static ReconciliationOperation Map(this ReconciliationOperationDto dto)
        {
            return new ReconciliationOperation
            {
                Id = dto.Id,
                IsOutgoing = dto.IsOutgoing,
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                KontragentId = dto.KontragentId,
                KontragentName = dto.KontragentName,
                DocumentSection = dto.DocumentSection,
                IsSalary = dto.IsSalary
            };
        }
    
        public static ReconciliationOperationDto Map(this ReconciliationOperation operation)
        {
            return new ReconciliationOperationDto
            {
                Id = operation.Id,
                IsOutgoing = operation.IsOutgoing,
                Sum = operation.Sum,
                Date = operation.Date,
                Number = operation.Number,
                Description = operation.Description,
                KontragentId = operation.KontragentId,
                KontragentName = operation.KontragentName,
                DocumentSection = operation.DocumentSection,
                IsSalary = operation.IsSalary
            };
        }
    }
}