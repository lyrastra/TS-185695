using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper
{
    public static class CertificateMinimalInfoMapper
    {
        public static List<CertificateMinimalInfoDto> Map(this List<CertificateMinimalInfoModel> models)
        {
            return models.Select(model => new CertificateMinimalInfoDto()
            {
                DnsName = model.DnsName,
                EmailName = model.EmailName,
                ExpirationDate = model.ExpirationDate,
                Issuer = model.Issuer,
                Name = model.Name,
                Number = model.Number,
                Subject = model.Subject,
                Thumbprint = model.Thumbprint
            }).ToList();
        }

        public static List<CertificateMinimalInfoModel> Map(this List<CertificateMinimalInfoDto> models)
        {
            return models.Select(model => new CertificateMinimalInfoModel()
            {
                DnsName = model.DnsName,
                EmailName = model.EmailName,
                ExpirationDate = model.ExpirationDate,
                Issuer = model.Issuer,
                Name = model.Name,
                Number = model.Number,
                Subject = model.Subject,
                Thumbprint = model.Thumbprint
            }).ToList();
        }

    }
}
