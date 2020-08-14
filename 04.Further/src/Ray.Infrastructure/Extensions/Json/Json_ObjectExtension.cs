
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions.Json;

namespace System
{
    public static class Json_ObjectExtension
    {
        public static string AsJsonStr(this object obj, bool useSystem = true)
        {
            if (obj == null) return null;
            return useSystem
                ? System.Text.Json.JsonSerializer.Serialize(obj)
                : Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static string AsFormatJsonStr(this object obj, bool useSystem = true)
        {
            return obj.AsJsonStr(useSystem).AsFormatJsonStr();
        }

        public static string AsJsonStr(this object obj, Action<SettingOption> option)
        {
            SettingOption settingOption = new SettingOption();
            option?.Invoke(settingOption);

            var setting = settingOption.BuildSettings();
            return JsonConvert.SerializeObject(obj, setting);
        }
    }
}
