using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.Extensions;

/// <summary>
/// Расширения для <see cref="ControllerBase"/>, помогающие формировать стандартные ответы.
/// </summary>
// ReSharper disable once UnusedType.Global
public static class ControllerBaseExtensions
{
    /// <summary>
    /// Формирует ответ с прикрепленным файлом
    /// </summary>
    /// <param name="controller">Контроллер</param>
    /// <param name="content">Файл в виде массива байтов</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип файла (mime-type)</param>
    /// <returns>FileContentResult</returns>
    /// <exception cref="ArgumentException">Если передается пустой контент или название файла</exception>
    // ReSharper disable once UnusedMember.Global
    public static IActionResult GetFileContentResult(
        this ControllerBase controller,
        byte[] content,
        string fileName,
        string contentType)
    {
        if (content == null || content.Length == 0)
        {
            throw new ArgumentException("Empty file content is not allowed",  nameof(content));
        }
            
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("Empty fileName is not allowed",  nameof(fileName));
        }

        return controller.File(content, contentType, fileName);
    }
}