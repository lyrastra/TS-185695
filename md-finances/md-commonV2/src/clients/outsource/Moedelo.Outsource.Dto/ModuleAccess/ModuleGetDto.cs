using System.Collections.Generic;

namespace Moedelo.Outsource.Dto.ModuleAccess
{
    /// <summary>
    /// Фильтр подключенных модулей
    /// </summary>
    public class ModuleGetDto
    {
        public IReadOnlyCollection<int> AccountIds { get; set; }
        public ModuleType? Type { get; set; }
    }
}