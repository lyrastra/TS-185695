using Moedelo.Common.Enums.Enums.EgrIp;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    public class EgrUlOkvedDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Код по Общероссийскому классификатору видов экономической деятельности
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование вида деятельности по Общероссийскому классификатору видов экономической деятельности
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Признак версии Общероссийского классификатора видов экономической деятельности
        /// </summary>
        public EgrUlOkvedVersion Version { get; set; }
    }
}