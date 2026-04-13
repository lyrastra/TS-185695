select
    b.Id as DocumentBaseId,
    1 as IsValid
from #BaseIds as b
join docs.PurseOperation as po on po.DocumentBaseId = b.Id;