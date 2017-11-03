namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_ModuleOperate_BLL : I_BLL<CRM_ModuleOperate_Model, string>
    {
        List<CRM_ModuleOperate_Model> GetList(ref GridPager pager, string queryStr);
    }
}


