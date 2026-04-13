using System;
using System.Linq;

namespace Moedelo.Common.AspNet.HostedServices.Extensions;

/// <summary>
/// Extension методы для валидации строк
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Валидирует LeadershipLockId на соответствие ограничениям Consul для имен ключей.
    /// Consul имеет следующие ограничения на имена ключей:
    /// - Не может быть пустой строкой
    /// - Не может содержать символы: &lt; &gt; : &quot; | ? * \ / 
    /// - Не может начинаться или заканчиваться точкой или пробелом
    /// - Не может содержать управляющие символы (ASCII 0-31)
    /// - Рекомендуется использовать только буквы, цифры, дефисы, подчеркивания и точки
    /// </summary>
    /// <param name="leadershipLockId">Идентификатор блокировки лидерства для валидации</param>
    /// <exception cref="ArgumentException">Выбрасывается, если LeadershipLockId не соответствует ограничениям Consul</exception>
    internal static void ValidateLeadershipLockId(this string leadershipLockId)
    {
        if (string.IsNullOrWhiteSpace(leadershipLockId))
        {
            throw new ArgumentException("LeadershipLockId не может быть пустой строкой или содержать только пробелы", nameof(leadershipLockId));
        }

        // Проверка на недопустимые символы в именах ключей Consul
        var invalidChars = new[] { '<', '>', ':', '"', '|', '?', '*', '\\', '/' };
        var invalidCharIndex = leadershipLockId.IndexOfAny(invalidChars);
        if (invalidCharIndex >= 0)
        {
            throw new ArgumentException($"LeadershipLockId содержит недопустимый символ '{leadershipLockId[invalidCharIndex]}' в позиции {invalidCharIndex}. Недопустимые символы: < > : \" | ? * \\ /", nameof(leadershipLockId));
        }

        // Проверка на управляющие символы (ASCII 0-31)
        if (leadershipLockId.Any(c => c < 32))
        {
            throw new ArgumentException("LeadershipLockId не может содержать управляющие символы (ASCII 0-31)", nameof(leadershipLockId));
        }

        // Проверка на начало или конец точкой или пробелом
        if (leadershipLockId.StartsWith('.') || leadershipLockId.EndsWith('.') ||
            leadershipLockId.StartsWith(' ') || leadershipLockId.EndsWith(' '))
        {
            throw new ArgumentException("LeadershipLockId не может начинаться или заканчиваться точкой или пробелом", nameof(leadershipLockId));
        }

        // Проверка на максимальную длину (Consul имеет ограничения на длину ключей)
        const int maxKeyLength = 512; // Консервативное ограничение для Consul
        if (leadershipLockId.Length > maxKeyLength)
        {
            throw new ArgumentException($"LeadershipLockId слишком длинный. Максимальная длина: {maxKeyLength} символов", nameof(leadershipLockId));
        }
    }
} 