SELECT 
	last_number
from 
	payment_order_number
where
	firm_id = @firmId and 
	settlement_account_id = @settlementAccountId and  
	year = @year
limit 1