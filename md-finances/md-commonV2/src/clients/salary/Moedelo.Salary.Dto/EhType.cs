using System.ComponentModel;

namespace Moedelo.Salary.Dto
{
    public enum EhType : byte
    {
        [Description("Нет заявления")]
        HasNotStatement = 0,
        [Description("В бумажном виде")]
        Paper,
        [Description("В электронном виде")]
        Electronic
    }
}