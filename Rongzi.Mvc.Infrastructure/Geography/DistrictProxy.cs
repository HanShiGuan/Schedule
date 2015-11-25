using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Rongzi.Entity.DTO.Common;
using Rongzi.Infrastructure.Http;
using Rongzi.Entity;

namespace Rongzi.Mvc.Infrastructure
{
    /// <summary>
    /// 城区代理类
    /// </summary>
    public class DistrictProxy
    {
        #region 低风险地区,只有6个城市有风险概念，不具有通用性放在程序里面做

        private static HashSet<int> _hasRiskLevelCity = new HashSet<int>()
        {
            3,      //    上海
            66,     //    苏州
            64,     //    无锡
            63,     //    南京
            78,     //    杭州
            81,     //    宁波
        };

        /// <summary>
        /// 低风险地区[贷款信用良好的地区]
        /// </summary>
        private static HashSet<int> _goodCredit = new HashSet<int>()
        {
            //------上海-------//
            37,     //  黄浦区
            38,     //  卢湾区
            39,     //  徐汇区
            40,     //  长宁区
            41,     //  静安区
            42,     //	普陀区
            43,     //	闸北区
            44,     //	虹口区
            45,     //	杨浦区
            46,     //	闵行区
            47,     //	宝山区
            48,     //	嘉定区
            49,     //	浦东新区
            //51,     //	松江区
            //52,     //	青浦区
            //53,     //	南汇区

            //------苏州--------//
            695,    //	沧浪区
            697,    //	平江区
            698,    //	金阊区
            699,    //	虎丘区
            703,    //	吴中区
            704,    //	相城区

            //------无锡--------//
            681,    //	崇安区
            682,    //	南长区
            683,    //	北塘区
            684,    //	锡山区
            685,    //	惠山区
            686,    //	滨湖区

            //------南京--------//
            668,    //	江宁区
            669,    //	浦口区
            670,    //	玄武区
            671,    //	白下区
            672,    //	秦淮区
            673,    //	建邺区
            674,    //	鼓楼区
            675,    //	下关区
            676,    //	栖霞区
            677,    //	雨花台区
            //680,    //	高淳县

            //------杭州--------//
            784,    //	上城区
            785,    //	下城区
            786,    //	江干区
            787,    //	拱墅区
            788,    //	西湖区
            789,    //	滨江区
            790,    //	余杭区
            796,    //	萧山区

            //------宁波--------//
            809,    //	海曙区
            810,    //	江东区
            811,    //	江北区
        };
        #endregion

        /// <summary>
        /// 是否低风险
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public static bool IsGoodCredit(int districtID)
        {
            return _goodCredit.Contains(districtID);
        }

        /// <summary>
        /// 是否划分风险等级
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public static bool HasRiskLevel(int cityID)
        {
            return _hasRiskLevelCity.Contains(cityID);
        }

        /// <summary>
        /// 调用接口,取某个城市下的区域
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public List<DistrictModel> GetDistricts(int cityID)
        {
            ApiHost host = new MvcApiHost("api/Common/District");
            RequestContext<int> req = new MvcRequestContext<int>();
            req.Content = cityID;
            var res = RzHttpClient.Post<RequestContext<int>, ResponseContext<List<DistrictModel>>>(host, req);
            if (res.HasContent())
            {
                return res.Result.Content;
            }
            return null;
        }
    }
}
