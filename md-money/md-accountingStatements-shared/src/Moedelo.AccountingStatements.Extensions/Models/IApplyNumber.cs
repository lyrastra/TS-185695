using System;

namespace Moedelo.AccountingStatements.Extensions.Models
{
    /// <summary>
    /// Для нумерации в зависимости от даты
    /// </summary>
    public interface IApplyNumber
    {
        /// <summary>
        /// Номер
        /// </summary>
        string Number { get; set; }
        
        /// <summary>
        /// Дата документа
        /// </summary>
        DateTime Date { get; }
    }
}