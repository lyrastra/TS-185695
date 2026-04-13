using System;
using Moedelo.Common.AspNet.HostedServices.Extensions;
using NUnit.Framework;

namespace Moedelo.Common.AspNet.HostedServices.Tests;

[TestFixture]
public class StringExtensionsTests
{
    [Test]
    public void ValidateLeadershipLockIdWithValidIdShouldNotThrowException()
    {
        // Act & Assert
        Assert.DoesNotThrow(() => "valid-lock-id".ValidateLeadershipLockId());
    }

    [Test]
    public void ValidateLeadershipLockIdWithEmptyStringShouldThrowArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => string.Empty.ValidateLeadershipLockId());
        Assert.That(exception.Message, Does.Contain("не может быть пустой строкой или содержать только пробелы"));
    }

    [Test]
    public void ValidateLeadershipLockIdWithWhitespaceStringShouldThrowArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => "   ".ValidateLeadershipLockId());
        Assert.That(exception.Message, Does.Contain("не может быть пустой строкой или содержать только пробелы"));
    }

    [TestCase("<invalid")]
    [TestCase("invalid>")]
    [TestCase("in:valid")]
    [TestCase("in\"valid")]
    [TestCase("in|valid")]
    [TestCase("in?valid")]
    [TestCase("in*valid")]
    [TestCase("in\\valid")]
    [TestCase("in/valid")]
    public void ValidateLeadershipLockIdWithInvalidCharactersShouldThrowArgumentException(string invalidLockId)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => invalidLockId.ValidateLeadershipLockId());
        Assert.That(exception.Message, Does.Contain("содержит недопустимый символ"));
    }

    [Test]
    public void ValidateLeadershipLockIdWithControlCharactersShouldThrowArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => "in\0valid".ValidateLeadershipLockId());
        Assert.That(exception.Message, Does.Contain("не может содержать управляющие символы"));
    }

    [TestCase(".invalid")]
    [TestCase("invalid.")]
    [TestCase(" invalid")]
    [TestCase("invalid ")]
    public void ValidateLeadershipLockIdWithLeadingOrTrailingInvalidCharsShouldThrowArgumentException(string invalidLockId)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => invalidLockId.ValidateLeadershipLockId());
        Assert.That(exception.Message, Does.Contain("не может начинаться или заканчиваться точкой или пробелом"));
    }

    [Test]
    public void ValidateLeadershipLockIdWithTooLongStringShouldThrowArgumentException()
    {
        // Arrange
        var longLockId = new string('a', 513); // Больше максимальной длины 512

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => longLockId.ValidateLeadershipLockId());
        Assert.That(exception.Message, Does.Contain("слишком длинный"));
    }

    [TestCase("valid-lock-id")]
    [TestCase("valid_lock_id")]
    [TestCase("valid.lock.id")]
    [TestCase("ValidLockId")]
    [TestCase("valid123")]
    [TestCase("123valid")]
    public void ValidateLeadershipLockIdWithValidCharactersShouldNotThrowException(string validLockId)
    {
        // Act & Assert
        Assert.DoesNotThrow(() => validLockId.ValidateLeadershipLockId());
    }
} 