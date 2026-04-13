select firm_id               as FirmId,
       settlement_account_id as SettlementAccountId,
       balance_date          as BalanceDate,
       end_balance           as Balance,
       modify_date           as ModifyDate
from (select h.firm_id,
             h.settlement_account_id,
             h.balance_date,
             h.end_balance,
             h.modify_date,
             row_number() over (
                 partition by h.firm_id, h.settlement_account_id
                 order by h.balance_date desc, h.id desc
                 ) as rowNum
      from bank_balance_history as h
               join temp_firm_ids as ids on
          ids.Id = h.firm_id
      where h.balance_date <= @onDate
        and h.balance_date >= @minDate
         --IsNotUserMovementFilter-- and is_user_movement = false
     ) as balances
where balances.rowNum = 1;
