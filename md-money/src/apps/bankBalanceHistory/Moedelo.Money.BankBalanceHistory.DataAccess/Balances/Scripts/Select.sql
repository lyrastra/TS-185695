with deduplicated_balances as (select start_balance,
                                      end_balance,
                                      incoming_balance,
                                      outgoing_balance,
                                      balance_date,
                                      row_number() over (partition by balance_date order by id desc) as rn_dedup
                               from bank_balance_history
                               where firm_id = @FirmId
                                 and settlement_account_id = @SettlementAccountId
                                 and balance_date between @StartDate and @EndDate
    --IsNotUserMovementFilter-- and is_user_movement = false
),
     balance_range (start_balance, end_balance, incoming_balance, outgoing_balance) as (select start_balance,
                                                                                               end_balance,
                                                                                               incoming_balance,
                                                                                               outgoing_balance,
                                                                                               row_number() over (order by balance_date)      as rn_date_asc,
                                                                                               row_number() over (order by balance_date desc) as rn_date_desc
                                                                                        from deduplicated_balances
                                                                                        where rn_dedup = 1)
select (select start_balance from balance_range where rn_date_asc = 1) as StartBalance,
       (select end_balance from balance_range where rn_date_desc = 1)  as EndBalance,
       sum(incoming_balance)                                           as IncomingBalance,
       sum(outgoing_balance)                                           as OutgoingBalance
from balance_range;
