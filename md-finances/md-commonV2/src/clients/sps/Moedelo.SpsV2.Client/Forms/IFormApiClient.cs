using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.Forms;

namespace Moedelo.SpsV2.Client.Forms
{
    public interface IFormApiClient
    {
        Task<FormWidgetResponseDto> GetDataForWidget(FormWidgetRequestDto request, int userId = 0);
    }
}