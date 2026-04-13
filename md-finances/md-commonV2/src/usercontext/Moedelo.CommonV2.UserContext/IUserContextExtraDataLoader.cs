using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;

namespace Moedelo.CommonV2.UserContext;

public interface IUserContextExtraDataLoader
{
    Task<IUserContextExtraData> GetAsync(int firmId, int userId, int roleId);
    IUserContextExtraData GetUnauthorized();
}