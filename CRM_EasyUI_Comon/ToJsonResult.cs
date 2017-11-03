namespace GroupThink.CRM.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using System.Collections;
    using System.Collections.Generic;

    public class ToJsonResult : JsonResult
    {
        private const string error = "该请求已被封锁，因为敏感信息透露给第三方网站，这是一个GET请求时使用的。为了可以GET请求，请设置JsonRequestBehavior AllowGet。";

        private string ConvertJsonDateToDateString(Match m)
        {
            string pattern = @"\d";
            char[] chArray = m.Value.ToCharArray();
            StringBuilder builder = new StringBuilder();
            Regex regex = new Regex(pattern);
            for (int i = 0; i < chArray.Length; i++)
            {
                if ((chArray[i].ToString() == "-") || regex.IsMatch(chArray[i].ToString()))
                {
                    builder.Append(chArray[i]);
                }
            }
            DateTime time = new DateTime(0x7b2, 1, 1);
            return time.AddMilliseconds((double) long.Parse(builder.ToString())).ToLocalTime().ToString(@"yyyy-MM-dd HH:mm:ss");
        }

        public static string ConvertString(string str)
        {
            //特殊字符
            List<string> str1 = new List<string>() { "“", "”", "\"" };
            List<string> str2 = new List<string>() { "'", "'", "'" };
            for(int i=0;i<str1.Count;i++)
            {
             if (str.Contains(str1[i]))
                {
                    str = str.Replace(str1[i], str2[i]);
                }
            }
       
            return str;

        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("context");
            }
            if ((base.JsonRequestBehavior == JsonRequestBehavior.DenyGet) && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("该请求已被封锁，因为敏感信息透露给第三方网站，这是一个GET请求时使用的。为了可以GET请求，请设置JsonRequestBehavior AllowGet。");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(base.ContentType))
            {
                response.ContentType = base.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (base.ContentEncoding != null)
            {
                response.ContentEncoding = base.ContentEncoding;
            }
            if (base.Data != null)
            {
                string input = new JavaScriptSerializer().Serialize(base.Data);
                string pattern = @"\\/Date\(-?\d+\)\\/";
                MatchEvaluator evaluator = new MatchEvaluator(this.ConvertJsonDateToDateString);               
                input = new Regex(pattern).Replace(input, evaluator);
                input = input.Replace("null", " \"\" ");
                input = input.Replace("00:00:00", "");
                response.Write(input);
            }
           
             
        }

        public string FormatStr { get; set; }
    }
}

