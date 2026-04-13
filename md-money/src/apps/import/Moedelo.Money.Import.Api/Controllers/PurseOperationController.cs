using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Money.Import.Domain.Interfaces;
using Moedelo.Money.Import.Domain.Models.PurseOperation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.PaymentOrderImport.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Moedelo.Money.Import.Api.Controllers
{
    /// <summary>
    /// Котроллер по обработке импорта в кошельках
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PurseOperationController : ControllerBase
    {
        private readonly IPurseOperationImportService purseOperationImportService;
        private readonly IDocumentTemplateService documentTemplateService;
        
        public PurseOperationController(
            IPurseOperationImportService purseOperationImportService, 
            IDocumentTemplateService documentTemplateService)
        {
            this.purseOperationImportService = purseOperationImportService;
            this.documentTemplateService = documentTemplateService;
        }

        /// <summary>
        /// Импорт операций по кошельку из файла Excel
        /// </summary>
        /// <param name="form"></param>
        /// <param name="kontragentId">id контрагента платежной системы</param>
        /// <param name="currentYear">Задает текущий год (поле для тестирования)</param>
        /// <returns></returns>
        [HttpPost("FromExcel")]
        public async Task<IActionResult> FromExcelAsync(IFormCollection form, [FromForm] int kontragentId, [FromForm] int? currentYear = null)
        {
            var responseStatus = new ImportStatus();

            var files = form?.Files;
            if (files?.Any() != true)
            {
                responseStatus.Status = PaymentImportResultStatus.WrongFile;
                responseStatus.ExData = new
                {
                    Errors = new[]
                    {
                        "Не удалось загрузить файл. Пожалуйста, убедитесь, что в файле есть данные для импорта"
                    }
                };
            }

            if (responseStatus.Status == PaymentImportResultStatus.WrongFile)
            {
                return new ApiDataResult(responseStatus);
            }
            
            await using var file = files[0].OpenReadStream();

            responseStatus = await purseOperationImportService.FromExcelAsync(new PurseOperationImportRequest
            {
                KontragentId = kontragentId,
                File = file,
                currentYear = currentYear
            });

            return new ApiDataResult(responseStatus);
        }

        /// <summary>
        /// Получение шаблона для импорта операций в формате Exsel
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDocumentTemplate")]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDocumentTemplateAsync(int? currentYear = null)
        {
            var file = await documentTemplateService.GetImportByPaymentSystems(currentYear);
           
            return File(file.Stream, file.ContentType, file.FileName);
        }
    }
}