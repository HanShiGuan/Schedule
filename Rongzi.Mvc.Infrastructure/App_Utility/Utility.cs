using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace Rongzi.Mvc.Infrastructure
{
    public class Utility
    {
        static string LOG_PATH = ConfigurationManager.AppSettings["LogPath"];

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(LogInfo log)
        {
            if (log == null) return;

            var now = DateTime.Now;

            //创建目录
            var path = Path.Combine(LOG_PATH, System.Enum.GetName(typeof(LogType), log.LogType));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, now.ToString("yyyy-MM"));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            //日志内容
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\r\n", now.ToString("时间：HH:mm:ss"));
            if (!String.IsNullOrEmpty(log.Id)) sb.AppendFormat("ID：{0}\r\n", log.Id);
            if (!String.IsNullOrEmpty(log.MessageType)) sb.AppendFormat("类型：{0}\r\n", log.MessageType);
            if (!String.IsNullOrEmpty(log.Message)) sb.AppendFormat("消息：{0}\r\n", log.Message);
            sb.Append("\r\n");

            //写入文件
            var filePth = Path.Combine(path, now.ToString("yyyy-MM-dd") + ".txt");
            StreamWriter sw = new StreamWriter(filePth, true);
            sw.Write(sb.ToString());
            sw.Close();
        }
    }

    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }

        /// <summary>
        /// 业务单ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        ServiceEvent = 1,       // 服务程序事件日志
        OrderFlowing = 2,       // 订单流转日志
        PayClose = 3,           // 支付关闭日志
        ClearQRCode = 4,        // 清除二维码
        ClearOrder = 5,         // 清理失效精选订单
        PrdRaking = 6,          // 经理积分
        Schedule = 7,            // 任务调度
    }
}
