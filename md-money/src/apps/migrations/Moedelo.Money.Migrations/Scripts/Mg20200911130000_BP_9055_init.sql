create table if not exists bank_balance_history
(
	id serial not null,
	firm_id int not null,
	settlement_account_id int not null,
	balance_date timestamp(0) not null,
	start_balance	decimal(20, 4) not null,
	end_balance	decimal(20, 4) not null,
	incoming_balance	decimal(20, 4) not null,
	outgoing_balance	decimal(20, 4) not null,
	constraint pk__bank_balance_history__id primary key (id)
);

create unique index if not exists uq__bank_balance_history__firm_id__settlement_account_id__balance_date
	on bank_balance_history (firm_id, settlement_account_id, balance_date);