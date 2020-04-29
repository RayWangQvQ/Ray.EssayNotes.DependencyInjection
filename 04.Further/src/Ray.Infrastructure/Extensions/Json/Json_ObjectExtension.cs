
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions.Json;

namespace System
{
    public static class Json_ObjectExtension
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="useSystem">是否使用系统json</param>
        /// <returns></returns>
        public static string AsJsonStr(this object obj, bool useSystem = true)
        {
            if (obj == null) return null;
            return useSystem
                ? System.Text.Json.JsonSerializer.Serialize(obj)
                : Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 序列化为格式化字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="useSystem">是否使用系统json</param>
        /// <returns></returns>
        public static string AsFormatJsonStr(this object obj, bool useSystem = true)
        {
            return obj.AsJsonStr(useSystem).AsFormatJsonStr();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public static string AsJsonStr(this object obj, Action<SettingOption> option = null)
        {
            SettingOption settingOption = new SettingOption();
            option?.Invoke(settingOption);

            var setting = settingOption.BuildSettings();
            return JsonConvert.SerializeObject(obj, setting);
        }
    }
}
