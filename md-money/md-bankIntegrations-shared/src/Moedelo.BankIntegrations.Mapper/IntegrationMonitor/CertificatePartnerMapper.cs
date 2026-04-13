using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class CertificatePartnerMapper
    {
        public static CertificatePartnerDto Map(this CertificatePartnerModel model)
        {
            return new CertificatePartnerDto()
            {
                DnsName = model.DnsName,
                EmailName = model.EmailName,
                ExpirationDate = model.ExpirationDate,
                Issuer = model.Issuer,
                Name = model.Name,
                Number = model.Number,
                Subject = model.Subject,
                Thumbprint = model.Thumbprint,
                UseCertificate = model.UseCertificate,
                IsExpires = model.IsExpires,
                PartnerId = model.PartnerId,
                PartnerName = model.PartnerName,
                ErrorComment = model.ErrorComment
            };
        }

        public static CertificatePartnerModel Map(this CertificatePartnerDto dto)
        {
            return new CertificatePartnerModel()
            {
                DnsName = dto.DnsName,
                EmailName = dto.EmailName,
                ExpirationDate = dto.ExpirationDate,
                Issuer = dto.Issuer,
                Name = dto.Name,
                Number = dto.Number,
                Subject = dto.Subject,
                Thumbprint = dto.Thumbprint,
                UseCertificate = dto.UseCertificate,
                IsExpires = dto.IsExpires,
                PartnerId = dto.PartnerId,
                PartnerName = dto.PartnerName,
                ErrorComment = dto.ErrorComment
            };
        }

        public static List<CertificatePartnerDto> Map(this List<CertificatePartnerModel> models)
        {
            return models.Select(model => model.Map()).ToList();
        }

        public static List<CertificatePartnerModel> Map(this List<CertificatePartnerDto> dtoList)
        {
            return dtoList.Select(dto => dto.Map()).ToList();
        }

    }
}
