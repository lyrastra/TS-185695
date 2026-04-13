using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Moedelo.Infrastructure.Json
{
    public static class JsonExtensions
    {
        public static string ToJsonString(
            this object obj,
            MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None,
            MdSerializerNullHandling nullHandling = MdSerializerNullHandling.Include)
        {
            return JsonConvert.SerializeObject(obj, MdSerializerSettings.GetBy(settingEnum, nullHandling));
        }

        public static object DeserializeToObject(this string jsonString, bool suppressException = false)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonString);
            }
            catch when (suppressException)
            {
                /* подавить исключения */
                return null;
            }
        }

        public static T FromJsonString<T>(this string json, MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None)
        {
            return JsonConvert.DeserializeObject<T>(json, MdSerializerSettings.GetBy(settingEnum));
        }

        public static T FromJsonStringSafeOrDefault<T>(this string json, MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, MdSerializerSettings.GetBy(settingEnum));
            }
            catch
            {
                return default;
            }
        }

        public static long GetJsonLength<TData>(this TData data)
        {
            if (data == null)
            {
                return 0L;
            }

            using var jsonStream = data.ToJsonStream(new StreamLengthCalculator());

            return jsonStream.Length;
        }

        public static TStream ToJsonStream<TData, TStream>(
            this TData value,
            TStream stream,
            MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None) where TStream : Stream
        {
            var serializer = JsonSerializer.CreateDefault(MdSerializerSettings.GetBy(settingEnum));

            using var textWriter = new StreamWriter(stream);
            using var jsonWriter = new JsonTextWriter(textWriter);

            serializer.Serialize(textWriter, value, typeof(TData));

            return stream;
        }

        public static TData FromJsonStream<TData>(this Stream jsonStream,
                MdSerializerSettingsEnum settingEnum = MdSerializerSettingsEnum.None,
                bool leaveStreamOpen = true)
        {
            using var streamReader = new StreamReader(jsonStream,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: leaveStreamOpen);

            using var jsonTextReader = new JsonTextReader(streamReader);

            var serializer = JsonSerializer.CreateDefault(MdSerializerSettings.GetBy(settingEnum));

            return serializer.Deserialize<TData>(jsonTextReader);
        }
    }
}