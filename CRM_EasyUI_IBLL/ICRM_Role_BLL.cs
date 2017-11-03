namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_Role_BLL : I_BLL<CRM_Role_Model, int>
    {
        /// <summary>
        /// 根据父级ID获取角色
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<CRM_Role_Model> GetList_(int parentId);

        /// <summary>
        /// 根据部门ID和父级ID获取角色
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        List<CRM_Role_Model> GetList_(int deptID, int parentID);

        /// <summary>
        /// 根据部门ID获取角色
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        List<CRM_Role_Model> GetList1(int deptID);

        //根据部门ID获取角色
        List<CRM_Role_Model> GetListByDeptId(int id);

        /// <summary>
        /// 所有
        /// </summary>
        /// <returns></returns>
        List<CRM_Role_Model> GetAll();

        //获取所有部门的信息
        List<P_Dept__Result> GetP_Dept__Result();

    }
}


