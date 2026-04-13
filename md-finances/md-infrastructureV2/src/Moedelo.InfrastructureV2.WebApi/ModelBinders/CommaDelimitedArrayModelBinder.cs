using System;
using System.ComponentModel;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Moedelo.InfrastructureV2.WebApi.ModelBinders
{
    public class CommaDelimitedArrayModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(key);
            if (string.IsNullOrWhiteSpace(value?.AttemptedValue))
            {
                return false;
            }
            var elementType = bindingContext.ModelType.GetElementType();
            var converter = TypeDescriptor.GetConverter(elementType);
            var strArray = value.AttemptedValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var objArray = Array.ConvertAll(strArray, x => converter.ConvertFromString(x?.Trim()));
            var array = Array.CreateInstance(elementType, objArray.Length);
            objArray.CopyTo(array, 0);
            bindingContext.Model = array;
            return true;
        }
    }

    public class CommaDelimitedArrayAttribute : ModelBinderAttribute
    {
        public CommaDelimitedArrayAttribute()
            : base(typeof(CommaDelimitedArrayModelBinder))
        {
        }
    }
}
