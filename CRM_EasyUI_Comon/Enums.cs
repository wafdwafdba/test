using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupThink.CRM.Common
{
    public enum UserTypeEnum
    {
        全部 = 0,
        管理员 = 1,
        用户 = 2
    }

    public enum UserStateEnum
    {
        正常使用 = 1,
        冻结 = 2,
        停用 = 3,
        离职 = 4
    }


}
