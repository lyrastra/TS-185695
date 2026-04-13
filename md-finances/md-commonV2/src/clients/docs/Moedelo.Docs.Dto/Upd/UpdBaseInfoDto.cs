using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Docs.Dto.Upd
{
    /// <summary>
    /// Основные свойства УПД как 
    /// Это не полная бизнес-модель и даже не все поля УПД, хранимые в БД,
    /// но существуют задачи, в которых этих полей достаточно (например, в md-bookkeeping для реализации контракта ILinkedDocument)
    /// </summary>
    public class UpdBaseInfoDto
    {
        public int Id { get; set; }
        
        public long DocumentBaseId { get; set; }
        
        public string DocumentNumber { get; set; }
        
        public decimal DocumentSum { get; set; }
        
        public DateTime DocumentDate { get; set; }
        
        public ProvidePostingType TaxPostingType { get; set; }
    }
}