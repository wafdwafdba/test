using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace GroupThink.CRM.Common
{
    public class Constant
    {
        public const int NoNumber = 0;
        public const string NoString = " - - ";

        /// <summary>
        /// 金额格式化
        /// </summary>
        public const string MoneyFormat = "#0.00";

        public static readonly Microsoft.Practices.Unity.UnityContainer container = new UnityContainer();
    }
}
