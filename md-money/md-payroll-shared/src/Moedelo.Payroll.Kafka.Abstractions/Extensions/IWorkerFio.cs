namespace Moedelo.Payroll.Kafka.Abstractions.Extensions
{
    public interface IWorkerFio
    {
        string Surname { get; }
        string Name { get; }
        string Patronymic { get; }
    }
}