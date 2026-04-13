delete from bank_balance_history
using temp_firm_ids as firms
where firm_id = firms.id;