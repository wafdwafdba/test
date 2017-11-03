namespace GroupThink.CRM.IBLL
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model.Sys;
    using System;
    using System.Collections.Generic;
    using GroupThink.CRM.Model;

    public interface ICRM_Customer_BLL : I_BLL<CRM_Customer_Model, Guid>
    {
        List<CRM_Customer_Model> GetList_(ref GridPager pager, string customerName, string contactName, string userId, int _isCompany, int _customerSource, int _customerBusiness, DateTime? _beginDate1, DateTime? _endDate1, DateTime? _beginDate2, DateTime? _endDate2, List<string> users);
    }
}


