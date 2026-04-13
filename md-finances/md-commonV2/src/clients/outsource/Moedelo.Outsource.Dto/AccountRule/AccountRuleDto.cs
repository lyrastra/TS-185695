using System;

namespace Moedelo.Outsource.Dto.AccountRule
{
    public class AccountRuleDto
    {
       
        public int Id { get; set; }
        
        public int AccountId { get; set; }
        
        public int[] EditableRules { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public DateTime ModifyDate { get; set; }
    }
}