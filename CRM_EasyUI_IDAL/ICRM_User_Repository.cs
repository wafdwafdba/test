using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Model;

namespace GroupThink.CRM.IDAL
{
    public interface ICRM_User_Repository : IRepository<CRM_User, string>
    {
        CRM_User Login(string username, string pwd);
    }
}

