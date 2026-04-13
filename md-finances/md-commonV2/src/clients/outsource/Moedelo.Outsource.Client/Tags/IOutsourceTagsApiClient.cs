using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Tags;

namespace Moedelo.Outsource.Client.Tags;

public interface IOutsourceTagsApiClient
{
    Task<IReadOnlyList<TagDto>> GetListAsync(int accountId);
}