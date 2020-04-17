
namespace System
{
    public static class Json_ObjectExtension
    {
        public static string AsJsonStr(this object obj, bool isSystem = true)
        {
            if (obj == null) return null;
            return isSystem
                ? System.Text.Json.JsonSerializer.Serialize(obj)
                : Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static string AsFormatJsonStr(this object obj)
        {
            return obj.AsJsonStr().AsFormatJsonStr();
        }
    }
}
