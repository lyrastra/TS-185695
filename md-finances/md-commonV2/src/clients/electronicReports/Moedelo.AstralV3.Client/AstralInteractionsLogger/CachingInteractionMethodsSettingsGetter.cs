using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.Interfaces;
using Moedelo.AstralV3.Client.DAO.DbObjects;
using Moedelo.AstralV3.Client.DAO.Interfaces;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger
{
    /// <summary>
    /// Кеширующий получатель настроек логирования. Получает настройки логирования по именам методов, создаёт недостающие настройки,
    /// кеширует результаты
    /// </summary>
    [InjectAsSingleton]
    public class CachingInteractionMethodsSettingsRepository : IInteractionMethodsSettingsRepository
    {
        /// <summary>
        /// Срок жизни кеша в секундах
        /// </summary>
        private readonly TimeSpan CacheLifetime = TimeSpan.FromSeconds(120);

        /// <summary>
        /// Этот режим логирования используется для новых методов (например, написанных разработчиком)
        /// </summary>
        private const MethodLoggingMode DefaultLoggingMode = MethodLoggingMode.LogAll;


        /// <summary>
        /// Время последнего обновления кеша (в UTC)
        /// </summary>
        private DateTime _lastCacheUpdate = new DateTime(1900, 1, 1);

        /// <summary>
        /// Кеш
        /// </summary>
        private Dictionary<string, AstralInteractionMethod> _cache = new Dictionary<string, AstralInteractionMethod>();

        private readonly IAstralInteractionMethodsDao _interactionMethodsDao;

        public CachingInteractionMethodsSettingsRepository(IAstralInteractionMethodsDao interactionMethodsDao)
        {
            _interactionMethodsDao = interactionMethodsDao;
        }

        public async Task<Dictionary<string, AstralInteractionMethod>> GetSettings(List<string> methodNames)
        {
            // Стар-ли кеш?
            if ((DateTime.UtcNow - _lastCacheUpdate) <= CacheLifetime)
            {
                // Отдаём кешированное
                return _cache;
            }

            // Сбрасываем кеш
            _cache.Clear();
            _lastCacheUpdate = DateTime.UtcNow;

            // Удаляем повторяющиеся имена
            var uniqueNames = methodNames
                .Distinct()
                .ToList();

            // Получаем существующие настройки по именам и добавляем их в кеш
            var existingSettings = await _interactionMethodsDao.Get(uniqueNames).ConfigureAwait(false);
            foreach (var existingSetting in existingSettings)
            {
                _cache.Add(existingSetting.MethodName, existingSetting);
            }

            // Получаем список несуществующих настроек
            var existingNames = _cache
                .Select(ce => ce.Key)
                .ToList();

            // И несуществующих
            var nonExistingNames = uniqueNames
                .Except(existingNames);

            // Добавляем настройки для несуществующих
            var addedSettingsIds = new List<int>();
            foreach (var nonExistingName in nonExistingNames)
            {
                addedSettingsIds.Add(await _interactionMethodsDao.Add(nonExistingName, DefaultLoggingMode).ConfigureAwait(false));
            }

            var addedSettings = await _interactionMethodsDao.Get(addedSettingsIds).ConfigureAwait(false);
            foreach (var addedSetting in addedSettings)
            {
                _cache.Add(addedSetting.MethodName, addedSetting);
            }

            return _cache;
        }
    }
}
