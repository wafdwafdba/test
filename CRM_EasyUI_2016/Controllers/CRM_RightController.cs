using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using GroupThink.CRM.IBLL;
using GroupThink.CRM.Common;
using GroupThink.CRM.Model;
using GroupThink.CRM.Model.Sys;
using System;
using CRM_EasyUI_2016.Core;

namespace CRM_EasyUI_2016.Controllers
{
    public class CRM_RightController : BaseController
    {
        [Dependency]
        public ICRM_Right_BLL m_BLL { get; set; }
        [Dependency]
        public ICRM_RightOperate_BLL r_operateBLL { get; set; }
        [Dependency]
        public ICRM_ModuleOperate_BLL m_operateBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult GetOptListByModule(GridPager pager,string rid, string mid)
        {
            pager.order = "desc";
            pager.sort = "ID";
            pager.rows = 1000;
            pager.page = 1;
            List<CRM_ModuleOperate_Model> _list = m_operateBLL.GetList(ref pager, mid);
            List<CRM_RightOperate_Model> list_ = r_operateBLL.GetAll().Where(t => t.RightID == rid + mid).ToList();
            List<CRM_ModuleOperate_Model> list = new List<CRM_ModuleOperate_Model>();
            foreach (var item in _list)
            {
                if (list_.Exists(t => t.KeyCode == item.KeyCode))
                {
                    item.IsSelect = true;
                }
                else
                {
                    item.IsSelect = false;
                }
                list.Add(item);
            }
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new CRM_ModuleOperate_Model()
                        {
                            ID = r.ID,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            ModuleId = r.ModuleId,
                            IsValid = r.IsValid,
                            Sort = r.Sort,
                            IsSelect = r.IsSelect
                        }).ToArray()
            };
            return Json(json);
        }

        #region 创建
        [HttpPost]     
        public JsonResult Create(int roleId,string moduleId,Array operateList)
        {
            try
            {
                m_BLL.Delete(roleId.ToString() + moduleId);//
                CRM_Right_Model r = new CRM_Right_Model();
                r.ID = roleId.ToString() + moduleId;
                r.RoleID = roleId;
                r.ModulID = moduleId;
                r.RightFlag = false;
                m_BLL.Create(ref errors, r);

                bool IsExist = false;  //是否有查看权限         
                for (int i = 0; i < operateList.Length; i++)
                {
                    if (operateList.GetValue(i).ToString() == "View")
                    {
                        IsExist = true;
                    }
                    if (operateList.GetValue(i).ToString() != "null")
                    {
                        CRM_RightOperate_Model ro = new CRM_RightOperate_Model();
                        ro.ID = r.ID + operateList.GetValue(i).ToString();
                        ro.RightID = r.ID;
                        ro.KeyCode = operateList.GetValue(i).ToString();
                        ro.IsValid = true;
                        r_operateBLL.Create(ref errors, ro);
                    }
                }

                r.RightFlag = IsExist;
                m_BLL.Edit(r);

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}
