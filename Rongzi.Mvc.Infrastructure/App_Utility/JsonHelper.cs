using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rongzi.Mvc.Infrastructure.App_Utility
{
    /// <summary>
    /// Json string和Object之间的转换
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将实体转换为Json对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJsonFromObject(object jsonObject)
        {
            string t = string.Empty;
            try
            {
                JsonSerializerSettings set = new JsonSerializerSettings();
                set.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                t = JsonConvert.SerializeObject(jsonObject, set);
            }
            catch { }
            return t;
        }
        /// <summary>
        /// 将stream对象转换为Json格式的String
        /// 返回
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetJsonFromStream(Stream stream)
        {
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(stream).ReadToEnd();
            return json;

        }

        public static T GetTFromJsonString<T>(string jsonString)
        {
            T t = default(T);
            try
            {
                JsonSerializerSettings set = new JsonSerializerSettings();
                set.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                t = JsonConvert.DeserializeObject<T>(jsonString, set);
            }
            catch (Exception ex)
            { }
            return t;
        }


    }
}
