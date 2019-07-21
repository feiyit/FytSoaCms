using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Api
{
    public class SiteTool
    {
        private static CmsSite _site;
        /// <summary>
        /// 当前用户对象
        /// </summary>
        public static CmsSite CurrentSite
        {

            get
            {
                //if (_site != null) return _site;
                var types = ConfigExtensions.Configuration[KeyHelper.LOGINAUTHORIZE];
                if (types == "Redis")
                {
                    _site= RedisHelper.Get<CmsSite>(KeyHelper.NOWSITE);
                }
                else
                {
                    _site= MemoryCacheService.Default.GetCache<CmsSite>(KeyHelper.NOWSITE);
                }
                return _site;
            }
        }
    }
}
