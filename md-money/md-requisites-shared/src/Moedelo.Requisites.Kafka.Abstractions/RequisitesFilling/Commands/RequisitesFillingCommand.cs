using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling.Commands;

public class RequisitesFillingCommand : IEntityCommandData
{
    public int FirmId { get; set; }
    public int UserId { get; set; }
    public string Inn { get; set; }
}