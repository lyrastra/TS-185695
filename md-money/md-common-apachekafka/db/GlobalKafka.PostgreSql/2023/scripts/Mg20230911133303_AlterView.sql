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
               bin_view.value
           else
               '(compressed)'
           end                             as offset_bin_map_first64
from topic_consuming_state
         inner join topic_consuming_state_bin_data tcsbd on topic_consuming_state.id = tcsbd.state_id,
    lateral (
        select case
                   when compression_type = 'Raw' then
                       least(8, length(offset_map) - 1)
                   else
                       -1
                   end as length
        ) as bin_view_settings,
     lateral(
         select string_agg(reverse(get_byte(offset_map, byte_num)::bit(8)::text), '') as value
         from generate_series(0, bin_view_settings.length) byte_num
         ) as bin_view;
;