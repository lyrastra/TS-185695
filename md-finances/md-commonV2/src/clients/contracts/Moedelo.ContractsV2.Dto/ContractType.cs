using System.ComponentModel;

namespace Moedelo.ContractsV2.Dto
{
    public enum ContractType
    {
        Default = 0,

        /// <summary>
        /// Основной договор
        /// </summary>
        [Description("Основной договор")]
        Main = 1,
    }
}