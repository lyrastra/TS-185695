using System;

namespace Moedelo.HomeV2.Dto.OnlineTv
{
    public class OnlineTvRegisteredPeopleResponseDto
    {
        /// <summary> GUID регистрации </summary>
        public Guid Guid { get; set; }

        /// <summary>Id события, на которое регистрируется участник</summary>
        public int EventId { get; set; }

        /// <summary> Id пользователя в системе для зарегистрированных </summary>
        public int UserId { get; set; }

        /// <summary>Имя, под которым зарегистрировался пользователь</summary>
        public string Name { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }
    }
}