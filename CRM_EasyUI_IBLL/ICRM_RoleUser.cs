namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_RoleUser_BLL : I_BLL<CRM_RoleUser_Model, string>
    {
        List<CRM_RoleUser_Model> GetAll();

        bool Delete(int roleId, string userId);

        bool IsExists(string userId, int roleId);
    }
}


