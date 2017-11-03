// ----------------------------------------------------------------
// Copyright (C) 2005 武汉群策联动软件有限公司
// 版权所有。 
//         
// 文件功能描述：工具类
//
// 
// 创建标识：Lupe------20150604------add
// ----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupThink.CRM.Common
{
    public class Utils
    {

        /// <summary>
        /// 是否为 Null、""、 " "、 空格等空白符号（IsNullOrWhiteSpace）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(object value)
        {
            return string.IsNullOrWhiteSpace(Utils.DbString(value));
        }

        /// <summary>
        /// 非空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NotNull(object value)
        {
            return !IsNull(value);
        }

        /// <summary>
        /// 对象转换为String类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DbString(object obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }

        /// <summary>
        /// 对象转换为String类型，为null返回指定的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static string DbString(object obj, string Default)
        {
            if (obj == null) return Default;
            return obj.ToString();
        }

        /// <summary>
        /// 对象转换为Int类型，Null则返回-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DbInt(object obj)
        {
            return DbInt(obj, -1);
        }

        /// <summary>
        /// 对象转换为Int类型，Null则返回指定值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static int DbInt(object obj, int Default)
        {
            if (obj == null) return Default;
            if (obj.ToString().ToLower() == "true") return 1;
            if (obj.ToString().ToLower() == "false") return 0;
            try
            {
                return int.Parse(obj.ToString());
            }
            catch { return Default; }
        }

        /// <summary>
        /// 对象转换为DateTime类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? DbDateTime(object obj)
        {
            if (obj == null) return null;
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch { return null; }
        }

        public static DateTime DbDateTime(object obj,DateTime Default)
        {
            if (obj == null) return Default;
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch { return Default; }
        }

        /// <summary>
        /// 带Null判断的Guid对象
        /// </summary>
        public class Guid
        {
            public System.Guid guid { get; set; }
            public bool IsNull { get; set; }

            public Guid(object _guid)
            {
                try
                {
                    if (guid is System.Guid)
                    {
                        this.guid = (System.Guid)_guid;
                    }
                    this.guid = new System.Guid(_guid.ToString());
                    this.IsNull = false;
                }
                catch
                {
                    //this.guid = new System.Guid();
                    this.IsNull = true;
                }
            }
        }
    }
}
