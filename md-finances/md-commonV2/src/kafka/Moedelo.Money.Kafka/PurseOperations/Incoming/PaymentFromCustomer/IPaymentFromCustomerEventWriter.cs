using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer
{
    /// <summary>
    /// События предполагались для OSNO-3631 "КУДИР для ИП с услугами (MVP)". На 03.12.2020 платежные системы не рассматриваются к реализации. 
    ///
    /// При реализации можно опираться на CashOrderChangedEventWriter в качестве примера.
    /// Важно учесть:
    ///  - создание
    ///  - обновление (предусмотреть поле, сигнализирующее о смене типа операции)
    ///  - удаление
    ///  - перепроведение
    /// </summary>
    public interface IPaymentFromCustomerEventWriter : IDI
    {
        Task WritePaymentFromCustomerCreatedAsync(int firmId, int userId, PaymentFromCustomerCreated eventData);
        Task WritePaymentFromCustomerUpdatedAsync(int firmId, int userId, PaymentFromCustomerUpdated eventData);
        Task WritePaymentFromCustomerDeletedAsync(int firmId, int userId, PaymentFromCustomerDeleted eventData);
    }
}