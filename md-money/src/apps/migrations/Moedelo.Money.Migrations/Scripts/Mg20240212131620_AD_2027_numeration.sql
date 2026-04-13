--Храним значение номера платёжного поручения начиная с которого ищется новый номер(в данный момент минимальный свободный)
--По мотивам AD-2027 нумерация производится для исходящих платежей в разрезе фирмы, расчетного счета и года
create table if not exists payment_order_number
(
	id serial not null,
	firm_id int not null,
	settlement_account_id int not null,
	year smallint not null,
	last_number int not null,
	constraint pk__payment_order_number__id primary key (id)
);

create unique index if not exists uq__payment_order_number__firm_id__settlement_account_id__year
	on payment_order_number (firm_id, settlement_account_id, year);