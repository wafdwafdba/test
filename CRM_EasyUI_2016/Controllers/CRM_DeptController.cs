using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupThink.CRM.Model.Sys;
using GroupThink.CRM.Common;
using GroupThink.CRM.IBLL;
using CRM_EasyUI_2016.Core;

namespace CRM_EasyUI_2016.Controllers
{
    public class CRM_DeptController : BaseController
    {
        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Dept_BLL m_BLL { get; set; }

        private ValidationErrors errors = new ValidationErrors();
        //
        // GET: /CRM_Dept/

        #region 列表
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager)
        {
            List<CRM_Dept_Model> list = this.m_BLL.GetList(ref pager);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new CRM_Dept_Model()
                        {
                            DeptID = r.DeptID,
                            DeptName = r.DeptName,
                            Des = r.Des,
                            IsEnable = (bool)r.IsEnable,
                            ParentID = r.ParentID,
                            IsDelete = r.IsDelete
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetList_(string id)
        {
            if (id == null)
                id = "0";
            List<CRM_Dept_Model> list = m_BLL.GetList_(int.Parse(id));
            var json = from r in list
                       select new CRM_Dept_Model()
                       {
                           DeptID = r.DeptID,
                           DeptName = r.DeptName,
                           ParentID = r.ParentID,
                           Des = r.Des,
                           IsEnable = r.IsEnable,
                           state = (m_BLL.GetList_(r.DeptID).Count > 0) ? "closed" : "open"
                       };

            return Json(json);

        }
        #endregion

        #region 新增
        //[SupportFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(string deptname, int parentId, string des, bool enable)
        {
            CRM_Dept_Model model = new CRM_Dept_Model();
            model.DeptName = deptname;
            model.ParentID = parentId;
            model.Des = des;
            model.IsEnable = enable;
            model.IsDelete = false;//
            if (m_BLL.Create(ref errors, model))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            string ErrorCol = errors.Error;
            if (ErrorCol == "请勿重复添加部门名称")
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 编辑
        //[SupportFilter]
        public ActionResult Edit(int deptid)
        {
            CRM_Dept_Model entity = m_BLL.GetById(deptid);
            return View(entity);
        }

        [HttpPost]
        public JsonResult Edit(int deptid, string deptname, int parentId, string des, bool enable)
        {
            CRM_Dept_Model model = new CRM_Dept_Model();
            model.DeptID = deptid;
            model.DeptName = deptname;
            model.ParentID = parentId;
            model.Des = des;
            model.IsEnable = enable;
            model.IsDelete = false;
            //model.PerformanceCount = false;
            //model.IsFenCompany = IsFenCompany;
            if (m_BLL.Edit(model))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            string ErrorCol = errors.Error;
            if (ErrorCol == "请勿重复添加部门名称")
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 明细
        //[SupportFilter]
        public ActionResult Details(int deptid)
        {
            CRM_Dept_Model entity = m_BLL.GetById(deptid);
            return View(entity);
        }
        #endregion

        #region 删除
        [HttpPost]
        public JsonResult Delete(string deptid)
        {
            var depts = new List<int>();

            string[] a1 = deptid.Split(',');
            for (int i = 0; i < a1.Length; i++)
            {
                depts.Add(Convert.ToInt32(a1[i]));
            }
            
            //depts.Add((int)deptid.Split(","));

            if (!string.IsNullOrWhiteSpace(deptid.ToString()))
            {
                if (this.m_BLL.DeleteAll(depts))
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 下拉列表，加载部门信息
        public string InitTree()
        {
            //string str = "{\"id\":\"0\",\"text\":\"\",\"ParentId\":\"0 \"},";
            string str = "";
            IList<CRM_Dept_Model> list = (from t in this.m_BLL.GetList_(0)
                                                     where t.IsEnable != false
                                                     select t).ToList<CRM_Dept_Model>();
            foreach (CRM_Dept_Model model in list)
            {
                str = str + this.Recursion(model) + ",";
            }
            return ("[" + str.TrimEnd(new char[] { ',' }) + "]");
        }
        public string Recursion(CRM_Dept_Model model)
        {
            string str = "";
            string str3 = str;
            str = str3 + "{\"id\":\"" + model.DeptID + "\",\"text\":\"" + model.DeptName + "\",\"ParentID\":\"" + model.ParentID + "\"";
            IList<CRM_Dept_Model> list = this.m_BLL.GetList_(model.DeptID).ToList<CRM_Dept_Model>();
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
        #endregion

        public JsonResult GetParentList() { 
            List<int> lists=new List<int>();
           foreach(int d in (from r in m_BLL.GetAll().Where(t=>t.IsDelete==false&&t.ParentID==0)
               select r.DeptID))
               {
               lists.Add(d);
                if (m_BLL.GetAll().Where(t => t.ParentID == d && t.IsDelete==false).Count() > 0)
                {
                    GetChild(lists, d);
                }
            }
            List<CRM_Dept_Model> data = (from r in m_BLL.GetAll().Where(t=>lists.Contains(t.DeptID))
                                                    select new CRM_Dept_Model()
                                                    {
                                                        DeptID = r.DeptID,
                                                        DeptName = r.DeptName,
                                                        ParentID = r.ParentID,
                                                        Des = r.Des,
                                                        IsEnable = r.IsEnable,
                                                        IsDelete = r.IsDelete,
                                                        Level = r.Level
                                                    }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
           }
        private List<int> GetChild(List<int> lists, int parent)
        {
            List<int> list = new List<int>();
            list = (from d in m_BLL.GetAll().Where(t => t.ParentID == parent && t.IsDelete == false)
                    select d.DeptID).ToList();
            if (list.Count() > 0)
            {
                foreach (int d in list)
                {
                    lists.Add(d);
                    GetChild(lists, d);
                }
            }
            return lists;
        }
    }
}
