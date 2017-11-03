using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_EasyUI_2016.Core;
using GroupThink.CRM.IBLL;
using GroupThink.CRM.Common;
using GroupThink.CRM.Model.Sys;

namespace CRM_EasyUI_2016.Controllers
{
    public class CRM_RoleController : BaseController
    {
        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Role_BLL m_BLL { get; set; }
        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Dept_BLL d_BLL { get; set; }
        [Microsoft.Practices.Unity.Dependency]
        public ICRM_User_BLL u_BLL { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_RoleUser_BLL ru_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        //
        // GET: /CRM_Role/

        public ActionResult Index()
        {
            return View();
        }
        public string InitTree()
        {
            string str = "{\"id\":\"0\",\"text\":\"--顶级菜单--\",\"ParentId\":\"0 \"},";
            IList<CRM_Role_Model> list = (from t in this.m_BLL.GetList_(0)
                                                     where t.Enable != false
                                                     select t).ToList<CRM_Role_Model>();
            foreach (CRM_Role_Model model in list)
            {
                str = str + this.Recursion(model) + ",";
            }
            return ("[" + str.TrimEnd(new char[] { ',' }) + "]");
        }

        public string InitTreeByDepID(int depId)
        {
            string str = "{\"id\":\"0\",\"text\":\"--顶级菜单--\",\"ParentId\":\"0 \"},";
            IList<CRM_Role_Model> list = (from t in this.m_BLL.GetList_(depId, 0)
                                                     where t.Enable != false
                                                     select t).ToList<CRM_Role_Model>();
            foreach (CRM_Role_Model model in list)
            {
                str = str + this.Recursion1(model) + ",";
            }
            return ("[" + str.TrimEnd(new char[] { ',' }) + "]");
        }
        public string Recursion1(CRM_Role_Model model)
        {
            string str = "";
            string str3 = str;
            str = str3 + "{\"id\":\"" + model.ID+ "\",\"text\":\"" + model.Name + "\",\"ParentId\":\"" + model.ParentId + "\"";
            IList<CRM_Role_Model> list = this.m_BLL.GetList_(model.ID).ToList<CRM_Role_Model>();
            if (list != null)
            {
                str = str + ",\"children\":[";
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        str = str + ",";
                    }
                    str = str + this.Recursion1(list[i]);
                }
                str = str + "]";
            }
            return (str + "}");
        }


