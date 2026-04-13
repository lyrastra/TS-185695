using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.KontragentsV2.Dto
{
    /// <summary>
    /// Модель перенесена "как есть" из md-kontragents
    /// </summary>
    public class KontragentDto
    {
        public KontragentDto()
        {
            SettlementAccounts = new List<KontragentSettlementAccountDto>();
        }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// ОКПО
        /// </summary>
        public string Okpo { get; set; }

        public int? SubAccount { get; set; }

        public bool IsFounder { get; set; }

        public long? SubcontoId { get; set; }

        public Source Source { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string Fio { get; set; }

        public int? PurseId { get; set; }

        public bool IsPopulation { get; set; }

        public KontragentType? NewType { get; set; }

        public KontragentForm? Form { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDuplicate { get; set; }

        public string LegalAddress { get; set; }

        public string ActualAddress { get; set; }

        public string RegistrationAddress { get; set; }

        public string TaxpayerNumber { get; set; }

        public string AdditionalRegNumber { get; set; }

        public List<KontragentSettlementAccountDto> SettlementAccounts { get; set; }

        /// <summary>
        /// Признак "Комиссионер"
        /// </summary>
        public bool IsCommissionAgent { get; set; }

        public int? CommissionAgentId { get; set; }

        public long? CommissionAgentStockId { get; set; }

        public KontragentSettlementAccountDto GetSettlementAccount()
        {
            if (SettlementAccounts != null)
            {
                return SettlementAccounts
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
            }

            return new KontragentSettlementAccountDto();
        }

        public string GetName()
        {
            if (Form.HasValue)
            {
                return Form == KontragentForm.FL ? Fio : ShortName;
            }

            return ShortName ?? FullName ?? Fio;
        }

        public string GetAddress()
        {
            if (Form.HasValue)
            {
                return Form == KontragentForm.FL ? RegistrationAddress : LegalAddress;
            }

            return LegalAddress ?? RegistrationAddress ?? string.Empty;
        }
    }
}