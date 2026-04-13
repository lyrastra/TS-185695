using Moedelo.Common.Enums.Enums.ElectronicReports;
using System.Collections.Generic;

namespace Moedelo.ErptV2.Client.EdsApi
{
    /// <summary>
    /// Модель для запроса на изменение кодов исходящих направлений внутри заявки на КЭП
    /// </summary>
    public class ChangeCodeOfOutgoingDirectionRequest
    {
        /// <summary>
        /// Тип операции
        /// </summary>
        public UpdateFundCodesOperation OperationType { get; set; }

        /// <summary>
        /// Список фирм, если изменть нужно конкретным фирмам
        /// </summary>
        public List<int> FirmIds { get; set; }

        /// <summary>
        /// Исходящее направление для котого производится замена кода
        /// </summary>
        public OutgoingDirectionType OutgoingDirectionType { get; set; }

        /// <summary>
        /// Текущий код
        /// </summary>
        public string SourceCode      { get; set; }

        /// <summary>
        /// Новый код
        /// </summary>
        public string DestinationCode { get; set; }
    }
}
