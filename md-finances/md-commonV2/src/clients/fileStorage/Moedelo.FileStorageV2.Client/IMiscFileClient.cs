using System;
using System.Threading.Tasks;
using Moedelo.FileStorageV2.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.FileStorageV2.Client
{
    public interface IMiscFileClient : IDI
    {
        [Obsolete("Этот метод ничего не делает (функционал удалён). Удалите использование метода")]
        Task UploadAsync(int firmId, int userId, MiscFileDto file);
    }
}