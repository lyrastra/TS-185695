using Moedelo.PaymentOrderImport.Kafka.Abstractions.Rules.Commands;
using System.Threading.Tasks;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Rules
{
    public interface IRuleCommandWriter
    {
        Task WriteRuleCommandApplyIgnoreNumberAsync(ApplyIgnoreNumberCommand commandData);
    }
}
