using Moedelo.Outsource.Dto.Services.Enums;

namespace Moedelo.Outsource.Dto.Services
{
    public class ServicePostDto
    {
        public ProductType Product { get; set; }
        public string ProductConfigurationCode { get; set; }
        public string Name { get; set; }
        public int? GroupId { get; set; }
        public ServiceType ServiceType { get; set; }
        public bool IsActive { get; set; }
    }
}