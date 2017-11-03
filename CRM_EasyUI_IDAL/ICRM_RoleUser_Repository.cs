using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Model;

namespace GroupThink.CRM.IDAL
{
    public interface ICRM_RoleUser_Repository : IRepository<CRM_RoleUser, string>
    {
        int Delete(int roleId, string userId);
    }
}

