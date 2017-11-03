using System.Web.Mvc;

namespace CRM_EasyUI_2016.Areas.TestManager
{
    public class TestManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TestManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TestManager_default",
                "TestManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
