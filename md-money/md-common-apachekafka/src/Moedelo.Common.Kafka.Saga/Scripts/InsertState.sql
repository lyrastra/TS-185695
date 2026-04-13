insert into state
(saga_guid, type, data, token, modify_date)
values (:SagaId,
        :StateType,
        :StateData::jsonb,
        :ExecutionContextToken,
        now())
;
