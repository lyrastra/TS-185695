select
	min(balance_date) = @StartDate and
	max(balance_date) = @EndDate and
	upper(daterange(date(@StartDate), date(@EndDate + interval '1' day))) -
	lower(daterange(date(@StartDate), date(@EndDate + interval '1' day))) = count(1)
	from bank_balance_history
	where firm_id = @FirmId
		and settlement_account_id = @SettlementAccountId
		and balance_date between @StartDate and @EndDate
      --IsNotUserMovementFilter-- and is_user_movement = false
		