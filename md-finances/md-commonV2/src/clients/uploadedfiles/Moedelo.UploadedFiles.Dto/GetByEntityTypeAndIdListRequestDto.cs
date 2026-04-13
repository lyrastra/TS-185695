using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Uploaded;

namespace Moedelo.UploadedFiles.Dto
{
    public class GetByEntityTypeAndIdListRequestDto
    {
        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Список идентификаторов сущностей
        /// </summary>
        public IReadOnlyCollection<long> EntityIdList { get; set; }
    }
}