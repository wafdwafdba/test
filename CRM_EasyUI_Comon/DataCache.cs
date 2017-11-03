using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace GroupThink.CRM.Common
{
    public class DataCache
    {
        // Methods
        public DataCache() { }
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        public static void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        public static object RemoveCache(string CacheKey)
        {
            return HttpRuntime.Cache.Remove(CacheKey);
        }

        public static void RemoveAllCache()
        {
            HttpRuntime.Cache.Remove("SystemConfiguration_Model"); //系统调置          
            //HttpRuntime.Cache.Remove("");
            //HttpRuntime.Cache.Remove("");
        }
    }

}
