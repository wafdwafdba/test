namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_Module_BLL : I_BLL<CRM_Module_Model, string>
    {
        /// <summary>
        /// 根据父级Id获取模块
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<CRM_Module_Model> GetList(string parentId);

        /// <summary>
        /// 根绝角色ID获取相关的模块
        /// </summary>
        /// <returns></returns>
        List<CRM_Module_Model> GetListByRoleID();


        /// <summary>
        /// 根据父级Id和角色ID获取模块
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<CRM_Module_Model> GetList(string parentId,int RoleID);
    }
}


