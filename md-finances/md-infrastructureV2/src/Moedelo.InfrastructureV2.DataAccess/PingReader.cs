using System.Threading.Tasks;
using Moedelo.InfrastructureV2.DataAccess.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.InfrastructureV2.DataAccess;

[InjectAsSingleton(typeof(IPingReader<>))]
internal class PingReader<TDbExecutor>(TDbExecutor dbExecutor) : IPingReader<TDbExecutor> where TDbExecutor : IDbExecutor
{
    private static readonly string PingQuery = @"select 'success';";

    public Task<string> PingAsync() => dbExecutor.FirstOrDefaultAsync<string>(new QueryObject(PingQuery));
}
