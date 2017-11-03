using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Model;

namespace GroupThink.CRM.IDAL
{
    public interface ICRM_Dept_Repository : IRepository<CRM_Dept, int>
    {
        List<P_Dept__Result> GetListFullName(DBContainer db);

        List<P_Dept_Result> GetList1(DBContainer db);

        int DeleteAll(List<int> ids);
    }
}

