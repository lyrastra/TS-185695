using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.TemporaryPassword;

namespace Moedelo.HomeV2.Client.TemporaryPassword
{
    public interface ITemporaryPasswordApiClient
    {
        /// <summary>
        /// Использовать одновременный временный пароль
        /// </summary>
        /// <param name="passwordDto">данные по временному паролю</param>
        /// <returns>true - пароль принят и использован (больше не будет принят), false - пароль не принят</returns>
        Task<bool> UseTemporaryPasswordAsync(TemporaryPasswordDto passwordDto);
        Task<bool> CheckTemporaryPasswordAsync(TemporaryPasswordDto tempPassword);
        Task DestroyTemporaryPasswordAsync(TemporaryPasswordDto tempPassword);
        Task<TemporaryPasswordDto> GenerateTemporaryPasswordAsync(int userId);
    }
}