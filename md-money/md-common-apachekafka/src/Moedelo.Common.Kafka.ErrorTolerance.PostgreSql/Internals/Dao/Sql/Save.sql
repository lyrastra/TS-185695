with state as (
    insert into topic_consuming_state
        (consumer_group_id, topic, partition, committed_offset, committed_date_utc, offset_map_depth)
        values (:ConsumerGroupId, :Topic, :Partition, :CommittedOffset, :CommittedDateUtc, :OffsetMapDepth)
        on conflict on constraint uq__topic_consuming_state__consumer_group_id__topic__partition
            do update set
                committed_date_utc = excluded.committed_date_utc,
                committed_offset = excluded.committed_offset,
                offset_map_depth = excluded.offset_map_depth
        returning id)
insert into topic_consuming_state_bin_data
(state_id, compression_type, offset_map)
select
    state.id,
    :DataCompressionType::offset_map_compression_type,
    :OffsetMapData --decode('DEADBEEF', 'hex')
from state
on conflict on constraint pk__topic_consuming_state_bin_data__state_id
do update set compression_type = excluded.compression_type,
              offset_map = excluded.offset_map
;
