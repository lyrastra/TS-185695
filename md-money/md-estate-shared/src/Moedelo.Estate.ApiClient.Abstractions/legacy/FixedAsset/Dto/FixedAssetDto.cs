using Moedelo.AccPostings.Enums;

namespace Moedelo.Estate.ApiClient.Abstractions.legacy.FixedAsset.Dto
{
    public class FixedAssetDto
    {
        public long Id { get; set; }
        public long DocumentBaseId { get; set; }
        public long? SubcontoId { get; set; }
        public string SubcontoName { get; set; }
        public SubcontoType SubcontoType { get; set; }
    }
}
