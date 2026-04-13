using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.AutoWizardCompletion;

namespace Moedelo.RptV2.Client.AutoWizardCompletion
{
    public interface IAutoPayMailSendApiClient : IDI
    {
        /// <summary>Отправить письмо с результатами автоматического завершения мастера</summary>
        Task<bool> SendMailAsync(SendMailRequest mailRequest);
    }
}