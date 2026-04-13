    using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain
{
    public interface IContractorWithRequisites
    {
        int Id { get; set; }
        string Name { get; set; }
        int? Form { get; set; }
        string Inn { get; set; }
        string Kpp { get; set; }
        string SettlementAccount { get; set; }
        string BankName { get; set; }
        string BankBik { get; set; }
        string BankCorrespondentAccount { get; set; }
        /// <summary>
        ///  остыльное поле дл¤ обрубани¤ валидации бика дл¤ валютных операций
        /// »мпорт создает контрагента резедента который должен быть нерезедентом
        /// пожтому валидаци¤ не дает пересохран¤ть валютные операции AD-956
        /// </summary>
        bool IsCurrency { get; set; }


    }
}