        public string Recursion(CRM_Role_Model model)
        {
            string str = "";
            string str3 = str;
            str = str3 + "{\"id\":\"" + model.ID + "\",\"text\":\"" + model.Name + "\",\"ParentId\":\"" + model.ParentId + "\"";
            IList<CRM_Role_Model> list = this.m_BLL.GetList_(model.ID).ToList<CRM_Role_Model>();
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

        #region 获取角色列表 // GET: /Authority/CRM_Role/GetJsList
        [HttpPost]
        public JsonResult GetJsList(string pid)
        {
            int parentId = string.IsNullOrWhiteSpace(pid) ? 0 : int.Parse(pid);


            List<CRM_Role_Model> list = this.m_BLL.GetList1(parentId);
            var json = from r in list
                       select new CRM_Role_Model()
                       {
                           ID = r.ID,
                           Name = r.Name,
                           DeptId = r.DeptId,
                           DeptName = r.DeptName,
                           ParentId = r.ParentId,
                           Des = r.Des,
                           Enable = r.Enable,
                           state = (m_BLL.GetList_(r.ID).Count > 0) ? "closed" : "open"
                       };

            return Json(json);
        }

        [HttpPost]
        public JsonResult GetJsList1()
        {
            int parentId = string.IsNullOrWhiteSpace(Request["pid"]) ? 0 : int.Parse(Request["pid"]);
            int organizeId = string.IsNullOrWhiteSpace(Request["oid"]) ? 0 : int.Parse(Request["oid"]);

            List<CRM_Role_Model> list = this.m_BLL.GetList_(organizeId, parentId);
            var json = from r in list
                       select new CRM_Role_Model()
                       {
                           ID = r.ID,
                           Name = r.Name,
                           DeptId = r.DeptId,
                           DeptName = r.DeptName,
                           ParentId = r.ParentId,
                           Des = r.Des,
                           Enable = r.Enable,
                           state = (m_BLL.GetList_(r.ID).Count > 0) ? "closed" : "open"
                       };

            return Json(json);
        }
        #endregion

        [HttpPost]
        public JsonResult GetList(GridPager pager)
        {
            List<CRM_Role_Model> list = m_BLL.GetList(ref pager);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new CRM_Role_Model()
                        {
                            ID = r.ID,
                            Name = r.Name,
                            DeptId = r.DeptId,
                            DeptName = r.DeptName,
                            ParentId = r.ParentId,
                            Des = r.Des,
                            Enable = r.Enable,
                            Level = r.Level

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetList_(string id)
        {
            if (id == null)
                id = "0";
            List<CRM_Role_Model> list = m_BLL.GetList_(int.Parse(id));
            var json = from r in list
                       select new CRM_Role_Model()
                       {
                           ID = r.ID,
                           Name = r.Name,
                           DeptId = r.DeptId,
                           DeptName = r.DeptName,
                           ParentId = r.ParentId,
                           Des = r.Des,
                           Enable = r.Enable,
                           state = (m_BLL.GetList_(r.ID).Count > 0) ? "closed" : "open"
                       };

            return Json(json);
        }

        public ActionResult Create(int oid)
        {
            ViewData["DeptId"] = oid;
            return View();
        }

        [HttpPost]
        public JsonResult GetParentList()
        {
            List<CRM_Role_Model> data = (from r in m_BLL.GetAll()
                                                    select new CRM_Role_Model()
                                                    {
                                                        ID = r.ID,
                                                        Name = r.Name,
                                                        DeptId = r.DeptId,
                                                        ParentId = r.ParentId,
                                                        Level = r.Level
                                                    }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(CRM_Role_Model model)
        {
            //model.Id = m_BLL.GetAll().Max(t=>t.Id) + 1;
            if (this.m_BLL.Create(ref this.errors, model))
            {
                return base.Json(1, JsonRequestBehavior.AllowGet);
            }
            string error = this.errors.Error;
            return base.Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            CRM_Role_Model byId = this.m_BLL.GetById(id);
            return View(byId);
        }

        [HttpPost]
        public JsonResult Edit(CRM_Role_Model model)
        {
            if (this.m_BLL.Edit(model))
            {
                return base.Json(1, JsonRequestBehavior.AllowGet);
            }
            return base.Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            CRM_Role_Model detailsById = this.m_BLL.GetDetailsById(id);
            return View(detailsById);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            //update 20141217 by galen
            //stop delete administrator
            if (id == 5)
            {
                return base.Json(2, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrWhiteSpace(id.ToString()))
            {
                if (this.m_BLL.Delete(id))
                {
                    return base.Json(1, JsonRequestBehavior.AllowGet);
                }
                return base.Json(0, JsonRequestBehavior.AllowGet);
            }
            return base.Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SelectDept(int DeptId)
        {
            List<CRM_Role_Model> data = (from r in m_BLL.GetListByDeptId(DeptId)
                                                    select new CRM_Role_Model()
                                                    {
                                                        ID = r.ID,
                                                        Name = r.Name,
                                                        Level = r.Level

                                                    }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectUser(int id)
        {
            base.ViewData["RoleId"] = id;
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList(GridPager pager, string userName, string id)
        {
            int RoleId = int.Parse(id);
            pager.order = "desc";
            pager.sort = "ID";
            pager.page = 1;
            pager.rows = 10;
            List<CRM_User_Model> _list = u_BLL.GetListQuery(ref pager, userName);
            List<CRM_User_Model> list = new List<CRM_User_Model>();

            List<string> list_ = ru_BLL.GetAll().Where(t => t.RoleId == RoleId).Select(a => a.UserId).ToList();//角色已分配人员

            list_.ForEach(a =>
            {
                CRM_User_Model user = u_BLL.GetById(a);
                user.IsSelect = true;
                list.Add(user);
            });

            foreach (var item in _list)
            {
                if (!list_.Contains(item.ID))
                {
                    item.IsSelect = false;
                    list.Add(item);
                }
            }

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new CRM_User_Model()
                        {
                            ID = r.ID,
                            UserName = r.UserName,
                            IsSelect = r.IsSelect
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateRoleUser(Array userList, int roleId)
        {
            List<CRM_RoleUser_Model> list = new List<CRM_RoleUser_Model>();
            for (int i = 0; i < userList.Length; i++)
            {
                List<CRM_RoleUser_Model> list_ = ru_BLL.GetAll().Where(t => t.UserId == userList.GetValue(i).ToString()).ToList();
                if (list_.Count > 0)
                {
                    foreach (var item in list_)
                    {
                        ru_BLL.Delete(item.RoleId, item.UserId);
                    }
                }
                CRM_RoleUser_Model model = new CRM_RoleUser_Model();
                model.UserId = userList.GetValue(i).ToString();
                model.RoleId = roleId;
                list.Add(model);
            }

            foreach (CRM_RoleUser_Model t in list)
            {
                if (!ru_BLL.IsExists(t.UserId, t.RoleId))
                {
                    ru_BLL.Create(ref errors, t);
                }
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
       
    }
}


