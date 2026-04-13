using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Comments;

namespace Moedelo.Outsource.Client.Comments;

public interface IOutsourceCommentsApiClient
{
    /// <summary>
    /// Список коментариев
    /// </summary>
    Task<IReadOnlyList<CommentDto>> GetListAsync(int accountId, int groupId, int tagId);
}