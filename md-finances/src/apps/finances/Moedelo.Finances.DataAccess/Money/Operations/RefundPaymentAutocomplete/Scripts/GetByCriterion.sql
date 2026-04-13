/*declare @RetailRefundBaseId int, -- розн. возврат, из которого был вызван автокомплит
		@direction int, -- направление (только для РКО)
		@paidStatus int = 6, -- оплаченные документы (только для п/п)
		@paymentLinkType int = 2, -- тип связи платежа с документом-основанием
		@retailRefundDocType int = 34, -- тип базового док-та для возврата от покупателя
		@FirmId int,
		@OperationTypes int(max), -- список типов ден. операций,
		@RetailRefundBaseId int,
		@KontragentId int, 
		@query varchar(max),
		@Offset int,
		@limit int,
		@excludeAccountCodes table (id int) -- исключаемые счета
		  insert @excludeAccountCodes(id) values(760600),(620200);
		*/

select 
	p.DocumentBaseId,
	p.KontragentId,
	p.Number,
	p.Date,
	p.Sum,
	p.DocumentType,
	count(1) over (partition by (select null)) as TotalCount
from (
	select distinct
		po.DocumentBaseId,
		po.KontragentId,
		po.PaymentNumber Number,
		po.Date,
		po.Sum,
		5 DocumentType
	from Accounting_PaymentOrder po
	left join AccountingLinkOfDocuments l on po.DocumentBaseId = l.LinkedFromId and l.Type = @paymentLinkType
	left join AccountingLinkedDocumentBase ldb on ldb.Id = l.LinkedToId and ldb.Type = @retailRefundDocType
	where po.FirmId = @FirmId
/*-- kontragent_condition -- 
	and po.KontragentId = @KontragentId
-- kontragent_condition --*/
	and po.OperationType in @OperationTypes
	and po.PaidStatus = @paidStatus
/*-- query_condition --
	and po.PaymentNumber like '%' + @query + '%'
-- query_condition --*/
	and (ldb.Id is null or ldb.Id = @RetailRefundBaseId)
/*-- excludeAccountCodes --
  and po.KontragentAccountCode not in (select id from @excludeAccountCodes)
-- excludeAccountCodes --*/

	union all

	select distinct
		co.DocumentBaseId,
		co.KontragentId,
		co.Number,
		co.Date,
		co.Sum,
		10 DocumentType
	from Accounting_CashOrder co
	left join AccountingLinkOfDocuments l on co.DocumentBaseId = l.LinkedFromId and l.Type = @paymentLinkType
	left join AccountingLinkedDocumentBase ldb on ldb.Id = l.LinkedToId and ldb.Type = @retailRefundDocType
/*-- excludeAccountCodes --
	left join dbo.SyntheticAccountType sat ON sat.Id = co.SyntheticAccountTypeId
-- excludeAccountCodes --*/
	where co.FirmId = @FirmId
/*-- kontragent_condition -- 
	and co.KontragentId = @KontragentId
-- kontragent_condition --*/
	and co.Direction = @direction
	and co.OperationType in @OperationTypes
/*-- query_condition --
	and co.Number like '%' + @query + '%'
-- query_condition --*/
	and (ldb.Id is null or ldb.Id = @RetailRefundBaseId)
/*-- excludeAccountCodes --
  and sat.Code not in (select id from @excludeAccountCodes)
-- excludeAccountCodes --*/
) p
order by p.DocumentBaseId desc
offset @Offset rows fetch next @limit rows only