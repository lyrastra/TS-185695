namespace Moedelo.SuiteCrm.Dto.LeadInfo
{
    public class UpdateRequisitiesDto
    {
        public int FirmId { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Ogrn { get; set; }

        /// <summary> Юридическое наименование </summary>
        public string UrName { get; set; }

        /// <summary> Юридический адрес </summary>
        public string UrAddress { get; set; }
    }
}