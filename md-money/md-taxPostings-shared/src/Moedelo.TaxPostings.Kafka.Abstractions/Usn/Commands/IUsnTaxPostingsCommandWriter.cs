using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Usn.Commands
{
    public interface IUsnTaxPostingsCommandWriter
    {
        /// <summary>
        /// Добавляет налоговоые проводки
        /// note: используется как костыль, чтобы дописать проводки к связанным документам
        /// </summary>
        Task WriteAppendAsync(AppendUsnTaxPostings commandData);

        /// <summary>
        /// Перезаписывает налоговоые проводки
        /// </summary>
        Task WriteOverwriteAsync(OverwriteUsnTaxPostings commandData);

        /// <summary>
        /// Удаляет налоговоые проводки
        /// </summary>
        Task WriteDeleteAsync(DeleteUsnTaxPostings commandData);
    }
}
