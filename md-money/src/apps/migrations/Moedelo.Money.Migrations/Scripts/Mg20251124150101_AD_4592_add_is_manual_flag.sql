alter table bank_balance_history add column if not exists is_user_movement boolean default false;

alter table bank_balance_history alter column is_user_movement drop default;

commit transaction;

drop index if exists uq__bank_balance_history__firm_id__settlement_account_id__balan;
-- Recreate index without locking table
create unique index uq__bank_balance_history__firm_id__settlement_account_id__balan
    on bank_balance_history (firm_id, settlement_account_id, balance_date, is_user_movement);

start transaction;
