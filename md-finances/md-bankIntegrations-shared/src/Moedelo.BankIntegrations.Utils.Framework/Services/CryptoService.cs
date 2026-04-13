using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.Utils.Framework.Services
{
    [InjectAsSingleton(typeof(ICryptoService))]
    public class CryptoService : ICryptoService
    {
        private readonly SettingValue password;
        private readonly SettingValue decryptWord;

        public CryptoService(ISettingRepository settingRepository)
        {
            this.decryptWord = settingRepository.Get("IntegrationCryptoDecryptWord");
            this.password = settingRepository.Get("IntegrationCryptoPassword");
        }

        public string EncryptText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            if (text.StartsWith(decryptWord.Value))
            {
                return text;
            }

            return $"{decryptWord.Value}{Convert.ToBase64String(EncryptData(Encoding.UTF8.GetBytes(text)))}";
        }

        public string DecryptText(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || !text.StartsWith(decryptWord.Value))
            {
                return text;
            }
            
            text = text.Replace(decryptWord.Value, "");
            
            return Encoding.UTF8.GetString(DecryptData(Convert.FromBase64String(text)));
        }
        
        /// <summary>
        /// https://learn.microsoft.com/ru-ru/dotnet/api/system.security.cryptography.rfc2898derivebytes?view=net-6.0
        /// Создаем объект Advanced Encryption Standard, используя стандартный алгоритм шифрования AES (Advanced Encryption Standard) с режимом шифрования CBC (Cipher Block Chaining).
        /// У AES режим по умолчанию CBC (Cipher Block Chaining) — каждый следующий блок открытого текста перед зашифровыванием складывается побитово по модулю 2 спредыдущим блоком зашифрованного текста.
        /// CBC длина ключа для AES составляет 256 бит (32 байта), а длина IV — 128 бит (16 байт).
        /// Инициализируем Rfc2898DeriveBytes, используя заданный пароль, случайные данные, число итераций и имя хэш-алгоритма для формирования ключа.
        /// Задаем ключ шифрования, задаем Initial Vector и записываем Initial Vector в поток.
        /// Создаем объект CryptoStream, используя объект Aes, поток для записи и режим шифрования.
        /// </summary>
        /// <param name="data">данные для шифрования</param>
        /// <returns>зашифрованные данные</returns>
        private byte[] EncryptData(byte[] data)
        {
            using (var encAlg = Aes.Create())
            {
                {
                    using (var hasher = new Rfc2898DeriveBytes(
                               password.Value,
                               encAlg.IV, 1000,
                               HashAlgorithmName.SHA256))
                    {
                        encAlg.Key = hasher.GetBytes(32);
                        using (var ms = new MemoryStream())
                        {
                            ms.Write(encAlg.IV, 0, encAlg.IV.Length);
                            using (var cs = new CryptoStream(ms, encAlg.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(data, 0, data.Length);
                            }

                            return ms.ToArray();
                        }
                    }
                }
            }
        }
        
        private byte[] DecryptData(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var iv = new byte[16];
                ms.Read(iv, 0, iv.Length);
                using (var encAlg = Aes.Create())
                {
                    using (var hasher = new Rfc2898DeriveBytes(password.Value, iv, 1000, HashAlgorithmName.SHA256))
                    {
                        encAlg.Key = hasher.GetBytes(32);
                        encAlg.IV = iv;
                        using (var cs = new CryptoStream(ms, encAlg.CreateDecryptor(), CryptoStreamMode.Read, true))
                        {
                            using (var output = new MemoryStream())
                            {
                                cs.CopyTo(output);
                                return output.ToArray();
                            }
                        }
                    }
                }
            }
        }
    }
}