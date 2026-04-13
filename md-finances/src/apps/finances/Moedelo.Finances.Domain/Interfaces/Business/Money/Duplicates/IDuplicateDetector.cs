using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Duplicates
{
    public interface IDuplicateDetector : IDI
    {
        Task<DuplicateDetectionResult[]> DetectAsync(DuplicateDetectionRequest request);
    }
}