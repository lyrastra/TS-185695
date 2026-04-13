using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Models;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel
{
    public class EgrUlInfoDto
    {
        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public string Kpp { get; set; }

        /// <summary>
        /// Дата формирования сведений из ЕГРЮЛ в отношении юридического лица
        /// </summary>
        public DateTime ExtractDate { get; set; }

        /// <summary>
        /// Дата присвоения ОГРН
        /// </summary>
        public DateTime OgrnDate { get; set; }

        /// <summary>
        /// Код по выбранному классификатору
        /// </summary>
        public string CodeOpf { get; set; }

        /// <summary>
        /// Полное наименование организационно-правовой формы
        /// </summary>
        public string OpfFullName { get; set; }

        /// <summary>
        /// Код по выбранному классификатору
        /// </summary>
        public EgrUlOpfType OpfType { get; set; }

        public bool OpfTypeSpecified { get; set; }

        /// <summary>
        /// Сведения о наименовании юридического лица
        /// </summary>
        public EgrUlNameInfoDto EgrUlName { get; set; }

        /// <summary>
        /// Сведения об адресе (месте нахождения)
        /// </summary>
        public EgrUlAddressInfoDto Address { get; set; }

        /// <summary>
        /// Сведения об адресе электронной почты юридического лица
        /// </summary>
        public EgrUlEmailInfoDto Email { get; set; }

        /// <summary>
        /// Сведения о регистрации (образовании) юридического лица
        /// </summary>
        public EgrUlRegInfoDto RegInfo { get; set; }

        /// <summary>
        /// Сведения о регистрирующем органе по месту нахождения юридического лица
        /// </summary>
        public EgrUlRegistrationAgencyInfoDto RegistrationAgency { get; set; }

        /// <summary>
        /// Сведения о состоянии (статусе) юридического лица
        /// </summary>
        public List<EgrUlStatusInfoDto> UlStatus { get; set; }

        /// <summary>
        /// Сведения о прекращении юридического лица
        /// </summary>
        public EgrUlTerminationInfoDto TerminationInfo { get; set; }

        /// <summary>
        /// Сведения об учете в налоговом органе
        /// </summary>
        public EgrUlTaxRegistrationInfoDto TaxRegistration { get; set; }

        /// <summary>
        /// Сведения о регистрации юридического лица в качестве страхователя в территориальном органе Пенсионного фонда Российской Федерации
        /// </summary>
        public EgrUlPFRegistrationInfoDto PFRegistration { get; set; }

        /// <summary>
        /// Сведения о регистрации юридического лица в качестве страхователя в исполнительном органе Фонда социального страхования Российской Федерации
        /// </summary>
        public EgrUlFssRegistrationInfoDto FssRegistration { get; set; }

        /// <summary>
        /// Сведения о размере указанного в учредительных документах коммерческой организации уставного капитала (складочного капитала, уставного фонда, паевого фонда)
        /// </summary>
        public EgrUlCapitalInfoDto Capital { get; set; }

        /// <summary>
        /// Сведения об управляющей организации
        /// </summary>
        public List<EgrUlManagementCompanyInfoDto> ManagementCompany { get; set; }

        /// <summary>
        /// Сведения о лице, имеющем право без доверенности действовать от имени юридического лица
        /// </summary>
        public List<EgrUlAuthorizedFlInfoDto> AuthorizedFl { get; set; }

        /// <summary>
        /// Сведения о лице, имеющем право без доверенности действовать от имени юридического лица
        /// </summary>
        public EgrUlFoundersInfoDto Founders { get; set; }

        /// <summary>
        /// Сведения о доле в уставном капитале общества с ограниченной ответственностью, принадлежащей обществу
        /// </summary>
        public EgrUlCapitalShareInfoDto CapitalShareOOO { get; set; }

        /// <summary>
        /// Сведения о держателе реестра акционеров акционерного общества
        /// </summary>
        public EgrUlRegistrarAOInfoDto RegistrarAO { get; set; }

        /// <summary>
        /// Сведения о видах экономической деятельности по Общероссийскому классификатору видов экономической деятельности
        /// </summary>
        public EgrUlOkvedInfoDto Okved { get; set; }

        /// <summary>
        /// Сведения о лицензиях, выданных ЮЛ
        /// </summary>
        public List<EgrUlLicenseInfoDto> License { get; set; }

        /// <summary>
        /// Сведения об обособленных подразделениях юридического лица
        /// </summary>
        public EgrUlDivisionsInfoDto Divisions { get; set; }
        
        /// <summary>
        /// Сведения об участии в реорганизации
        /// </summary>
        public List<EgrUlReorgInfoDto> Reorganization { get; set; }
        
        /// <summary>
        /// Сведения о правопредшественнике
        /// </summary>
        public List<EgrUlPredecessorInfoDto> Predecessor { get; set; }
        
        /// <summary>
        /// Сведения о крестьянском (фермерском) хозяйстве, на базе имущества которого создано юридическое лицо
        /// </summary>
        public List<EgrUlPFEPredecessorInfoDto> PFEPredecessor { get; set; }
        
        /// <summary>
        /// Сведения о правопреемнике
        /// </summary>
        public List<EgrUlAssigneeInfoDto> Assignee { get; set; }
        
        /// <summary>
        /// Сведения о крестьянском (фермерском) хозяйстве, которые  внесены в ЕГРИП в связи с приведением правового статуса крестьянского (фермерского) хозяйства в соответствие с нормами части первой Гражданского кодекса Российской Федерации
        /// </summary>
        public EgrUlPFEAssigneeInfoDto PFEAssignee { get; set; }
    }
}