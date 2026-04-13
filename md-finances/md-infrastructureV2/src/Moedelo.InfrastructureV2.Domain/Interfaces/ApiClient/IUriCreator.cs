using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;

public interface IUriCreator
{
    string Create(string host, string path);

    string Create(string host, string path, object queryParams);

    string Create(string host, string path, IEnumerable<KeyValuePair<string, object>> queryParams);

    string Create(string host, string path, string query);
}