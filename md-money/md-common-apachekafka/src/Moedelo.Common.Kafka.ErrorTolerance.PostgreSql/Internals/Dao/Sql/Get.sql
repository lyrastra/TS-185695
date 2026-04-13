select
    state.consumer_group_id as ConsumerGroupId,
    state.topic,
    state.partition,
    state.committed_offset as CommittedOffset,
    state.committed_date_utc as CommittedDateUtc,
    state.offset_map_depth as OffsetMapDepth,
    data.compression_type as DataCompressionType,
    data.offset_map as OffsetMapData
from topic_consuming_state as state
inner join topic_consuming_state_bin_data as data on data.state_id = state.id
where
    state.consumer_group_id = :ConsumerGroupId
and state.topic = :Topic
and state.partition = :Partition
;