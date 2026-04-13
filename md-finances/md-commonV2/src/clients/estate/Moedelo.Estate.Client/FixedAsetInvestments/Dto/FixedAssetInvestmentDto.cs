using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Estate;

namespace Moedelo.Estate.Client.FixedAsetInvestments.Dto
{
    public class FixedAssetInvestmentDto
    {
        public long Id { get; set; }
        
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// id субконто в общем справочнике субконто
        /// </summary>
        public long SubcontoId { get; set; }

        /// <summary>
        /// Имя, которое будет отображаться в аналитическом учете
        /// </summary>
        public SubcontoType SubcontoType { get; set; }

        public string SubcontoName { get; set; }

        public FixedAssetInvestmentType InvestmentType { get; set; }
    }
}