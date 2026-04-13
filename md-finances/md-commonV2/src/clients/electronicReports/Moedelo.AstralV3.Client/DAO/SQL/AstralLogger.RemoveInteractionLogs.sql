delete
	top(@chunkSize)
from
	dbo.AstralInteractionLog
where
	EventDateTime < @thresholdDate