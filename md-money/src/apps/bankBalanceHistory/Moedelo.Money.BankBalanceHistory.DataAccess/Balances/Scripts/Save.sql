insert into bank_balance_history as bbh
(firm_id,
 settlement_account_id,
 balance_date,
 start_balance,
 end_balance,
 incoming_balance,
 outgoing_balance,
 is_user_movement,
 modify_date)
select distinct FirmId,
                SettlementAccountId,
                BalanceDate,
                StartBalance,
                EndBalance,
                IncomingBalance,
                OutgoingBalance,
                IsUserMovement,
                NOW()::timestamp
from temp_balances
on conflict (firm_id, settlement_account_id, balance_date, is_user_movement) do update
    set start_balance    = excluded.start_balance,
        end_balance      = excluded.end_balance,
        incoming_balance = excluded.incoming_balance,
        outgoing_balance = excluded.outgoing_balance,
        is_user_movement = excluded.is_user_movement,
        modify_date      = NOW()::timestamp