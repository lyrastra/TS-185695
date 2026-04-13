using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.BillNumberGenerate
{
    public interface IBillNumberGenerateClient : IDI
    {
        /// <summary>
        /// Получить теущий номер счета со смещением в виде строки
        /// </summary>
        /// <param name="index">Смещение</param>
        /// <returns></returns>
        Task<string> GetNextBillNumberAsync(int index);

        /// <summary>
        /// Сгенерировать новый номер счета в виде строки
        /// </summary>
        /// <returns></returns>
        Task<string> TakeBillNumberAsync();
    }
}