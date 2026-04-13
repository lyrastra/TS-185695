namespace Moedelo.CommonV2.EventBus.Backoffice
{
    public class SendBillCommand
    {
        /// <summary>
        /// Номер счёта на отправку
        /// </summary>
        public string BillNumber { get; set; }
        /// <summary>
        /// Необходимо ли отправлять письмо на основную почту пользователя фирмы, которой был выставлен счёт
        /// </summary>
        public bool SendToUser { get; set; }
        /// <summary>
        /// E-mail оператора, которому также необходимо отправить счёт
        /// </summary>
        public string OperatorEmail { get; set; }
        /// <summary>
        /// Дополнительный e-mail для отправки счёта
        /// </summary>
        public string AdditionalSendBillEmail { get; set; }
    }
}