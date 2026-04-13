using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Consultations.Dto.ConsultationUsage;

namespace Moedelo.Consultations.Client.ConsultationUsage
{
    public interface IConsultationUsageClient
    {
        Task<ConsultationUsageResponseDto> GetAsync(int userId, int firmId);
        Task<int> GetQuestionCountAsync(int userId, int firmId, QuestionCountRequestDto requestDto);
        Task<int> GetNotReadAnswersCountAsync(int userId, int firmId);

        /// <summary>
        /// Посчитать количество вопросов от пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserConsultationStatsDto> GetUserStatsAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает данные о пяти последних консультантах
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<UserConsultantDataDto>> GetLastUsersConsultantsDataAsync(int firmId,
            CancellationToken cancellationToken);
    }
}