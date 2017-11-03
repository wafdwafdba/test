using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupThink.CRM.Common
{
    public class JsonHandler
    {
        public static JsonMessage CreateMessage(int ptype, string pmessage)
        {
            return new JsonMessage { type = ptype, message = pmessage };
        }

        public static JsonMessage CreateMessage(int ptype, string pmessage, string pvalue)
        {
            return new JsonMessage { type = ptype, message = pmessage, value = pvalue };
        }
    }
}
