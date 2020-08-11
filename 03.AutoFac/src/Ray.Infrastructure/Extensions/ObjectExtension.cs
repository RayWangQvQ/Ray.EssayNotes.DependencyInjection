using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ray.Infrastructure.Helpers;

namespace Ray.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 利用反射获取实例的某个字段值
        /// （包括私有变量）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fielName"></param>
        /// <returns>返回装箱后的object对象</returns>
        public static object GetFieldValue(this object obj, string fielName)
        {
            try
            {
                Type type = obj.GetType();
                FieldInfo fieldInfo = type.GetFields(BindingFlags.NonPublic
                    | BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly
                    | BindingFlags.Static)
                    .FirstOrDefault(x => x.Name == fielName);
                return fieldInfo?.GetValue(obj);
            }
            catch
            {
                //todo:记录日志
                return default;
            }
        }

        /// <summary>
        /// 利用反射获取实例的某个属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="index"></param>
        /// <returns>返回装箱后的object对象</returns>
        public static object GetPropertyValue(this object obj, string fieldName, object[] index = null)
        {
            try
            {
                Type type = obj.GetType();
                var pi = type.GetProperty(fieldName, BindingFlags.NonPublic
                    | BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly
                    | BindingFlags.Static);
                return pi?.GetValue(obj, index);
            }
            catch
            {
                //todo:记录日志
                return default;
            }
        }


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

        public static string AsJsonStr(this object obj, Action<SettingOption> option = null)
        {
            SettingOption settingOption = new SettingOption();
            option?.Invoke(settingOption);

            var setting = settingOption.BuildSettings();
            return JsonConvert.SerializeObject(obj, setting);
        }
    }

    public class SettingOption
    {
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// 忽略或只保留部分属性
        /// </summary>
        public FilterPropsOption FilterProps { get; set; }

        /// <summary>
        /// 枚举序列化为字符串
        /// </summary>
        public bool EnumToString { get; set; } = false;

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public JsonSerializerSettings BuildSettings()
        {
            if (SerializerSettings == null)
                SerializerSettings = new JsonSerializerSettings();

            //忽略/只保留部分属性
            if (FilterProps != null)
                SerializerSettings.ContractResolver = new FilterPropsContractResolver(FilterProps);

            //枚举处理
            if (EnumToString)
                SerializerSettings.Converters.Add(new StringEnumConverter());

            return SerializerSettings;
        }
    }

    public class FilterPropsOption
    {
        public FilterEnum FilterEnum { get; set; } = FilterEnum.Ignore;

        public string[] Props { get; set; } = { };
    }

    public enum FilterEnum
    {
        Ignore,
        Retain
    }
}
