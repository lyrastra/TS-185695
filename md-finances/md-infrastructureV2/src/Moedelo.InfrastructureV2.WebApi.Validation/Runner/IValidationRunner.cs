using System;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Runner
{
    public interface IValidationRunner : IDI
    {
        /// <summary>
        ///     Запустить указанную процедуру валидации
        /// </summary>
        /// <param name="action">
        ///     Процедура валидации. Принимает параметр типа ModelStateDictionary,
        ///     в который должна помещать ошибки с помощью метода AddModelError
        /// </param>
        void Validate(Action<ModelStateDictionary> action);

        /// <summary>
        ///     Запустить указанную процедуру валидации
        /// </summary>
        /// <param name="action">
        ///     Процедура валидации. Принимает параметр типа ModelStateDictionary,
        ///     в который должна помещать ошибки с помощью метода AddModelError
        /// </param>
        Task ValidateAsync(Func<ModelStateDictionary, Task> action);
    }
}