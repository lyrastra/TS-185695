namespace Moedelo.AstralV3.Client.Types
{
    /// <summary>Объект для хранения информации о уиде и получателях</summary>
    public class UidWithListReceivers
    {
        /// <summary>Уид отправки/письма</summary>
        public string Uid { get; set; }

        /// <summary>Массив гуидов получателей</summary>
        public string[] Receivers { get; set; }

        /// <summary>Отправитель</summary>
        public string Sender { get; set; }
    }
}
