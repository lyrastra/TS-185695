-- ОПИСАНИЕ:
-- 1. Находит в БД все таблицы с полем firm_id
-- 2. Удаляет из этих таблиц все строки с firm_id, содержащим значения из переданного списка temp_deleted_firm::id 
-- ВХОДНЫЕ ПАРАМЕТРЫ: скрипт ожидает наличие табличного парметра temp_deleted_firm (id int not null)

-----------------------------------------------------------------------
-- отладочные данные, которые могут быть полезны при локальной отладке
-- create temp table if not exists temp_deleted_firm
-- (
--     id int not null
-- );
-- 
-- insert into temp_deleted_firm (id) values
--                                           (1111), (2222), (3333);
-- 
-- create temp table if not exists temp_debug
-- (
--     str varchar
-- );

-----------------------------------------------------------------------

-- глобальная таблица для передачи результатов из exec в текущую сессию
-- drop table if exists temp_global_data_cleaning_results;
create global temp table if not exists temp_global_data_cleaning_results
(
    transaction_id    int          not null,
    table_name        varchar(256) not null,
    deleted_row_count int          not null,
    is_error          bit          not null,
    message           varchar
    );

-- глобальная таблица для передачи идентификаторов фирм из текущей сессии в exec
create global temp table if not exists temp_global_deleted_firm
(
    transaction_id    int          not null,
    id int not null
);

-- локальная временная таблица для хранения результатов текущей сессии
-- drop table if exists temp_transaction_results;
create temp table temp_transaction_results
(
    table_name        varchar(256) not null,
    deleted_row_count int          not null,
    is_error          bit          not null,
    message           varchar
);

do
$$
    declare
target_table record;
        declare cmd varchar(1024);
        declare current_transaction_id int;
        declare exception_message text;
begin
        -- генерим случайный идентификатор для текущей сессии (проведуры удаления)
select random() * 10000000 into current_transaction_id;

-- помещаем идентификаторы в глобальную временную таблицу с идентификаторами удалённых фирм 
insert into temp_global_deleted_firm (transaction_id, id)
select current_transaction_id, id from temp_deleted_firm;

-- собираем все таблицы в схеме public со столбцом firm_id
for target_table in select t.table_schema || '.' || t.table_name as full_table_name,
                           c.column_name
                    from information_schema.tables t
                             inner join information_schema.columns c on c.table_name = t.table_name
                        and c.table_schema = t.table_schema
                    where c.column_name = 'firm_id'
                      and t.table_schema = 'public'
                      and t.table_type = 'BASE TABLE'
                    order by t.table_schema
                        loop
                -- генерируем скрипт удаления строк по firm_id и сбора статистики исполнения этого скрипта
                cmd := 'do $generated_delete_statement$ '
                       || 'declare '
                       || ' start_time timestamptz;'
                       || ' end_time timestamptz;'
                       || ' delta double precision;'                       
                       || ' deleted_count int;'                       
                       || ' begin '
                           || ' start_time := clock_timestamp();'
                           || ' with deleted as (delete from '
                           || target_table.full_table_name || ' as target_table '
                           || ' using temp_global_deleted_firm as firm'
                           || ' where target_table.' || target_table.column_name || ' = firm.Id'
                           || '   and firm.transaction_id = ' || current_transaction_id::varchar
                           || ' returning 1) select count(*) from deleted into deleted_count;'
                           || ' end_time := clock_timestamp();'
                           || 'insert into temp_global_data_cleaning_results (transaction_id, table_name, deleted_row_count, is_error, message) '
                           || 'values( '
                           || current_transaction_id::varchar || ','
                           || quote_literal(target_table.full_table_name) || ','
                           || ' deleted_count, 0::bit, '
                           || ' ''done at '' || round((1000 * ( extract(epoch from end_time) - extract(epoch from start_time) ))::numeric, 2)::text || '' msec''' 
                           || ');'
                    || ' end $generated_delete_statement$';
-- это может быть полезно при локальной отладке
-- insert into temp_debug (str) select cmd;

-- исполняем сгенерированный скрипт (собственно удаление строк из очередной таблицы)
begin
execute cmd;
exception when others then
                    -- обработка исключения - записываем сообщение об ошибке в результаты
                    get stacked diagnostics exception_message = message_text;
insert into temp_global_data_cleaning_results (transaction_id, table_name, deleted_row_count, is_error, message)
values (current_transaction_id, target_table.full_table_name, 0, 1::bit, exception_message::varchar);
end;
end loop;
        -- копируем значения из глобальной временной таблицы в локальную временную
insert into temp_transaction_results (table_name, deleted_row_count, is_error, message)
select table_name, deleted_row_count, is_error, message
from temp_global_data_cleaning_results as r
where r.transaction_id = current_transaction_id;

-- зачищаем данные текущей сессии из глобальных временных таблиц
delete from temp_global_data_cleaning_results where transaction_id = current_transaction_id;
delete from temp_global_deleted_firm where transaction_id = current_transaction_id;
end
$$;

select
    table_name as "TableName",
    deleted_row_count as "DeletedRowCount",
    is_error as "IsError",
    message as "Message" 
from temp_transaction_results;
-----------------------------------------------------------------------

-- пригодится, если отлаживать в каком-нибудь инструменте
-- select * from temp_debug;
-- truncate temp_transaction_results;
-- truncate temp_deleted_firm;
-- truncate temp_debug;
