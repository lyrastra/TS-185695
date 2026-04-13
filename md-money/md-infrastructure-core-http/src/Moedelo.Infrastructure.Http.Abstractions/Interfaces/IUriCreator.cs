#nullable enable
using System.Collections.Generic;

namespace Moedelo.Infrastructure.Http.Abstractions.Interfaces
{
    /// <summary>
    /// Сервис построения абсолютных URI-строк. Предназначен для удобного формирования адресов
    /// HTTP/HTTPS-запросов из базового хоста, относительного пути и различных вариантов
    /// представления query-параметров. Реализует правила:
    /// <list type="bullet">
    /// <item>Если в <paramref name="host"/> отсутствует явный порт, то добавляется порт по умолчанию<br/>
    /// (80 для http, 443 для https).</item>
    /// <item>Все имя/значения query-параметров URL-кодируются.</item>
    /// <item>DateTime преобразуется в формат ISO 8601 (UTC, суффикс «Z»).</item>
    /// <item>Массивы примитивов сериализуются как несколько одноимённых параметров.<br/>Пример: ids=[1,2] → ?ids=1&amp;ids=2</item>
    /// <item>Сложные объекты разворачиваются в «плоскую» структуру с точечной нотацией.<br/>Пример: user.{Name="Ann"} → ?user.Name=Ann</item>
    /// </list>
    /// </summary>
    public interface IUriCreator
    {
        /// <summary>
        /// Формирует URI из хоста, относительного пути и уже подготовленной строки query.
        /// <para>Используйте, если у вас уже есть полная строка параметров (без ведущего «?»).</para>
        /// </summary>
        /// <param name="host">Базовый адрес: протокол + домен, может включать порт.</param>
        /// <param name="path">Относительный путь (может начинаться с «/»).</param>
        /// <param name="query">Строка query без символа «?»; может быть <see langword="null"/> или пустой.</param>
        /// <returns>Полностью сформированная, URL-кодированная строка.</returns>
        string Create(
            string host, 
            string? path, 
            string? query);
        
        /// <summary>
        /// Формирует URI из хоста и пути, самостоятельно извлекая (при наличии) часть после «?»
        /// из <paramref name="path"/>. Удобно использовать, когда путь уже содержит query.
        /// </summary>
        /// <param name="host">Базовый адрес (протокол, домен, порт).</param>
        /// <param name="path">Путь, опционально включающий query-часть.</param>
        /// <returns>Готовая строка URI.</returns>
        string Create(
            string host, 
            string? path);

        /// <summary>
        /// Формирует URI, принимая query как коллекцию пар «ключ-значение».
        /// </summary>
        /// <param name="host">Базовый адрес.</param>
        /// <param name="path">Относительный путь.</param>
        /// <param name="queryParams">Коллекция параметров; <see langword="null"/>/пустая коллекция приведут к URI без query.</param>
        /// <returns>Полная строка URI.</returns>
        string Create(
            string host, 
            string path, 
            IReadOnlyCollection<KeyValuePair<string, object>>? queryParams);
        
        /// <summary>
        /// Формирует URI, принимая произвольный объект с публичными свойствами как источник
        /// query-параметров. Каждый публичный невиртуальный property становится параметром.
        /// </summary>
        /// <param name="host">Базовый адрес.</param>
        /// <param name="path">Относительный путь.</param>
        /// <param name="queryParams">Объект со свойствами; может быть анонимным. Если объект реализует
        /// <see cref="System.Collections.IEnumerable"/> и не является коллекцией <c>KeyValuePair&lt;string, …&gt;</c>,
        /// будет выброшено <see cref="System.ArgumentException"/>.
        /// </param>
        /// <returns>Полная строка URI.</returns>
        string Create(
            string host, 
            string path, 
            object? queryParams);
    }
}