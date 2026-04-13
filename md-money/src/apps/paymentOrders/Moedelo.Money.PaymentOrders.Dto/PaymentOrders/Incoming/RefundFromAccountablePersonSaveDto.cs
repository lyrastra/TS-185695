namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

public class RefundFromAccountablePersonSaveDto : RefundFromAccountablePersonDto
{
    public MissingWorkerRequisitesDto MissingWorkerRequisites { get; set; }
}