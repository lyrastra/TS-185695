--drop index before creating duplicates
drop index uq__bank_balance_history__firm_id__settlement_account_id__balance_date;

--alter column to date type
alter table bank_balance_history alter balance_date type date using balance_date::timestamp;

--clear duplicates, leave only one record for one firm-account-date
with ranked (id, rnk) as (
	select
		id,
		row_number() over (partition by firm_id, settlement_account_id, balance_date) as rnk
		from bank_balance_history bbh 
)
delete from
	bank_balance_history as bbh
	using ranked as r
	where
		bbh.id = r.id and r.rnk > 1;
	
--create index again
create unique index if not exists uq__bank_balance_history__firm_id__settlement_account_id__balance_date
	on bank_balance_history (firm_id, settlement_account_id, balance_date);