update state
    set
        type = :StateType,
        data = :StateData::jsonb,
        modify_date = now()
where
      saga_guid = :SagaId
;
