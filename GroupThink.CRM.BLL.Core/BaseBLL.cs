namespace GroupThink.CRM.BLL.Core
{
    using GroupThink.CRM.Model;
    using System;

    public abstract class BaseBLL : IDisposable
    {
        protected DBContainer db = new DBContainer();

        protected BaseBLL()
        {
        }

        public void Dispose()
        {
            if (this.db != null)
            {
                this.db.Dispose();
            }
        }
    }
}

