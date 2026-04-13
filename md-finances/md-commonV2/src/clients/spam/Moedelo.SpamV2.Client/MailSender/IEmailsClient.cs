using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SpamV2.Dto.MailSender.EmailTable;

namespace Moedelo.SpamV2.Client.MailSender
{
    public interface IEmailsClient : IDI
    {
        Task<FirmEmailDto> GetByIdAsync(int id);

        Task<int> SaveAsync(FirmEmailDto request);
    }
}
