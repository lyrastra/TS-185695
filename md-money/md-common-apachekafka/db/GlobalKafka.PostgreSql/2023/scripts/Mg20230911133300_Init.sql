create type offset_map_compression_type as enum('Raw', 'Zstd');

create table topic_consuming_state
(
    id int not null generated always as identity,
    consumer_group_id varchar(256) not null,
    topic varchar(128) not null,
    partition smallint not null,
    committed_offset bigint null,
    committed_date_utc timestamp(0) null,
    offset_map_depth int null,

    constraint pk__topic_consuming_state__id primary key (id),
    constraint uq__topic_consuming_state__consumer_group_id__topic__partition unique (consumer_group_id, topic, partition)
);

create table topic_consuming_state_bin_data
(
    state_id int not null,
    compression_type offset_map_compression_type not null,
    offset_map bytea not null,

    constraint pk__topic_consuming_state_bin_data__state_id primary key (state_id),
    constraint fk__topic_consuming_state_bin_data__state foreign key (state_id)
        references topic_consuming_state (id) on delete cascade
);
