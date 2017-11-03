namespace GroupThink.CRM.BLL.Core
{
    using GroupThink.CRM.Common;
    using GroupThink.CRM.Model;
    using System;
    using System.IO;
    using System.Text;
    using System.Web;

    public static class ExceptionHander
    {
        public static void WriteException(Exception ex)
        {
            try
            {
                using (DBContainer container = new DBContainer())
                {
                    SysException exception2 = new SysException {
                        HelpLink = ex.HelpLink,
                        Message = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        TargetSite = ex.TargetSite.ToString(),
                        Data = ex.Data.ToString(),
                        CreateTime = DateTime.Now
                    };
                    SysException entity = exception2;
                    container.SysException.AddObject(entity);
                    container.SaveChanges();
                }
            }
            catch (Exception exception3)
            {
                try
                {
                    string path = "~/exceptionLog.txt";
                    using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath(path), true, Encoding.Default))
                    {
                        writer.WriteLine((ex.Message + "|" + ex.StackTrace + "|" + exception3.Message + "|" + DateTime.Now.ToString()).ToString());
                        writer.Dispose();
                        writer.Close();
                    }
                }
                catch
                {
                }
            }
        }
    }
}

