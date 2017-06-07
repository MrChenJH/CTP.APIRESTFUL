using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CTP.Redis
{
    public class Util
    {



        private static String PREFIX = "\\u";
        public static String native2Ascii(String str)
        {
            return native2Ascii(str, false);
        }

        /// <summary>
        /// 是否加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isSerc"></param>
        /// <returns></returns>
        public static String native2Ascii(String str, Boolean isSerc)
        {
            if (!isSerc)
            {
                return str;
            }

            char[] chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                sb.Append(char2Ascii(chars[i]));
            }
            return sb.ToString();
        }

        private static String char2Ascii(char c)
        {
            if (c > 255)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(PREFIX);
                int code = (c >> 8);
                String tmp = code.ToString("X");
                if (tmp.Length == 1)
                {
                    sb.Append("0");
                }
                sb.Append(tmp);
                code = (c & 0xFF);
                tmp = code.ToString("X");
                if (tmp.Length == 1)
                {
                    sb.Append("0");
                }
                sb.Append(tmp);
                return sb.ToString();
            }
            else
            {
                return c.ToString();
            }
        }
    }

    /// <summary>
    /// 常用类型转换方式
    /// </summary>
    public static class KdtCommonUtility
    {
        #region object或泛型

        /// <summary>
        /// 判断结构是否为默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }

        /// <summary>
        /// 判断是否是空数据(null或DBNull)
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNothing(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// 比较两个值
        /// </summary>
        /// <param name="value1">待比较值</param>
        /// <param name="value2">比较值</param>
        /// <returns></returns>
        public static bool CompareTo(this object value1, object value2)
        {
            bool isSystemType1 = Type.GetTypeCode(value1.GetType()) != TypeCode.Object;
            bool isSystemType2 = Type.GetTypeCode(value2.GetType()) != TypeCode.Object;

            bool success = false;
            if (isSystemType1)
            {
                //如果value1是基础类型.value2不是基础类型.但已实现IConvertible接口.则将value2转为value1类型
                if (!isSystemType2 && value2 is IConvertible)
                {
                    value2 = ((IConvertible)value2).ToType(value1.GetType(), null);
                }
                else if (value1.GetType() != value2.GetType() && value2 is IConvertible)
                {
                    value2 = ((IConvertible)value2).ToType(value1.GetType(), null);
                }
            }
            else if (isSystemType2)
            {
                //如果value2是基础类型.value1不是基础类型.但已实现IConvertible接口.则将value1转为value2类型
                if (!isSystemType1 && value1 is IConvertible)
                {
                    value1 = ((IConvertible)value1).ToType(value2.GetType(), null);
                }
                else if (value1.GetType() != value2.GetType() && value1 is IConvertible)
                {
                    value1 = ((IConvertible)value1).ToType(value2.GetType(), null);
                }
            }

            int result = 1;

            if (value1 is IComparable)
            {
                success = true;
                result = ((IComparable)value1).CompareTo(value2);
            }
            else if (value2 is IComparable)
            {
                success = true;
                //value2 与 value1相比较.所以结果为相反
                result = 0 - ((IComparable)value2).CompareTo(value1);
            }
            if (success && result == 0)
                return true;

            return false;
        }

        /// <summary>
        /// 发送方法或属性请求，确认方法或属性是否可用。
        /// 确认反射类中是否存在需要的方法或属性。
        /// </summary>
        /// <param name="value">反射类</param>
        /// <param name="member">反射方法或属性名</param>
        /// <param name="ensureNoParameters">确保没有参数</param>
        /// <returns>反射方法中存在对应的方法或属性</returns>
        public static bool Reflect_Respond(this object value, string member, bool ensureNoParameters = true)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            Type type = value.GetType();

            MethodInfo methodInfo = type.GetMethod(member);
            if (methodInfo != null && (!ensureNoParameters || !methodInfo.GetParameters().Any()))
                return true;

            PropertyInfo propertyInfo = type.GetProperty(member);
            if (propertyInfo != null && propertyInfo.CanRead)
                return true;

            return false;
        }

        /// <summary>
        /// 调用反射方法或属性并读取返回信息
        /// </summary>
        /// <param name="value">反射类</param>
        /// <param name="member">反射方法或属性名</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>属性值或方法返回值</returns>
        public static object Reflect_Call(this object value, string member, object[] parameters = null)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            Type type = value.GetType();

            MethodInfo methodInfo = type.GetMethod(member);
            if (methodInfo != null)
                return methodInfo.Invoke(value, parameters);

            PropertyInfo propertyInfo = type.GetProperty(member);
            if (propertyInfo != null)
                return propertyInfo.GetValue(value, null);

            return null;
        }

        /// <summary>
        /// 反射获取对象属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象值</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns>属性值</returns>
        public static object PropertyVal<T>(this T t, string propertyname) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return null;

            return property.GetValue(t, null);
        }

        /// <summary>
        /// 反射设置对象属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象值</param>
        /// <param name="propertyname">属性名称</param>
        /// <param name="propertyvalue">属性值</param>
        public static void PropertyVal<T>(this T t, string propertyname, object propertyvalue) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return;

            property.SetValue(t, propertyvalue, null);
        }

        /// <summary>
        /// 反射创建实体类
        /// </summary>
        public static T Create<T>(this Type t) where T : class, new()
        {
            return (T)Activator.CreateInstance(t);
        }

        /// <summary>
        /// 将对象类型转化成对应类型对象
        /// </summary>
        /// <param name="value">对象值</param>
        /// <param name="type">转化目标类型</param>
        /// <returns></returns>
        public static object ChangeTo(this object value, Type _type)
        {
            if (value.IsNothing())
            {
                switch (Type.GetTypeCode(_type))
                {
                    case TypeCode.Boolean: return false;
                    case TypeCode.Byte: return new Byte();
                    case TypeCode.Char: return new char();

                    case TypeCode.DateTime: return DateTime.Now;
                    case TypeCode.Decimal: return 0;
                    case TypeCode.Double: return 0;
                    case TypeCode.Empty: return "";
                    case TypeCode.Int16: return 0;
                    case TypeCode.Int32: return 0;
                    case TypeCode.Int64: return 0;
                    case TypeCode.Object: return new object();
                    case TypeCode.SByte: return new SByte();
                    case TypeCode.Single: return new Single();
                    case TypeCode.String: return "";
                    case TypeCode.UInt16: return 0;
                    case TypeCode.UInt32: return 0;
                    case TypeCode.UInt64: return 0;
                    default: return "";
                }
            }

            if (_type.Name.ToLower() == "enum")
            {
                if (value is int)
                    return Enum.ToObject(_type, value);
                else
                    return Enum.Parse(_type, value.ToString());
            }
            else if (Type.GetTypeCode(_type) == TypeCode.Boolean)
            {
                int v;
                if (Int32.TryParse(value.ToString(), out v))
                    value = v;
            }
            else if (Type.GetTypeCode(_type) == TypeCode.DateTime
                && value.ToString().Length == 8)
            {
                value = DateTime.ParseExact(value.ToString(), "yyyyMMdd",
                    new System.Globalization.CultureInfo("Zh-cn"));
            }

            return System.Convert.ChangeType(value, _type);
        }

        /// <summary>
        /// 强制数据类型转换成指定格式
        /// </summary>
        /// <typeparam name="T">指定数据类型</typeparam>
        /// <param name="value">需转换值</param>
        public static T Convert<T>(this object value)
        {
            return value.Convert<T>(default(T));
        }

        /// <summary>
        /// 强制数据类型转换成指定格式
        /// </summary>
        /// <typeparam name="T">指定数据类型</typeparam>
        /// <param name="value">需转换值</param>
        public static T Convert<T>(this object value, T defaultValue)
        {
            if (value.IsNothing())
                return defaultValue;

            if (value is string && value == null)
                return defaultValue;

            if (value is bool && Type.GetTypeCode(typeof(T)) == TypeCode.Int32)
            {
                value = (bool)value ? 1 : 0;
            }
            else if (typeof(T).Name.ToLower() == "enum")
            {
                if (value is int)
                    return (T)Enum.ToObject(typeof(T), value);
                else
                    return (T)Enum.Parse(typeof(T), value.ToString());
            }
            else if (Type.GetTypeCode(typeof(T)) == TypeCode.Boolean)
            {
                if (value.ToString().Length == 1)
                {
                    int v;
                    if (Int32.TryParse(value.ToString(), out v))
                        value = v;
                }
            }
            else if (Type.GetTypeCode(typeof(T)) == TypeCode.DateTime
                && value.ToString().Length == 8)
            {
                value = DateTime.ParseExact(value.ToString(), "yyyyMMdd",
                    new System.Globalization.CultureInfo("Zh-cn"));
            }
            else if (value is string && Type.GetTypeCode(typeof(T)) == TypeCode.Int32)
            {
                string v = value.ToString();
                if (v.Equals("false", StringComparison.OrdinalIgnoreCase) || v.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    value = v.Equals("false", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                }
            }

            return (T)System.Convert.ChangeType(value, typeof(T));
        }


        /// <summary>
        /// 转成Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string ToJson<T>(this object v, T v1)
        {
            string json = JsonConvert.SerializeObject(v);
            return json;
        }



        /// <summary>    
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串    
        /// </summary>    
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }


        /// <summary>
        /// 将实体类转换成JSON字符串
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体类</param>
        public static string ToJson<T>(this T entity, bool _transtime = true) where T : class, new()
        {
            if (entity == null) return "";
            string jsonString = JsonConvert.SerializeObject(entity);
            if (_transtime)
            {
                //替换Json的Date字符串    
                string p = @"\\/Date\(-{0,1}(\d+)\+\d+\)\\/";
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                Regex reg = new Regex(p);
                jsonString = reg.Replace(jsonString, matchEvaluator);
            }

            return jsonString;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this string json) where T : class
        {

            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }
        #endregion.

    }



}
