using System;
using System.Collections.Generic;
using Jose;
using Moedelo.Common.Jwt.Abstractions;
using Moedelo.Common.Jwt.Abstractions.Exceptions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Common.Jwt.Crypto;
using Moedelo.Common.Jwt.Extensions;

namespace Moedelo.Common.Jwt;

/// <summary>
/// Реализация сервиса для работы с JWT токенами с поддержкой RSA256 подписи.
/// Использует библиотеку Jose-JWT и сертификаты X.509 из хранилища Windows.
/// Настраивается через конфигурацию: CheckJwtSign (вкл/выкл проверки подписи) и IdX509SerialNumber (серийные номера сертификатов).
/// </summary>
[InjectAsSingleton(typeof(IJwtService))]
internal sealed class JwtService : IJwtService
{
    private const string JwtAlgName = "alg";
    private const JwsAlgorithm JwtSignAlgorithm = JwsAlgorithm.RS256;

    private const string PrivateJwtHeader = "IsPrivate";

    private readonly SettingValue signJwtSetting;
    private readonly IPrivateKeyRepository keyRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="JwtService"/>.
    /// </summary>
    /// <param name="settingRepository">Репозиторий настроек для получения конфигурации JWT (CheckJwtSign, IdX509SerialNumber).</param>
    /// <param name="keyRepository">Репозиторий приватных ключей для подписи и верификации токенов.</param>
    public JwtService(
        ISettingRepository settingRepository,
        IPrivateKeyRepository keyRepository)
    {
        this.keyRepository = keyRepository;
        signJwtSetting = settingRepository.GetSignJwt();
    }

    /// <inheritdoc />
    public T Decode<T>(string token)
        where T : class
    {
        try
        {
            var claims = DecodeInternal<T>(token);
            if (claims == default)
            {
                throw new Exception("Unexpected empty claims");
            }

            return claims;
        }
        catch (Exception ex)
        {
            throw new JwtException(ex);
        }
    }

    private T DecodeInternal<T>(string token)
    {
        var headers = JWT.Headers(token);
        Enum.TryParse<JwsAlgorithm>(headers[JwtAlgName].ToString(), out var signAlgorithm);
        var useSign = signJwtSetting.GetBoolValueOrDefault(false);
            
        // no reason to load certificates
        if (useSign == false)
        {
            // no check sign
            return JWT.Payload<T>(token);
        }

        if (signAlgorithm == JwsAlgorithm.none)
        {
            throw new Exception("Unexpected none sign algorithm");
        }

        if (signAlgorithm != JwtSignAlgorithm)
        {
            throw new Exception("Unsupported sign algorithm");
        }

        var privateKey = keyRepository.PrivateKey ?? throw new Exception("No one private key found");

        return JWT.Decode<T>(token, privateKey, JwtSignAlgorithm);
    }

    /// <inheritdoc />
    public string Encode(object payload, IDictionary<string, object> headers = null)
    {
        var useSign = signJwtSetting.GetBoolValueOrDefault(false);

        if (useSign == false)
        {
            return JWT.Encode(payload, null, JwsAlgorithm.none, headers);
        }

        var privateKey = keyRepository.PrivateKey ?? throw new Exception("No one private key found");

        return JWT.Encode(payload, privateKey, JwtSignAlgorithm, headers);
    }

    /// <inheritdoc />
    public IDictionary<string, object> Headers(string token)
    {
        return JWT.Headers(token);
    }

    /// <inheritdoc />
    public bool IsPrivate(string token)
    {
        var headers = Headers(token);

        return headers.ContainsKey(PrivateJwtHeader);
    }
}