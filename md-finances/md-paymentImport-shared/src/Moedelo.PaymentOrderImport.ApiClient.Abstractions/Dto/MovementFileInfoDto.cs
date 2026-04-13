using System;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class MovementFileInfoDto
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public MovementSource Source { get; set; }
    }
}