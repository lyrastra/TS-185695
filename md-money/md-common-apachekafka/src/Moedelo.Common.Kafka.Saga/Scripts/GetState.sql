select 
    ss.saga_guid as "SagaId",
    ss.type as "StateType",
    ss.data as "StateData",
    ss.token as "ExecutionContextToken"
from state ss
where ss.saga_guid = :SagaId
