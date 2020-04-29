using System.IO;
using Newtonsoft.Json;

namespace System
{
    public static class Json_StringExtension
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="useSystem">是否使用系统json</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string str, bool useSystem = true)
        {
            return useSystem
                ? System.Text.Json.JsonSerializer.Deserialize<T>(str)
                : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>将json字符串加工为格式化字符串</summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string AsFormatJsonStr(this string str)
        {
            var jsonSerializer = new JsonSerializer();
            var jsonTextReader = new JsonTextReader((TextReader)new StringReader(str));
            object obj = jsonSerializer.Deserialize((JsonReader)jsonTextReader);
            if (obj == null) return str;
            var stringWriter = new StringWriter();
            var jsonTextWriter1 = new JsonTextWriter((TextWriter)stringWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            JsonTextWriter jsonTextWriter2 = jsonTextWriter1;
            jsonSerializer.Serialize((JsonWriter)jsonTextWriter2, obj);
            return stringWriter.ToString();
        }
    }
}
