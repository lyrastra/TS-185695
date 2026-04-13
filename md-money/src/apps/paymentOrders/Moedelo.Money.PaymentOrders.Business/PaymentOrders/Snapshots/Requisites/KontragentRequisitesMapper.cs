using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites
{
    internal static class KontragentRequisitesMapper
    {
        /// <summary>
        /// Возвращает реквизиты контрагента в представлении PaymentSnapshot
        /// </summary>
        public static OrderDetails Map(KontragentRequisites kontragentRequisites)
        {
            if (kontragentRequisites == null)
            {
                return new OrderDetails();
            }

            return new OrderDetails
            {
                Name = kontragentRequisites.Name?.Trim(),
                Inn = kontragentRequisites.Inn?.Trim(),
                Kpp = kontragentRequisites.Kpp?.Trim(),
                SettlementNumber = kontragentRequisites.SettlementAccount?.Trim(),
                BankName = kontragentRequisites.BankName?.Trim(),
                BankBik = kontragentRequisites.BankBik?.Trim(),
                BankCorrespondentAccount = kontragentRequisites.BankCorrespondentAccount?.Trim(),
                // todo: Address = ?,
                // todo: BankCity = ?,
                Okato = kontragentRequisites.Okato?.Trim(),
                Oktmo = kontragentRequisites.Oktmo?.Trim(),
                IsOoo = true, //todo: костыль для старого бека
            };
        }
    }
}