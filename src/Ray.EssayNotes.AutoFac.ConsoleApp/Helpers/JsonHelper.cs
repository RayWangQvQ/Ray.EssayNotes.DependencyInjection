using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Helpers
{
    public static class JsonHelper
    {
        /// <summary>json格式化</summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string AsFormatJsonString(this string str)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonTextReader jsonTextReader = new JsonTextReader((TextReader)new StringReader(str));
            object obj = jsonSerializer.Deserialize((JsonReader)jsonTextReader);
            if (obj == null)
                return str;
            StringWriter stringWriter = new StringWriter();
            JsonTextWriter jsonTextWriter1 = new JsonTextWriter((TextWriter)stringWriter);
            jsonTextWriter1.Formatting = Formatting.Indented;
            jsonTextWriter1.Indentation = 4;
            jsonTextWriter1.IndentChar = ' ';
            JsonTextWriter jsonTextWriter2 = jsonTextWriter1;
            jsonSerializer.Serialize((JsonWriter)jsonTextWriter2, obj);
            return stringWriter.ToString();
        }
    }
}
