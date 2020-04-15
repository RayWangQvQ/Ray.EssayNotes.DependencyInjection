using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Ray.Infrastructure.Extensions
{
    public static class JsonExtension
    {
        /// <summary>json格式化</summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string AsFormatJsonString(this string str)
        {
            var jsonSerializer = new JsonSerializer();
            var jsonTextReader = new JsonTextReader((TextReader)new StringReader(str));
            object obj = jsonSerializer.Deserialize((JsonReader)jsonTextReader);
            if (obj == null)
                return str;
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
