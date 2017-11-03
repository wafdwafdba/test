using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupThink.CRM.Model.Sys;
using System.Text;
using GroupThink.CRM.Common;

namespace CRM_EasyUI_2016.Core
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserId()
        {
            if (Session["Account"] != null)
            {
                CRM_User_Model info = (CRM_User_Model)Session["Account"];
                return info.ID;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 当前用户Id
        /// </summary>
        public string UserId
        {
            get
            {
                return ((CRM_User_Model)Session["Account"]).ID;
            }
        }

        /// <summary>
        /// 当前用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return ((CRM_User_Model)Session["Account"]).UserName;
            }
        }

        /// <summary>
        /// 获取当前用户Name
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            if (Session["Account"] != null)
            {
                CRM_User_Model info = (CRM_User_Model)Session["Account"];
                return info.UserName;
            }
            else
            {
                return "";
            }
        }

        ///// <summary>
        ///// 允许查看下属数据
        ///// </summary>
        //public bool AllowViewSub
        //{
        //    get
        //    {
        //        return (bool)Session["AllowViewSub"];
        //    }
        //}

        /// <summary>
        /// 允许查看下属客户信息
        /// </summary>
        public bool ViewSub1
        {
            get
            {
                return (bool)Session["ViewSub1"];
            }
        }
        /// <summary>
        /// 允许查看下属客户联系方式
        /// </summary>
        public bool ViewSub2
        {
            get
            {
                return (bool)Session["ViewSub2"];
            }
        }
        /// <summary>
        /// 允许查看下属跟进信息
        /// </summary>
        public bool ViewSub3
        {
            get
            {
                return (bool)Session["ViewSub3"];
            }
        }
        /// <summary>
        /// 允许查看下属合同信息
        /// </summary>
        public bool ViewSub4
        {
            get
            {
                return (bool)Session["ViewSub4"];
            }
        }

        /// <summary>
        /// 允许操作（编辑、删除等）下属客户信息
        /// </summary>
        public bool ActionSub1
        {
            get
            {
                return (bool)Session["ActionSub1"];
            }
        }
        /// <summary>
        /// 允许操作（编辑、删除等）下属跟进信息
        /// </summary>
        public bool ActionSub2
        {
            get
            {
                return (bool)Session["ActionSub2"];
            }
        }
        /// <summary>
        /// 允许操作（编辑、删除等）下属合同信息
        /// </summary>
        public bool ActionSub3
        {
            get
            {
                return (bool)Session["ActionSub3"];
            }
        }

        /// <summary>
        /// 自己和下属用户列表
        /// </summary>
        public List<string> Users
        {
            get
            {
                return (List<string>)Session["GetUsers"];//获得当前用户及其下级用户
            }
        }

        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public string GetUserID()
        {
            if (Session["Account"] != null)
            {
                CRM_User_Model info = (CRM_User_Model)Session["Account"];
                return info.ID;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public CRM_User_Model GetAccount()
        {
            if (Session["Account"] != null)
            {
                return (CRM_User_Model)Session["Account"];
            }
            return null;
        }


        public bool ValidateSql(string sql, ref string msg)
        {
            if (sql.ToLower().IndexOf("delete") > 0)
            {
                msg = "查询参数中含有非法语句DELETE";
                return false;
            }
            if (sql.ToLower().IndexOf("update") > 0)
            {
                msg = "查询参数中含有非法语句UPDATE";
                return false;
            }

            if (sql.ToLower().IndexOf("insert") > 0)
            {
                msg = "查询参数中含有非法语句INSERT";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取当前页或操作访问权限
        /// </summary>
        /// <returns>权限列表</returns>
        public List<PermModel> GetPermission()
        {
            string filePath = HttpContext.Request.FilePath;

            List<PermModel> perm = (List<PermModel>)Session[filePath];
            return perm;
        }

        //public string GetFilterFileExt(string strFileExt)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    foreach (string str in strFileExt.Split(','))
        //    {
        //        if (SystemConfig.FTPFileType.Contains(str))
        //        {
        //            sb.Append(str + ",");
        //        }
        //    }
        //    return sb.ToString().TrimEnd(',');
        //}

        //获取当前用户及其所有下级用户
        public List<string> GetUsers()
        {
            if (Session["GetUsers"] != null)
            {
                List<string> users = (List<string>)Session["GetUsers"];
                return users;
            }
            else
            {
                return null;
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ToJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                JsonRequestBehavior = behavior,
                FormatStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, JsonRequestBehavior behavior, string format)
        {
            return new ToJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormatStr = format
            };
        }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, string format)
        {
            return new ToJsonResult
            {
                Data = data,
                FormatStr = format
            };
        }

    }
}
