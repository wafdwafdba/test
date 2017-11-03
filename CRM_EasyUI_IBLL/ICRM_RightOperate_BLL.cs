namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_RightOperate_BLL : I_BLL<CRM_RightOperate_Model, string>
    {
        List<CRM_RightOperate_Model> GetAll();
    }
}


