namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_Dept_BLL : I_BLL<CRM_Dept_Model, int>
    {
        /// <summary>
        /// 根据父级ID获取列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<CRM_Dept_Model> GetList_(int parentId);

        /// <summary>
        /// 根据部门父ID递归出下级部门
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<CRM_Dept_Model> GetAllByParentId(int parentId);

        List<CRM_Dept_Model> GetAll();

        /// <summary>
        /// 根据父级Id获取列表
        /// </summary>
        /// <param name="parentId">父级Id</param>          
        /// <returns>模型列表</returns>
        List<CRM_Dept_Model> GetListByParentId(int parentId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteAll(List<int> id);

    }
}


