using System;

namespace Moedelo.CommonV2.EventBus.Backoffice;

/// <summary>
/// Команда для приземления оператора
/// </summary>
public class OperatorAssignmentCommand
{
    public Guid RequestGuid { get; set; }
    public string OperatorGroupName { get; set; }
    public int FirmId { get; set; }
    public OperatorGroupTypeEnum OperatorGroupType { get; set; }

    public enum OperatorGroupTypeEnum
    {
        Unknown = 0,
        Training = 1,
        Support = 2
    }
}