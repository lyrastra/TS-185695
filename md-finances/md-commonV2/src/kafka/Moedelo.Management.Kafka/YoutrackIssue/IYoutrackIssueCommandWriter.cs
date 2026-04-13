using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Management.Kafka.YoutrackIssue.Models;

namespace Moedelo.Management.Kafka.YoutrackIssue
{
    public interface IYoutrackIssueCommandWriter : IDI
    {
        Task CreateIssueAsync(string key, string token, CreateIssueCommandData commandData);
    }
}