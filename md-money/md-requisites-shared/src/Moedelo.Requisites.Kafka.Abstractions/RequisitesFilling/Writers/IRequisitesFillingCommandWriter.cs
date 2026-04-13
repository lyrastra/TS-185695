using System.Threading;
using System.Threading.Tasks;
using Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling.Commands;

namespace Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling.Writers;

public interface IRequisitesFillingCommandWriter
{
    Task WriteAsync(RequisitesFillingCommand commandData, CancellationToken cancellationToken);
}

