using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.Binders;

/// <summary>
/// Биндер, который позволяет десериализовать json-значение из элемента multipart-formdata запроса
/// Based on https://github.com/BrunoZell/JsonModelBinder
/// </summary>
/// <example>
/// <code><![CDATA[
/// [HttpPost("files/link")]
/// public async Task<IActionResult> LinkFileAsync(
///     [Required, FromForm(Name = "data"), ModelBinder(BinderType = typeof(JsonModelBinder))]
///     NewLinkedFileClaimsDto data,
///     IFormFile file)
/// {
///     // ...
///     return Ok();
/// }
/// ]]></code>
/// </example>
public class JsonModelBinder : IModelBinder
{
    private readonly MvcNewtonsoftJsonOptions options;

    public JsonModelBinder(IOptions<MvcNewtonsoftJsonOptions> options) =>
        this.options = options.Value;

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext is null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Test if a value is received
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        // Get the json serialized value as string
        var serialized = valueProviderResult.FirstValue;

        // Return a successful binding for empty strings or nulls
        if (string.IsNullOrEmpty(serialized)) {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        // Deserialize json string using custom json options defined in startup, if available
        var deserialized = options?.SerializerSettings is null ?
            JsonConvert.DeserializeObject(serialized, bindingContext.ModelType) :
            JsonConvert.DeserializeObject(serialized, bindingContext.ModelType, options.SerializerSettings);

        // Run data annotation validation to validate properties and fields on deserialized model
#pragma warning disable CS8604 // Possible null reference argument.
        var validationResultProps = from property in TypeDescriptor.GetProperties(deserialized).Cast<PropertyDescriptor>()
#pragma warning restore CS8604 // Possible null reference argument.
            from attribute in property.Attributes.OfType<ValidationAttribute>()
            where !attribute.IsValid(property.GetValue(deserialized))
            select new {
                Member = property.Name,
                ErrorMessage = attribute.FormatErrorMessage(string.Empty)
            };

        var validationResultFields = from field in TypeDescriptor.GetReflectionType(deserialized).GetFields().Cast<FieldInfo>()
            from attribute in field.GetCustomAttributes<ValidationAttribute>()
            where !attribute.IsValid(field.GetValue(deserialized))
            select new {
                Member = field.Name,
                ErrorMessage = attribute.FormatErrorMessage(string.Empty)
            };

        // Add the validation results to the model state
        var errors = validationResultFields.Concat(validationResultProps);
        foreach (var validationResultItem in errors)
            bindingContext.ModelState.AddModelError(validationResultItem.Member, validationResultItem.ErrorMessage);

        // Set successful binding result
        bindingContext.Result = ModelBindingResult.Success(deserialized);

        return Task.CompletedTask;
    }
}