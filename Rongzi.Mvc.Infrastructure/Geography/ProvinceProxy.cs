using System;
using System.Collections.Generic;
using System.Linq;
using Rongzi.Entity;
using Rongzi.Entity.DTO.Common;
using Rongzi.Infrastructure.Http;

namespace Rongzi.Mvc.Infrastructure
{
    /// <summary>
    /// 省份代理类，省份固定不变可放永久缓存
    /// </summary>
    public static class ProvinceProxy
    {
        /// <summary>
        /// 热门省份缓存
        /// </summary>
        public static Lazy<Dictionary<int, ProvinceModel>> HotProvinceMap = new Lazy<Dictionary<int, ProvinceModel>>(
            () => GetProvinceMap(1));

        /// <summary>
        /// 取有权限的省份
        /// </summary>
        /// <param name="provinceIDs"></param>
        /// <returns></returns>
        public static Dictionary<int, ProvinceModel> GetAuthorityProvince(IEnumerable<int> provinceIDs)
        {
            var authorityProvinceMap = new Dictionary<int, ProvinceModel>();
            provinceIDs = provinceIDs.Distinct();
            if (provinceIDs ==null || provinceIDs.Count() == 0
                || HotProvinceMap.Value == null || HotProvinceMap.Value.Count == 0) return authorityProvinceMap;
            
            foreach (var province in provinceIDs)
            {
                if (!HotProvinceMap.Value.ContainsKey(province)) break;

                authorityProvinceMap.Add(province, HotProvinceMap.Value[province]);
            }

            return authorityProvinceMap;
        }

        /// <summary>
        /// 调用接口获取省份信息
        /// </summary>
        /// <param name="isHot"></param>
        /// <returns></returns>
        private static Dictionary<int, ProvinceModel> GetProvinceMap(int isHot)
        {
            //请求参数
            var request = new MvcRequestContext<string>();
            ApiHost host = new MvcApiHost("api/Common/Province?isHot=" + isHot);

            var ret = RzHttpClient.Post<RequestContext<string>, ResponseContext<List<ProvinceModel>>>(host, request).Result;
            if (ret.Head.Ret != 0 || ret.Content == null)
            {
                return null;
            }

            return ret.Content.ToDictionary(k => k.ProId, v => v);
        }
    }
}
