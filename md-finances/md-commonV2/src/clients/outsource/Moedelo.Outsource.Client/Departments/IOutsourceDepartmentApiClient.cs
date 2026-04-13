using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Departments;

namespace Moedelo.Outsource.Client.Departments;

public interface IOutsourceDepartmentApiClient
{
    Task<int> InsertAsync(int firmId, int userId, DepartmentPostDto model);
}