namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_User_BLL : I_BLL<CRM_User_Model, string>
    {
        CRM_User Login(string username, string pwd);

        List<CRM_User_Model> GetListQuery(ref GridPager pager, string userName);


        List<CRM_User_Model> GetList(ref GridPager pager, List<string> users, string UserName, int DeptId, int RoleId, int UserType, int UserState);

        /// <summary>
        /// 根据部门Id得到用户列表（递归）
        /// </summary>
        /// <param name="DeptId"></param>
        /// <returns></returns>
        List<CRM_User_Model> GetAllByDeptId(int DeptId);
    }
}


