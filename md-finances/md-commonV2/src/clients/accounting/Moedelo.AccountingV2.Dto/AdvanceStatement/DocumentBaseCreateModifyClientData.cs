namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class DocumentBaseCreateModifyClientData
    {
        public long DocumentBaseId { get; set; }
        
        public string CreateDate { get; set; }
        
        public string CreateUser { get; set; }
        
        public string ModifyDate { get; set; }
        
        public string ModifyUser { get; set; }
    }
}