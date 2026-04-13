using System;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.Documents
{
    public class DocumentRequisitesDto
    {
        public int? DirectorId { get; set; }
        
        public int? AccountantId { get; set; }
        
        public string Inn { get; set; }
        
        public bool IsOoo { get; set; }
        
        public string Kpp { get; set; }
        
        public string Ogrn { get; set; }
        
        public string Pseudonym { get; set; }
        
        public string Surname { get; set; }
        
        public string Name { get; set; }
        
        public string Patronymic { get; set; }
        
        public bool NeedAccountant { get; set; }
        
        public string ShortPseudonym { get; set; }
        
        public string InFace { get; set; }
        
        public string InReason { get; set; }
        
        public string PhoneCode { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Okpo { get; set; }
        
        public DateTime? RegistrationDate { get; set; }
        
        /// <summary>
        /// Контакты в шапке счетов 
        /// </summary>
        public string BillHeader { get; set; }
        
        /// <summary>
        /// Отображать QR-код для оплаты в счетах
        /// </summary>
        public bool IsBillQrCodeEnabled { get; set; }

        /// <summary>
        /// Форма собственности
        /// </summary>
        public Opf Opf { get; set; }
        
        /// <summary>
        /// Настройка переноса заголовка таблицы в документах
        /// </summary>
        public DocumentTableHeaderCarryoverMode TableHeaderCarryoverMode { get; set; }
    }
}