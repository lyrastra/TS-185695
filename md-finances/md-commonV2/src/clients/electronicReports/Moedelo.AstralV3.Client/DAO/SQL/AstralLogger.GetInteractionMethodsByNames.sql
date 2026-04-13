select
	Id				as Id,
	MethodName		as MethodName,
	Mode			as Mode
from
    moedelo_logs.dbo.AstralInteractionMethod
where
    MethodName in @methodNames
