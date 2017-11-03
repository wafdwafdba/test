using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupThink.CRM.Model.Sys;
using GroupThink.CRM.Common;
using GroupThink.CRM.IBLL;
using CRM_EasyUI_2016.Core;
using GroupThink.CRM.Model;

namespace CRM_EasyUI_2016.Controllers
{
    public class CRM_UserController : BaseController
    {
        DBContainer db = new DBContainer();
        [Microsoft.Practices.Unity.Dependency]
        public ICRM_User_BLL m_BLL { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_Role_BLL r_BLL { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public ICRM_RoleUser_BLL ru_BLL { get; set; }

        private ValidationErrors errors = new ValidationErrors();
        //
        // GET: /CRM_User/

        #region 列表
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList_(GridPager pager)
        {
            List<string> users = (List<string>)Session["GetUsers"];
            string UserName = Utils.DbString(Request["UserName"]);
            int DeptId = Utils.DbInt(Request["DeptId"]);
            int RoleId = Utils.DbInt(Request["RoleId"]);
            int UserType = Utils.DbInt(Request["UserType"]);
            int UserState = Utils.DbInt(Request["UserState"]);

            List<CRM_User_Model> list = m_BLL.GetList(ref pager, users, UserName, DeptId, RoleId,UserType,UserState);

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new CRM_User_Model()
                        {
                            ID = r.ID,
                            UserName = r.UserName,
                            PassWord = r.PassWord,
                            Sex = r.Sex,
                            Tel = r.Tel,
                            DeptName = r.DeptName,
                            UserType = (int)r.UserType,
                            UserTypeName = r.UserTypeName,
                            UserStateName = Enum.GetName(typeof(UserStateEnum), r.UserState),
                            DeptRole = r.DeptRole,
                            UserState=r.UserState,
                            CreateTime = r.CreateTime
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增
        //[SupportFilter]
        public ActionResult Create()
        {
            BindDropDownList();
            return View();
        }

        [HttpPost]
        public JsonResult Create(CRM_User_Model model)
        {

            model.RoleID = Convert.ToInt32(Request["RoleId"]);
            model.CreateTime = DateTime.Now;
            model.CreateUserID = this.UserId;
            model.PassWord = "123456";
            //UserType
            model.UserState = 1;
            if (m_BLL.Create(ref errors, model))
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            string ErrorCol = errors.Error;
            if (ErrorCol == "请勿重复添加用户信息")
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 编辑
        //[SupportFilter]
        public ActionResult Edit(string id)
        {
            BindDropDownList();
            CRM_User_Model entity = m_BLL.GetDetailsById(id);
            return View(entity);
        }

        [HttpPost]
        public JsonResult Edit(CRM_User_Model model)
        {
            model.RoleID = Convert.ToInt32(Request["RoleId"]);

            CRM_RoleUser_Model c_r = ru_BLL.GetById(model.ID);

            if (m_BLL.Edit(model))
            {

                if (model.RoleID != null)
                {
                    c_r.RoleId = (int)model.RoleID;
                    ru_BLL.Edit(c_r);
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            string ErrorCol = errors.Error;
            if (ErrorCol == "请勿重复添加用户信息")
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 明细
        //[SupportFilter]
        public ActionResult Details(string id)
        {
            CRM_User_Model entity = m_BLL.GetDetailsById(id);
            return View(entity);
        }
        #endregion

        #region 删除
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (this.m_BLL.Delete(id))
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 下拉列表
        private void BindDropDownList()
        {
           
           List<SelectListItem> ute = new List<SelectListItem>();
           foreach (UserTypeEnum bt in Enum.GetValues(typeof(UserTypeEnum)))
                                    {
                                       ute.Add(new SelectListItem() { Text=bt.ToString(),Value=Convert.ToInt32(bt).ToString()});
                                    }

           ViewData["UserType"] = ute;
        }
        #endregion

        //分配角色
        public ActionResult AllotRole(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [HttpPost]
        public JsonResult GetRoleList(GridPager pager, string queryStr, string id)
        {
            List<CRM_Role_Model> _list = r_BLL.GetList(ref pager);
            List<CRM_RoleUser_Model> list_ = ru_BLL.GetAll().Where(t => t.UserId == id).ToList();
            List<CRM_Role_Model> list = new List<CRM_Role_Model>();
            foreach (var item in _list)
            {
                if (list_.Exists(t => t.RoleId == item.ID))
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
                        select new CRM_Role_Model()
                        {
                            ID = r.ID,
                            Name = r.Name,
                            DeptId = r.DeptId,
                            DeptName = r.DeptName,
                            Des = r.Des,
                            Enable = r.Enable,
                            IsSelect = r.IsSelect

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AllotRole(Array roleList, string userId)
        {
            List<CRM_RoleUser_Model> list_ = ru_BLL.GetAll().Where(t => t.UserId == userId).ToList();
            foreach (var item in list_)
            {
                ru_BLL.Delete(item.RoleId, item.UserId);
            }
            List<CRM_RoleUser_Model> list = new List<CRM_RoleUser_Model>();
            for (int i = 0; i < roleList.Length; i++)
            {
                CRM_RoleUser_Model model = new CRM_RoleUser_Model();
                model.UserId = userId;
                model.RoleId = int.Parse(roleList.GetValue(i).ToString());
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
