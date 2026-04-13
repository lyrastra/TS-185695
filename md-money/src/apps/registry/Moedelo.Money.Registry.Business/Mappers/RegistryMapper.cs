using Moedelo.Money.Kafka.Abstractions.Registry.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Business.Mappers
{
    internal static class RegistryMapper
    {
        internal static OperationCreated MapToCreatedEvent(CreateMoneyOperationCommand command)
        {
            return new()
            {
                DocumentBaseId = command.DocumentBaseId,
                Date = command.Date,
                Number = command.Number,
                OperationType = command.OperationType,
                Direction = command.Direction,
                Source = new Kafka.Abstractions.Models.Source
                {
                    Id = command.Source.Id,
                    Type = command.Source.Type
                },
                Sum = command.Sum,
                IsPaid = command.IsPaid
            };
        }

        internal static OperationUpdated MapToUpdatedEvent(UpdateMoneyOperationCommand command)
        {
            return new()
            {
                DocumentBaseId = command.DocumentBaseId,
                Date = command.Date,
                Number = command.Number,
                OperationType = command.OperationType,
                Direction = command.Direction,
                Source = new Kafka.Abstractions.Models.Source
                {
                    Id = command.Source.Id,
                    Type = command.Source.Type
                },
                Sum = command.Sum,
                IsPaid = command.IsPaid
            };
        }

        internal static OperationDeleted MapToDeletedEvent(DeleteMoneyOperationCommand command)
        {
            return new()
            {
                DocumentBaseId = command.DocumentBaseId,
                Date = command.Date,
                Number = command.Number,
                OperationType = command.OperationType
            };
        }
    }
}
