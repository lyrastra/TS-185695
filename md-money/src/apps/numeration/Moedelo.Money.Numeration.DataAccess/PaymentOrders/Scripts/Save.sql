insert 
	into payment_order_number 
		(firm_id, settlement_account_id, year, last_number) 
	values (@firmId, @settlementAccountId, @year, @number)
    on 
		conflict (firm_id, settlement_account_id, year) do update
    set 
		last_number = @number