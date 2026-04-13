using System;

namespace Moedelo.PaymentImport.Dto
{
    public class MovementFileInfoDto
    {
        public string Path { get; set; }
    
        public string Name { get; set; }
        
        public DateTime UploadDate { get; set; }
        
        public int Source { get; set; }
    }
}