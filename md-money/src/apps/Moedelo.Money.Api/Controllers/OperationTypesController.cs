using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OperationTypesController : ControllerBase
    {
        private readonly SettingValue newOperationsSetting;

        public OperationTypesController(
            ISettingRepository settingRepository)
        {
            newOperationsSetting = settingRepository.Get("NewBackendOperationTypes");
        }

        /// <summary>
        /// Список типов операций
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OperationTypeDescriptionDto[]>))]
        [SwaggerOperation(Tags = new[] { "Деньги" })]
        public IActionResult GetAsync()
        {
            var responseDto = OperationTypeMapper.Get();
            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Список типов операций для открытия нового бэкенда
        /// </summary>
        [HttpGet("NewBackend")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetForNewBackendAsync()
        {
            var newBackendOperations = newOperationsSetting.Value ?? string.Empty;
            if (string.IsNullOrWhiteSpace(newBackendOperations))
            {
                return new ApiDataResult(Array.Empty<OperationType>());
            }
            var operationTypes = newBackendOperations.Split(',')
                .Select(x => int.Parse(x))
                .Cast<OperationType>()
                .ToArray();
            return new ApiDataResult(operationTypes);
        }
    }
}
