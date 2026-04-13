using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class NewAdvanceStatementClientData : BaseClientData
    {
        public NewAdvanceStatementClientData()
        {
            BusinessTripItems = new List<NewAdvanceStatementBusinessTripItemClientData>();
            PaymentToSupplierItems = new List<NewAdvanceStatementPaymentSupplierItemClientData>();
            GoodsAndMaterialsItems = new List<NewAdvanceStatementProductAndMaterialItemClientData>();
            AdvanceDocuments = new List<FinancialDocumentClientData>();
            DebtDocuments = new List<FinancialDocumentClientData>();
            TaxPostings = new List<TaxPostingClientData>();
            AdvanceDocumentsList = new List<FinancialDocumentClientData>();
            DebtDocumentsList = new List<FinancialDocumentClientData>();
        }

    public long Id { get; set; }

    public long DocumentBaseId { get; set; }

    public string Number { get; set; }

    public string Date { get; set; }

    public int WorkerId { get; set; }

    public string WorkerName { get; set; }

    /// <summary>
    /// Дата увольнения сотрудника
    /// </summary>
    public string WorkerEndDate { get; set; }

    public WorkerBalanceClientData WorkerBalance { get; set; }

    public decimal Sum { get; set; }

    public decimal BalanceToCashSum { get; set; }

    public bool IsEnterBalanceOfCash { get; set; }

    public long CashId { get; set; }

    public long? AdvancePaymentDocumentId { get; set; }

    public AdvancePaymentDocumentTypes AdvancePaymentDocumentType { get; set; }

    public AdvanceStatementType AdvanceStatementType { get; set; }

    public BusinessTripClientData BusinessTrip { get; set; }

    public IList<NewAdvanceStatementPaymentSupplierItemClientData> PaymentToSupplierItems { get; set; }

    public IList<NewAdvanceStatementBusinessTripItemClientData> BusinessTripItems { get; set; }

    public IList<NewAdvanceStatementProductAndMaterialItemClientData> GoodsAndMaterialsItems { get; set; }

    public bool Closed { get; set; }

    public bool CanEdit { get; set; }

    public List<TaxPostingClientData> TaxPostings { get; set; }

    public bool ProvideInAccounting { get; set; }

    public TaxationSystemType? TaxationSystemType { get; set; }

    public ProvidePostingType PostingsAndTaxMode { get; set; }

    /// <summary>Документы на выдачу аванса (доступные)</summary>
    public IList<FinancialDocumentClientData> AdvanceDocumentsList { get; set; }

    /// <summary>Документы на выдачу перерасхода (доступные)</summary>
    public IList<FinancialDocumentClientData> DebtDocumentsList { get; set; }

    /// <summary>Документы на выдачу аванса (привязанные к отчёту)</summary>
    public IList<FinancialDocumentClientData> AdvanceDocuments { get; set; }

    /// <summary>Документы на выдачу перерасхода (привязанные к отчёту)</summary>
    public IList<FinancialDocumentClientData> DebtDocuments { get; set; }

    /// <summary>
    /// Долг сотрудника (если меньше 0, трактуется как долг организации)
    /// </summary>
    public decimal DebtSum { get; set; }

    public string Comment { get; set; }

    public string CreateUser { get; set; }

    public string ModifyUser { get; set; }

    public string CreateDate { get; set; }

    public string ModifyDate { get; set; }

    /// <summary>
    /// Временный BaseId - выдаётся создаваемому документу
    /// Не является настоящим baseId и не может быть использован для идентификации документа в системе
    /// Используется _только_ для загрузки сканов во время процедуры создания документа
    /// Должен быть либо равен baseId (документ не новый), либо 0 (не установлен), либо меньше 0 (только что созданный документ)
    /// Получается клиентом через специальный сервис при создании документа
    /// </summary>
    public long TemporaryBaseId { get; set; }
  }
}
