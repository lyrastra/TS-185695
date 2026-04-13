using System;

namespace Moedelo.HomeV2.Dto.BankRequest
{
    public class BankRequestResponseDto
    {
        public int RequestId { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Сюда кладётся ответ банка при неудачной отправке запроса
        /// ex.Message при сохранении заявки в таблицу
        /// </summary>
        public object AdditionalInfo { get; set; }

        public bool IsValid { get; set; }
    }
}