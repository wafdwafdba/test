using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using GroupThink.CRM.IBLL;
using GroupThink.CRM.Common;
using GroupThink.CRM.Model.Sys;
using CRM_EasyUI_2016.Core;
using System.Transactions;
using System.Threading;
using System.IO;
using System.Text;

namespace CRM_EasyUI_2016.Areas.TestManager.Controllers
{
    public class TestController : BaseController
    {
        #region 接口
        ValidationErrors errors = new ValidationErrors();
        #endregion

        #region 查找
        //[SupportFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Test(string PageNum
            , string randcode)
        {
            int pn = 0;
            if (!string.IsNullOrEmpty(PageNum))
            {
                if (PageNum.ToString().Trim() != string.Empty)
                {
                    if (int.TryParse(PageNum.ToString().Trim(), out pn))
                    {
                        pn = int.Parse(PageNum.ToString().Trim());
                    }
                }
            }

            List<NewsInfo> ListQuery = NewsInfo.GetListByPn(pn);
            //string ResultJson = "[{\"Num\":-1,\"Ntitle\":\"暂无数据\"}]";
            //if (ListQuery.Count > 1)
            //{
            //    ResultJson = Obj2Json<List<NewsInfo>>(ListQuery);
            //}
            var json = new
            {
                total = 10,
                rows = ListQuery
            };

            Thread.Sleep(5000);//因为数据量比较少，这里线程暂停5秒，让页面出现数据加载等待的效果

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
