using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class KafkaConsumerStateMapper
    {
        public static KafkaConsumerStateModel Map(this KafkaConsumerStateDto dto)
        {
            return new KafkaConsumerStateModel
            {
                GroupId = dto.GroupId,
                Topic = dto.Topic,
                ServiceId = dto.ServiceId,
                ConsumerGuid = dto.ConsumerGuid,
                State = dto.State,
                AssignedPartitions = dto.AssignedPartitions ?? new List<int>(),
                TotalLag = dto.TotalLag,
                PartitionLags = dto.PartitionLags ?? new Dictionary<string, long>(),
                CommittedOffsets = dto.CommittedOffsets ?? new Dictionary<string, long>(),
                EndOffsets = dto.EndOffsets ?? new Dictionary<string, long>(),
                HasLagProblem = dto.HasLagProblem,
                HasPausedPartitions = dto.HasPausedPartitions,
                PausedPartitionsCount = dto.PausedPartitionsCount,
                MaxLagTimeSeconds = dto.MaxLagTimeSeconds,
                OldestUnprocessedMessageTime = dto.OldestUnprocessedMessageTime
            };
        }

        public static KafkaConsumerStateDto Map(this KafkaConsumerStateModel model)
        {
            return new KafkaConsumerStateDto
            {
                GroupId = model.GroupId,
                Topic = model.Topic,
                ServiceId = model.ServiceId,
                ConsumerGuid = model.ConsumerGuid,
                State = model.State,
                AssignedPartitions = model.AssignedPartitions ?? new List<int>(),
                TotalLag = model.TotalLag,
                PartitionLags = model.PartitionLags ?? new Dictionary<string, long>(),
                CommittedOffsets = model.CommittedOffsets ?? new Dictionary<string, long>(),
                EndOffsets = model.EndOffsets ?? new Dictionary<string, long>(),
                HasLagProblem = model.HasLagProblem,
                HasPausedPartitions = model.HasPausedPartitions,
                PausedPartitionsCount = model.PausedPartitionsCount,
                MaxLagTimeSeconds = model.MaxLagTimeSeconds,
                OldestUnprocessedMessageTime = model.OldestUnprocessedMessageTime
            };
        }

        public static List<KafkaConsumerStateModel> Map(this List<KafkaConsumerStateDto> list)
        {
            return list.Select(dto => dto.Map()).ToList();
        }

        public static List<KafkaConsumerStateDto> Map(this List<KafkaConsumerStateModel> list)
        {
            return list.Select(model => model.Map()).ToList();
        }
    }
}
