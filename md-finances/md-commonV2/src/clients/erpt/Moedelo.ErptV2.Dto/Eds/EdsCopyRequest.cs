namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsCopyRequest
    {
         /// <summary>Фирма с которой поисходит копирование</summary>
        public int FromFirmId { get; set; }
        /// <summary>Фирма на которою поисходит копирование</summary>
        public int ToFirmId { get; set; }
        /// <summary>Нужно ли удалять подпись с фирмы, с которой происходит копирование?</summary>
        public bool IsClearSource { get; set; }
        /// <summary>Логин оператора который производит копирование</summary>
        public string OperatorLogin { get; set; }
    }
}