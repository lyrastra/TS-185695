--We need receiving bank balance data timestamp
alter table bank_balance_history add column if not exists modify_date timestamp(0) null;