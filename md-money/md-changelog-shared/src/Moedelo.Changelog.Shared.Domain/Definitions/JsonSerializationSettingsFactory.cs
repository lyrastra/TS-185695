using Moedelo.Infrastructure.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moedelo.Changelog.Shared.Domain.Definitions
{
    internal static class JsonSerializationSettingsFactory
    {
        public static JsonSerializerSettings SkipErrorSettings => Create();

        private static JsonSerializerSettings Create()
        {
            var settings = MdSerializerSettings.GetBy(MdSerializerSettingsEnum.None) ?? new JsonSerializerSettings();
            settings.Error = HandleDeserializationError;

            return settings;
        }
        
        private static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            // здесь можно отлаживать ошибки десериализации
            // var currentError = errorArgs.ErrorContext.Error.Message;
            // все ошибки игнорируем (скорее всего описание модели было изменено несовместимым образом)
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
