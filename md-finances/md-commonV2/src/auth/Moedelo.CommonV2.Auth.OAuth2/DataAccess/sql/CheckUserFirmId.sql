set nocount on;
set transaction isolation level read uncommitted;

select iif(exists(
   select id
   from dbo.UserFirmData
   where id_user = @UserId
     and id_firm = @FirmId), 1, 0) as isExist;
