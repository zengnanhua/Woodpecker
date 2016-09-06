using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

namespace Web.Common
{

    /// <summary>Json的封装函数</summary>
    public class JsonHelper
    {
        #region 通用

        /// <summary>检查字符串是否json格式</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static bool IsJson(string jText)
        {
            if (string.IsNullOrEmpty(jText) || jText.Length < 3)
            {
                return false;
            }

            if (jText.Substring(0, 2) == "{\"" || jText.Substring(0, 3) == "[{\"")
            {
                return true;
            }
            return false;
        }

        /// <summary>检查字符串是否json格式数组</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static bool IsJsonRs(string jText)
        {
            if (string.IsNullOrEmpty(jText) || jText.Length < 3)
            {
                return false;
            }

            if (jText.Substring(0, 3) == "[{\"")
            {
                return true;
            }
            return false;
        }

        /// <summary>格式化 json</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static string Fmt_Null(string jText)
        {
            return StringHelper.ReplaceString(jText, ":null,", ":\"\",", true);
        }

        /// <summary>格式化 json ，删除左右二边的[]</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static string Fmt_Rs(string jText)
        {
            jText = jText.Trim();
            jText = jText.Trim('[');
            jText = jText.Trim(']');
            return jText;
        }

        #endregion

        #region Json序列化

        /// <summary>序列化</summary>
        /// <param name="obj">object </param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            var idtc = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" };

            return JsonConvert.SerializeObject(obj, idtc);
        }


        /// <summary>序列化--sql</summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>   
        public static string ToJson_FromSQL(DataTable dt)
        {
            string ss = ToJson(dt);
            dt.Dispose();
            return ss;
        }

        #endregion

        #region Json反序列化

        /// <summary>反序列化</summary>
        /// <param name="jText"></param>
        /// <returns></returns>      
        public static DataTable ToDataTable(string jText)
        {
            if (string.IsNullOrEmpty(jText))
            {
                return null;
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<DataTable>(jText);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>反序列化</summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jText">json字符串</param>
        /// <returns>类型数据</returns>
        public static T ToObject<T>(string jText)
        {
            return (T)JsonConvert.DeserializeObject(jText, typeof(T));
        }


        /// <summary>
        /// json 字符串转换为智能model 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static List<T> JsonToModel<T>(string jText) where T : new()
        {
            DataTable dt = JsonHelper.ToDataTable(jText);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //T model = (T)Activator.CreateInstance(typeof(T));  
                T model = new T();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                   
                    if (dt.Columns.Contains(propertyInfo.Name))           //json里面有的字段就转
                        ChangeType(propertyInfo, model, dr);
                }
                modelList.Add(model);
            }
            return modelList;
        }
        #endregion
        private static void ChangeType<T>(PropertyInfo propertyInfo, T model, DataRow row)
        {
            if (propertyInfo.PropertyType.FullName.ToLower().Contains("decimal"))
            {
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model,Convert.ToDecimal(row[propertyInfo.Name]), null);

            }
            else if (propertyInfo.PropertyType.FullName.ToLower().Contains("int"))
            {
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model, Convert.ToInt32(row[propertyInfo.Name]), null);
            }
            else
            {
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model, row[propertyInfo.Name].ToString(), null);
            }

          
        }

    }
}