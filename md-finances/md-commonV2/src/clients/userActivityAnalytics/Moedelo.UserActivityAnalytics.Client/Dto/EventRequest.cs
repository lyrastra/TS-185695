namespace Moedelo.UserActivityAnalytics.Client.Dto
{
    public class EventRequest
    {
        /// <summary>
        /// EventId в базе
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Url страницы
        /// </summary>
        public string St1 { get; set; }

        /// <summary>
        /// ControlId в базе (new_header, widget_kalendar, ...)
        /// </summary>
        public string St2 { get; set; }

        /// <summary>
        /// ActionId в базе (click_text, click_point, load ...)
        /// </summary>
        public string St3 { get; set; }

        /// <summary>
        /// AbTestId в базе
        /// </summary>
        public string St4 { get; set; }

        /// <summary>
        /// Param1 в базе
        /// </summary>
        public string St5 { get; set; }

        /// <summary>
        /// Param2 в базе
        /// </summary>
        public string St6 { get; set; }

        public string SessionId { get; set; }
    }
}