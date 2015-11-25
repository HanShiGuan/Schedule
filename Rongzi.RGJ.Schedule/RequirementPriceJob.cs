using System;
using Quartz;
using Rongzi.Entity;
using Rongzi.Infrastructure.Http;
using Rongzi.Infrastructure.Log;
using Rongzi.Mvc.Infrastructure;

namespace Rongzi.RGJ.Schedule
{
    /// <summary>
    /// 需求书价格计算job
    /// </summary>
    public class RequirementPriceJob : IJob
    {
        // CommonLoging 
        private static readonly Common.Logging.ILog CommonLogger = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var host = new MvcApiHost("api/Customer/BatchUpdateApplyInfoesPrice");
                var request = new MvcRequestContext<string>() { Content = string.Empty };

                var response = RzHttpClient.Post<RequestContext<string>, ResponseContext<bool>>(host, request);

                if (response.HasContent() && response.Result.Content)
                {
                    return;
                }
                Log.Info("Rongzi.SDT.Schedule", "RequeirmentPriceJob",
                    string.Format("RequeirmentPriceJob == host:{0}  /r  request:{1} /r response:{2}",
                    host.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(request),
                    Newtonsoft.Json.JsonConvert.SerializeObject(response)));

                CommonLogger.Info("RequeirmentPriceJob 任务运行成功结束");
            }
            catch (Exception ex)
            {
                CommonLogger.Error("Rongzi.SDT.Schedule---RequirementPriceJob", ex);
            }
        }

    }
}
