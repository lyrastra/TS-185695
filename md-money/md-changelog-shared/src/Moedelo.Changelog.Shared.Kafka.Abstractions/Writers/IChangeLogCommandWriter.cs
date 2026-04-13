using System.Threading.Tasks;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions.Writers
{
    public interface IChangeLogCommandWriter
    {
        Task WriteAsync(EntityStateSaveCommandFields commandFields);
        Task WriteAsync(ExplicitChangesSaveCommandFields commandFields);
    }
}
