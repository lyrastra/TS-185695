namespace Moedelo.Spam.ApiClient.Abastractions.Constants.PushNotifications
{
    public static class PushType
    {
        public const string TaskNewComment = "task_new_comment";
        public const string СhatNewMessage = "chat_new_message";
        public const string Info = "info";
        public const string Birthday = "Birthday";
        public const string BalanceNotification = "balance_notification";
        public const string InvoicePaymentDetails = "invoice_payment_details";
        public const string ExpiringBillingPayment = "expiring_billing_payment";
        public const string ChangeEmdStatus = "change_edm_status";
        public const string OutsourceTaskWaitingForClientFeedback = "outsource_task_waiting_for_client_feedback";
        public const string DemandReceived = "demand_received";
        public const string AcceptedReports = "accepted_reports";
        public const string PaymentRequestRejected = "payment_request_rejected";
        public const string PaymentRequestInitiated = "payment_request_initiated";
        public const string PaymentRequestPreApproved = "payment_request_pre_approved";
        public const string PaymentRequestApproved = "payment_request_approved";
        public const string PaymentRequestPaid = "payment_request_paid";
        public const string SalaryCalendarEventPaid = "salary_calendar_event_paid";
        public const string UnpaidSaleBillsWithoutDueDate = "unpaid_sale_bills_without_due_date_for_month";
        public const string SaleBillsDueOnDate = "sale_bills_due_on_date";
        public const string MassNotification = "mass_notification";
        public const string MissingRequisites = "missing_requisites";
    }
}