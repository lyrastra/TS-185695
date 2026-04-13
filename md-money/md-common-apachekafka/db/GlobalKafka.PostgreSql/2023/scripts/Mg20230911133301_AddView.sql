create view topic_consuming_state_view
as select
       id,
       consumer_group_id,
       topic,
       partition,
       committed_offset,
       committed_date_utc,
       offset_map_depth,
       compression_type,
       offset_map,
       length(offset_map) as offset_map_length
from topic_consuming_state
inner join topic_consuming_state_bin_data tcsbd on topic_consuming_state.id = tcsbd.state_id;
;