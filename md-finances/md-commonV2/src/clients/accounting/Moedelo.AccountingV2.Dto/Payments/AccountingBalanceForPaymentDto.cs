using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class AccountingBalanceForPaymentDto
    {
        public AccountingBalanceForPaymentDto()
        {
            Workers = new List<BalanceByWorkerDto>();
        }

        public decimal Ndfl { get; set; }

        /// <summary>
        /// ФФОМС
        /// </summary>
        public decimal Ffoms { get; set; }

        /// <summary>
        /// ПФР
        /// </summary>
        public decimal Pfr { get; set; }

        /// <summary>
        /// ПФР накопительная
        /// </summary>
        public decimal PfrAccumulate { get; set; }

        /// <summary>
        /// ПФР страховая
        /// </summary>
        public decimal PfrInsurance { get; set; }

        /// <summary>
        /// ФСС нетрудоспособность
        /// </summary>
        public decimal FssDisability { get; set; }

        /// <summary>
        /// ФСС травматизм
        /// </summary>
        public decimal FssInjury { get; set; }

        public List<BalanceByWorkerDto> Workers { get; set; }
        
        /// <summary>
        /// ОПС, ОМС, ОСС по ВНиМ
        /// </summary>
        public decimal InsuranceFee { get; set; }
    }
}