using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupThink.CRM.Model;
using GroupThink.CRM.Common;
using GroupThink.CRM.IBLL;
using Microsoft.Practices.Unity;
using GroupThink.CRM.Model.Sys;

namespace CRM_EasyUI_2016.Controllers
{
    public class LoginController : Controller
    {
        #region 申明

        DBContainer db = new DBContainer();
        [Dependency]
        public ICRM_User_BLL u_BLL { get; set; }
        [Dependency]
        public ICRM_Dept_BLL d_BLL { get; set; }
        [Dependency]
        public ICRM_Role_BLL r_BLL { get; set; }

        //模块菜单的BLL
        [Dependency]
        public ICRM_Module_BLL m_BLL { get; set; }

        [Dependency]
        public ICRM_RoleUser_BLL ru_BLL { get; set; }
        //[Dependency]
        //public IGroupThink_CRM_LoginLog_BLL g_BLL { get; set; }

        #endregion

        ValidationErrors errors = new ValidationErrors();

        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Login(string username, string pwd)
        {
            //GroupThink_CRM_User user = accountBLL.Login(username, ValueConvert.MD5(pwd));
            CRM_User user = u_BLL.Login(username, pwd);
            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            }
            else if (user.UserState != (int)UserStateEnum.正常使用)//冻结、停用
            {
                return Json(JsonHandler.CreateMessage(0, "账户被系统禁用"), JsonRequestBehavior.AllowGet);
            }
            // user.LastLoginTime = DateTime.Now;
            //u_Rep.Edit(user);

            //GroupThink_CRM_LoginLog_Model g = new GroupThink_CRM_LoginLog_Model();
            //g.ID = Guid.NewGuid();
            //g.UserId = user.Id;
            //g.LoginDate = DateTime.Now;
            //g.LoginTime = DateTime.Now;

            //IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            //if (addressList.Length > 1)
            //{
            //    g.IP = ((IPAddress)addressList[0]).ToString();
            //}

            //g_BLL.Create(ref errors, g);

            CRM_User_Model account = new CRM_User_Model();
            account.ID = user.ID;
            account.UserName = user.UserName;
            Session["Account"] = account;
            Session["GetUsers"] = GetSubUsersDeptRole(user.ID);//获取本身和下级用户

            Session["RoleID"] = db.CRM_RoleUser.FirstOrDefault(t => t.UserId == user.ID).RoleId;//将角色ID存入Role缓存

            //Session["GetUsers"] = db.P_GetUsers(user.Id).ToList();
            Session.Timeout = 24 * 12 * 60;

            return Json(JsonHandler.CreateMessage(1, ""), JsonRequestBehavior.AllowGet);
        }

        private List<string> GetSubUsersDeptRole(string uid)
        {
            List<int> roles = (from ru in ru_BLL.GetAll().Where(t => t.UserId == uid)
                               select (int)ru.RoleId).ToList();
            List<int> depts = new List<int>();//获取部门list
            List<string> list2 = new List<string>();//
            foreach (int role in roles)
            {
                int deptId = r_BLL.GetById(role).DeptId;
                if (!depts.Contains(deptId))
                {
                    depts.Add(deptId);
                }
            }
            List<int> deptSub = new List<int>();//下级部门
            depts.ForEach(t =>
            {
                List<int> r1 = d_BLL.GetListByParentId(t).Select(a => a.DeptID).ToList();
                if (r1 != null)
                {
                    deptSub.AddRange(r1);
                }
            });
            deptSub.ForEach(t =>
            {
                List<string> r1 = u_BLL.GetAllByDeptId(t).Select(a => a.ID).ToList();
                if (r1 != null)
                {
                    list2.AddRange(r1);
                }
            });
            //判断下级部门为空
            if (deptSub.Count == 0)
            {
                List<string> tt = GetSubUsers(uid);
                list2.AddRange(tt);
            }

            //下级角色所有用户
            List<int> roles2 = new List<int>();//下级角色
            roles.ForEach(r =>
            {
                List<int> r1 = r_BLL.GetAll().Where(a => a.ParentId == r).Select(b => b.ID).ToList().ToList();
                if (r1 != null)
                {
                    roles2.AddRange(r1);
                }
            });
            roles2.ForEach(t =>
            {
                List<string> r1 = ru_BLL.GetAll().Where(a => a.RoleId == t).Select(b => b.UserId).ToList();
                if (r1 != null)
                {
                    list2.AddRange(r1);
                }
            });

            list2.Add(uid);
            list2 = list2.Distinct().ToList();
            return list2;
        }

        private List<string> GetSubUsers(string uid)
        {
            List<int> roles = (from ru in ru_BLL.GetAll().Where(t => t.UserId == uid)
                               select (int)ru.RoleId).ToList();
            List<int> lists = new List<int>();
            foreach (int role in roles)
            {
                if (r_BLL.GetAll().Where(t => t.ParentId == role).Count() > 0)
                {
                    GetChild(lists, role);
                }
            }
            List<string> u = (from ru in ru_BLL.GetAll()
                              where lists.Contains(ru.RoleId)
                              select ru.UserId).ToList();
            u.Add(uid);
            return u;
        }

        private List<int> GetChild(List<int> lists, int parent)
        {
            List<int> list = new List<int>();
            list = (from r in r_BLL.GetAll().Where(t => t.ParentId == parent)
                    select r.ID).ToList();
            if (list.Count() > 0)
            {
                foreach (int r in list)
                {
                    lists.Add(r);
                    GetChild(lists, r);
                }
            }
            return lists;
        }

        #region 加载菜单
        [HttpPost]
        public JsonResult initMenuData()
        {
            var mlist = this.m_BLL.GetListByRoleID().ToList();//查询业务角色所属的菜单，正常情况下应该是
            var list = new List<Menu_Model>();
            mlist.ForEach(t=>{
                    list.Add(new Menu_Model
                    {
                        menuid = t.ID,
                        icon = "",
                        menuname = t.Name,
                        menus = getChild(t.ID, (int)Session["RoleID"])
                    });
                });


            return Json(list, JsonRequestBehavior.AllowGet);
        }


        private List<menus_Model> getChild(string parentid,int roleID)
        {
            var mlist = this.m_BLL.GetList(parentid, roleID).OrderBy(t => t.Sort).ToList();
            var list = new List<menus_Model>();
            if (mlist.Count > 0)
            {
                mlist.ForEach(t =>
                {

                    list.Add(new menus_Model
                    {
                        menuid=t.ID,
                        menuname = t.Name,
                        icon = "",
                        url = t.Url,
                        children = getChild(t.ID, roleID)
                    });
                });
            }
            return list;
        }


        class Menu_Model
        {
            public string menuid { get; set; }
            public string icon { get; set; }
            public string menuname { get; set; }
            public List<menus_Model> menus { get; set; }
        }

        class menus_Model
        {
            public string menuid { get; set; }
            public string menuname { get; set; }
            public string icon { get; set; }
            public string url { get; set; }
            public List<menus_Model> children { get; set; }
        }
        #endregion

    }
}
