using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace GroupThink.CRM.Common
{
    public class ResultHelper
    {
        /// <summary>
        /// 短信定时器开关，true 可执行 false 不可执行
        /// </summary>
        public static bool SMSTimerStatus = true; //false

        public static string NewId
        {
            get
            {
                string id = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                string guid = Guid.NewGuid().ToString().Replace("-", "");
                id += guid.Substring(0, 10);
                return id;
            }
        }

        public static string NewTimeId
        {
            get
            {
                string id = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                return id;
            }
        }

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubValue(string value, int length)
        {
            if (value.Length > length)
            {
                value = value.Substring(0, length);
                value += "...";
                return NoHtml(value);
            }
            else
            {
                return NoHtml(value);
            }
        }


        //还原的时候
        public static string InputText(string inputString)
        {

            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();
                //if (inputString.Length > maxLength) 
                //inputString = inputString.Substring(0, maxLength); 
                inputString = inputString.Replace("<br>", "\n");
                inputString = inputString.Replace("&", "&amp");
                inputString = inputString.Replace("'", "''");
                inputString = inputString.Replace("<", "&lt");
                inputString = inputString.Replace(">", "&gt");
                inputString = inputString.Replace("chr(60)", "&lt");
                inputString = inputString.Replace("chr(37)", "&gt");
                inputString = inputString.Replace("\"", "&quot");
                inputString = inputString.Replace(";", ";");

                return inputString;
            }
            else
            {
                return "";
            }

        }
        //添加的时候
        public static string OutputText(string outputString)
        {

            if ((outputString != null) && (outputString != String.Empty))
            {
                outputString = outputString.Trim();
                outputString = outputString.Replace("&amp", "&");
                outputString = outputString.Replace("''", "'");
                outputString = outputString.Replace("&lt", "<");
                outputString = outputString.Replace("&gt", ">");
                outputString = outputString.Replace("&lt", "chr(60)");
                outputString = outputString.Replace("&gt", "chr(37)");
                outputString = outputString.Replace("&quot", "\"");
                outputString = outputString.Replace(";", ";");
                outputString = outputString.Replace("\n", "<br>");
                return outputString;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHtml(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&hellip;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&mdash;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;

        }
        /// <summary>
        /// 格式化文本（防止SQL注入）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Formatstr(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex10 = new System.Text.RegularExpressions.Regex(@"select", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex11 = new System.Text.RegularExpressions.Regex(@"update", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex12 = new System.Text.RegularExpressions.Regex(@"delete", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex10.Replace(html, "s_elect");
            html = regex11.Replace(html, "u_pudate");
            html = regex12.Replace(html, "d_elete");
            html = html.Replace("'", "’");
            html = html.Replace("&nbsp;", " ");
            return html;
        }
        /// <summary>
        /// 检查SQL语句合法性
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool ValidateSQL(string sql, ref string msg)
        {
            if (sql.ToLower().IndexOf("delete") > 0)
            {
                msg = "查询参数中含有非法语句DELETE";
                return false;
            }
            if (sql.ToLower().IndexOf("update") > 0)
            {
                msg = "查询参数中含有非法语句UPDATE";
                return false;
            }

            if (sql.ToLower().IndexOf("insert") > 0)
            {
                msg = "查询参数中含有非法语句INSERT";
                return false;
            }
            return true;
        }
        //获取当前时间
        public static DateTime NowTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        //获取空时间
        public static DateTime NullTime
        {
            get
            {
                return Convert.ToDateTime("1901-01-01 00:01:01");
            }
        }

        /// <summary>
        /// 将日期转换成字符串
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>字符串</returns>
        public static string DateTimeConvertString(DateTime? dt)
        {
            if (dt == null)
            {
                return "";
            }
            else
            {
                return Convert.ToDateTime(dt.ToString()).ToShortDateString();
            }
        }
        /// <summary>
        /// 将字符串转换成日期
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>日期</returns>
        public static DateTime? StringConvertDatetime(string str)
        {
            if (str == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return Convert.ToDateTime(str);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 将错误写入exceptionLog.txt
        /// </summary>
        /// <param name="strMess">错误</param>
        public static void ErrorLog(string strMess)
        {
            try
            {
                string path = "~/exceptionLog.txt";
                using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(path), true, Encoding.Default))
                {
                    writer.WriteLine("==============================================");

                    writer.WriteLine(strMess);
                    writer.Dispose();
                    writer.Close();
                }
            }
            catch
            {
            }
        }

        ///// <summary>
        ///// 写入调试日志
        ///// </summary>
        ///// <param name="strMess">分布信息</param>
        ///// <param name="title">标题</param>
        //public static void debugLog(string strMess, string title = "")
        //{
        //    bool openDebugLog = ConfigurationManager.AppSettings["OpenDebugLog"].GetBool();
        //    if (!openDebugLog) return;
        //    try
        //    {
        //        string path = "~/debugLog.txt";
        //        using (StreamWriter writer = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(path), true, Encoding.Default))
        //        {
        //            if (title != "")
        //            {
        //                writer.WriteLine("====================【" + DateTime.Now.ToString() + "】" + title + "====================");
        //            }
        //            writer.WriteLine("【" + DateTime.Now.ToString() + "】" + strMess);
        //            writer.Dispose();
        //            writer.Close();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    ErrorLog("debugLog" + DateTime.Now.ToString() + ":" + strMess);
        //}

        public static string GetUserIP()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static bool Islicense = false;
        public static void LoadLicense()
        {
          #if WithLicense
            Islicense = false;
            string strMess = string.Empty;

            try
            {
                string strFilePath = Path.Combine(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "GroupThink.license");
                StringBuilder sb = new StringBuilder();

                strMess = string.Format("URL:{0}||Host:{1}||Time:{2}", System.Web.HttpContext.Current.Request.Url.ToString(), System.Web.HttpContext.Current.Request.Url.Host.ToString(), DateTime.Now.ToString());
                if (File.Exists(strFilePath))
                {
                    StreamReader srReadFile = new StreamReader(strFilePath);
                    while (!srReadFile.EndOfStream)
                    {
                        sb.Append(srReadFile.ReadLine());
                    }
                    srReadFile.Close();

                    string strResult = "";
                    if (sb.ToString().Length > 0)
                    {
                        strResult = DESEncrypt.Decrypt(sb.ToString());
                    }
                    string[] strkey = strResult.Replace("www.shopnum1QQ.com", "").Split(new char[] { ',' });
                    if (strkey.Length > 1)
                    {
                        if (System.Web.HttpContext.Current.Request.Url.Host.ToString().ToLower() == strkey[0].ToString().ToLower()) //域名相等
                        {
                            if (Convert.ToDateTime(strkey[1].ToString()) >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) //到期时间大于小于当前日期
                            {
                                Islicense = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Islicense = false;
                strMess = string.Format("解析文件出错:{0}||Time:{1}", ex.ToString(), DateTime.Now.ToString());
                ErrorLog(strMess);
            }
#else
            Islicense = true;
            return;
#endif
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 检查短信DLL文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool Modules_SMSIsExist()
        {
            if (CheckFileExists("GroupThink.CRM.SMS.dll"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查邮件DLL文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool Modules_EmailIsExist()
        {
            if (CheckFileExists("GroupThink.CRM.E_Mail.dll"))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 检查DLL文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckFileExists(string strFileName)
        {
            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/bin/{0}", strFileName))))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回当前第几周
        /// <para>国内计算方式　星期一为第一天</para>
        /// </summary>
        /// <returns></returns>
        public static int GetWeekOfYear()
        {
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);

            int currentDay = DateTime.Today.DayOfYear;
            if ((int)DateTime.Now.DayOfWeek == 0)
            {
                currentDay--;
            }
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        /// <summary>
        /// 获取中文星期
        /// </summary>
        /// <param name="Index">数字</param>
        /// <returns>string</returns>
        public static string GetWeekInfo(int Index)
        {
            switch (Index)
            {
                case 1: return "星期一";
                case 2: return "星期二";
                case 3: return "星期三";
                case 4: return "星期四";
                case 5: return "星期五";
                case 6: return "星期六";
                case 7: return "星期日";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 格式化字符串，例如将"shan,Silver,Snow,Star," 格式化为 "'shan','Silver','Snow','Star'"
        /// </summary>
        /// <param name="strOld">需要处理的字符串</param>
        /// <returns>string</returns>
        public static string StringSqlInFormat(string strOld)
        {
            strOld = strOld.Trim(',');

            string[] strs = strOld.Split(',');
            StringBuilder sb = new StringBuilder();

            foreach (string item in strs)
            {
                if (item.Trim().Length > 0)
                {
                    sb.AppendFormat("'{0}',", item.Trim());
                }
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 根据总数和每页条数获取总页数
        /// </summary>
        /// <param name="total">总条数</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>int</returns>
        public static int GetTotalPage(int total, int pageSize)
        {
            if (pageSize == 0)
            {
                return 0;
            }
            else
            {
                if (total % pageSize > 0)
                {
                    return total / pageSize + 1;
                }
                else
                {
                    return total / pageSize;
                }
            }
        }


        /// <summary>
        /// 获取我的提醒类型
        /// </summary>
        /// <param name="typeId">类型Id</param>
        /// <returns></returns>
        public static string GetMyRemindType(int typeId)
        {
            switch (typeId)
            {
                case 1: return "跟进客户";
                case 2: return "合同到期";
                case 3: return "联系人生日";
                case 4: return "非业务提醒";
                default:
                    return "";
            }
        }
    }
}