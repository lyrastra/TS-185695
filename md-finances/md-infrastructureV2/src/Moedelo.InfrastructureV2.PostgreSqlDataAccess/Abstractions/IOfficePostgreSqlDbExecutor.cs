using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.PostgreSqlDataAccess;

public interface IOfficePostgreSqlDbExecutor : IDbExecutor, IDI
{

}