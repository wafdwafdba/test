using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupThink.CRM.DAL;
using GroupThink.CRM.IBLL;
using GroupThink.CRM.IDAL;
using Microsoft.Practices.Unity;
using GroupThink.CRM.BLL;

namespace GroupThink.CRM.Core
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<ICRM_User_BLL, CRM_User_BLL>();//用户管理
            container.RegisterType<ICRM_User_Repository, CRM_User_Repository>();

            container.RegisterType<ICRM_Dept_BLL, CRM_Dept_BLL>();//部门管理
            container.RegisterType<ICRM_Dept_Repository, CRM_Dept_Repository>();

            container.RegisterType<ICRM_Role_BLL, CRM_Role_BLL>();//角色管理
            container.RegisterType<ICRM_Role_Repository, CRM_Role_Repository>();

            container.RegisterType<ICRM_Module_BLL, CRM_Module_BLL>();//模块管理
            container.RegisterType<ICRM_Module_Repository, CRM_Module_Repository>();

            container.RegisterType<ICRM_ModuleOperate_BLL, CRM_ModuleOperate_BLL>();//模块操作管理
            container.RegisterType<ICRM_ModuleOperate_Repository, CRM_ModuleOperate_Repository>();

            container.RegisterType<ICRM_RoleUser_BLL, CRM_RoleUser_BLL>();//角色用户管理
            container.RegisterType<ICRM_RoleUser_Repository, CRM_RoleUser_Repository>();

            container.RegisterType<ICRM_Right_BLL, CRM_Right_BLL>();//right管理
            container.RegisterType<ICRM_Right_Repository, CRM_Right_Repository>();

            container.RegisterType<ICRM_RightOperate_BLL, CRM_RightOperate_BLL>();//CRM_RightOperate管理
            container.RegisterType<ICRM_RightOperate_Repository, CRM_RightOperate_Repository>();

            container.RegisterType<ICRM_Customer_BLL, CRM_Customer_BLL>();//客户管理
            container.RegisterType<ICRM_Customer_Repository, CRM_Customer_Repository>();

            container.RegisterType<ICRM_CustomerConnect_BLL, CRM_CustomerConnect_BLL>();//客户联系人管理
            container.RegisterType<ICRM_CustomerConnect_Repository, CRM_CustomerConnect_Repository>();

            container.RegisterType<ICRM_Type_BLL, CRM_Type_BLL>();//数据字典管理
            container.RegisterType<ICRM_Type_Repository, CRM_Type_Repository>();
        }               
    }
}