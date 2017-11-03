namespace CRM_EasyUI_2016.Controllers
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.IBLL;
    using GroupThink.CRM.Model.Sys;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using CRM_EasyUI_2016.Core;

    public class CRM_ModuleController : BaseController
    {    
        private ValidationErrors errors = new ValidationErrors();

        [HttpPost]
        public JsonResult Create(CRM_Module_Model model)
        {
            model.CreateTime = new DateTime?(DateTime.Now);
            model.CreateUserID = base.GetCurrentUserId();
            if ((model != null) && base.ModelState.IsValid)
            {
                string error;
                if (this.m_BLL.IsExist(model.ID))
                {
                    error = "此模块ID已存在，请输入一个不同的ID!";
                    return base.Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ": " + error));
                }
                if (this.m_BLL.Create(ref this.errors, model))
                {
                    return base.Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                error = this.errors.Error;
                return base.Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + error));
            }
            return base.Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
        }

        //[SupportFilter]
        public ActionResult Create(string id)
        {
            ((dynamic) base.ViewBag).Perm = base.GetPermission();
            CRM_Module_Model model2 = new CRM_Module_Model {
                ParentID = id,
                Enable = true,
                Sort = 0
            };
            CRM_Module_Model model = model2;
            return base.View(model);
        }

        [HttpPost]
        public JsonResult CreateOpt(CRM_ModuleOperate_Model info)
        {
            if ((info != null) && base.ModelState.IsValid)
            {
                if (this.m_operateBLL.GetById(info.ID).ID!= null)
                {
                    return base.Json(JsonHandler.CreateMessage(0, Suggestion.PrimaryRepeat), JsonRequestBehavior.AllowGet);
                }
                CRM_ModuleOperate_Model model = new CRM_ModuleOperate_Model {
                    ID = info.ModuleId + info.KeyCode,
                    Name = info.Name,
                    KeyCode = info.KeyCode,
                    ModuleId = info.ModuleId,
                    IsValid = info.IsValid,
                    Sort = info.Sort
                };
                if (this.m_operateBLL.Create(ref this.errors, model))
                {
                    return base.Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
                }
                string error = this.errors.Error;
                return base.Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + error), JsonRequestBehavior.AllowGet);
            }
            return base.Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOpt(string moduleId)
        {
            ((dynamic) base.ViewBag).Perm = base.GetPermission();
            CRM_ModuleOperate_Model model = new CRM_ModuleOperate_Model {
                ModuleId = moduleId,
                IsValid = true
            };
            return base.View(model);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (this.m_BLL.Delete(id))
                {
                    return base.Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                string error = this.errors.Error;
                return base.Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + error), JsonRequestBehavior.AllowGet);
            }
            return base.Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteOpt(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (this.m_operateBLL.Delete(id))
                {
                    return base.Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                string error = this.errors.Error;
                return base.Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + error), JsonRequestBehavior.AllowGet);
            }
            return base.Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CRM_Module_Model model)
        {
            if ((model != null) && base.ModelState.IsValid)
            {
                if (this.m_BLL.Edit(model))
                {
                    return base.Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                string error = this.errors.Error;
                return base.Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + error));
            }
            return base.Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
        }

        public ActionResult Edit(string id)
        {
            ((dynamic) base.ViewBag).Perm = base.GetPermission();
            CRM_Module_Model byId = this.m_BLL.GetById(id);
            return base.View(byId);
        }

        [HttpPost]
        public JsonResult GetList(string id)
        {
            if (id == null)
            {
                id = "0";
            }
            IEnumerable<CRM_Module_Model> data = from r in (from t in this.m_BLL.GetList(id)
                                                            where t.ID != "0"
                                                            select t).ToList<CRM_Module_Model>()
                                                             select new CRM_Module_Model 
                                                             { ID = r.ID,
                                                              Name = r.Name,
                                                              ParentID = r.ParentID,
                                                              Url = r.Url, Sort = r.Sort,
                                                              Enable = r.Enable, 
                                                              CreateUserID = r.CreateUserID,
                                                              CreateTime = r.CreateTime,
                                                              IsLast = r.IsLast,
                                                              state = (this.m_BLL.GetList(r.ID).Count > 0) ? "closed" : "open" 
                                                             };
            return base.Json(data);
        }

        [HttpPost]
        public JsonResult GetOptListByModule(GridPager pager, string mid)
        {
            pager.rows = 10;
            pager.page = 1;
            pager.sort = "ID";
            pager.order = "DESC";
            List<CRM_ModuleOperate_Model> list = this.m_operateBLL.GetList(ref pager, mid);
            var data = new
            {
                total = pager.totalRows,
                rows = (from r in list select new CRM_ModuleOperate_Model { ID = r.ID, Name = r.Name, KeyCode = r.KeyCode, ModuleId = r.ModuleId, IsValid = r.IsValid, Sort = r.Sort }).ToArray<CRM_ModuleOperate_Model>()
            };
            return base.Json(data);
        }

        public ActionResult Index()
        {
            ((dynamic) base.ViewBag).Perm = base.GetPermission();
            return base.View();
        }

        public string InitTree()
        {
            string str = "{\"id\":\"0\",\"text\":\"--顶级菜单--\",\"ParentID\":\"0 \"},";
            IList<CRM_Module_Model> list = (from t in this.m_BLL.GetList("0")
                where t.ID != "0"
                select t).ToList<CRM_Module_Model>();
            foreach (CRM_Module_Model model in list)
            {
                str = str + this.Recursion(model) + ",";
            }
            return ("[" + str.TrimEnd(new char[] { ',' }) + "]");
        }

        public string Recursion(CRM_Module_Model model)
        {
            string str = "";
            string str3 = str;
            str = str3 + "{\"id\":\"" + model.ID + "\",\"text\":\"" + model.Name + "\",\"ParentID\":\"" + model.ParentID + "\"";
            IList<CRM_Module_Model> list = this.m_BLL.GetList(model.ID).ToList<CRM_Module_Model>();
            if (list != null)
            {
                str = str + ",\"children\":[";
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        str = str + ",";
                    }
                    str = str + this.Recursion(list[i]);
                }
                str = str + "]";
            }
            return (str + "}");
        }


        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Module_BLL m_BLL { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_ModuleOperate_BLL m_operateBLL { get; set; }

    }
}

