using System;
using Moedelo.Requisites.Enums.FirmRequisites;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
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
        
        public DateTime? RegistrationDate { get; set; }
        
        public string PhoneCode { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Okpo { get; set; }
        
        /// <summary>
        /// Реквизит в шапке счета
        /// </summary>
        public string BillHeader { get; set; }

        /// <summary>
        /// Печатать QR-код в счетах
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