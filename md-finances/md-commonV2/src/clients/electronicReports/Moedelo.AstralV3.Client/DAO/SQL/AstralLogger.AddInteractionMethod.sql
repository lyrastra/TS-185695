insert into moedelo_logs.dbo.AstralInteractionMethod
(
	MethodName,
	Mode
)
values
(
	@methodName,
	@mode
)
SELECT SCOPE_IDENTITY()
