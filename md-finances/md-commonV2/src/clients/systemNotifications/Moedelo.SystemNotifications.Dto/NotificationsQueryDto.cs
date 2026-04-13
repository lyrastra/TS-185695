using System;

namespace Moedelo.SystemNotifications.Dto
{
    public class NotificationsQueryDto
    {
        public string Query { get; set; }
        public bool? IsVisible {get;set;}
        public string OrderByColumn {get;set;}
        public string OrderByDirection {get;set;}
        public string CreatedBy {get;set;}
        public int? CreateUserId {get;set;}
        public DateTime? DateFrom {get;set;}
        public DateTime? DateTo {get;set;}
        public DateTime? CreatedDateFrom {get;set;}
        public DateTime? CreatedDateTo {get;set;}
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 20;
    }
}
