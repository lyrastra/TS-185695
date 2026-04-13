drop view if exists topic_consuming_state_view;

create view topic_consuming_state_view
as
select id,
       consumer_group_id,
       topic,
       partition,
       committed_offset,
       committed_offset + offset_map_depth as consumer_offset,
       committed_date_utc,
       offset_map_depth,
       compression_type,
       offset_map,
       length(offset_map)                  as offset_map_length,
       case
           when compression_type = 'Raw' then
               right(offset_map::text, -1)::varbit::text
           else
               '(compressed)'
           end                             as offset_bin_map
from topic_consuming_state
         inner join topic_consuming_state_bin_data tcsbd on topic_consuming_state.id = tcsbd.state_id;
;