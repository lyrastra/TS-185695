set nocount on;
set transaction isolation level read uncommitted;

select iif(exists(
    select u.id
    from dbo.Users as u
    inner join dbo.Account as a
        on u.AccountId = a.Id
    where u.id = @UserId
        and (a.Type = 2
        or (a.Type = 1
        and @UserId <> a.MainAdminId)) ), 1, 0) as isExist;
