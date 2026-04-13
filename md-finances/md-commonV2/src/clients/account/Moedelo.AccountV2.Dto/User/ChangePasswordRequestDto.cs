using System;

namespace Moedelo.AccountV2.Dto.User
{
    public class ChangePasswordRequestDto
    {
        public int UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        /// <summary>Устаналивается для простого обновления пароля без сравнения с текущим</summary>
        public bool IsWithoutChecking { get; set; }

        /// <summary>
        /// Информация о сессии, которую не надо удалять
        /// (обычно это сессия пользователя, который меняет пароль)
        /// </summary>
        public SessionInfo SessionToKeepAlive { get; set; }

        public class SessionInfo
        {
            public Guid Guid { get; set; }
            public int OAuthClientId { get; set; }
        }
    }
}