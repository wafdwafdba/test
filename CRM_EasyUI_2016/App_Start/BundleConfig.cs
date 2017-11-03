using System.Web;
using System.Web.Optimization;
using System.Collections.Generic;
using System.IO;

namespace CRM_EasyUI_2016
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region JS
            ////加载easui,加载菜单 js
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.8.0.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery_from").Include("~/Scripts/jquery.form.js"));
           
           // //加载JQuery插件
           // bundles.Add(new ScriptBundle("~/bundles/easyui/jqplus").Include(
           //"~/Scripts/easyui/jquery.easyui.plus.js"));

            //jquery验证插件
            bundles.Add(new Bundle("~/bundles/jquery_val").Include("~/Scripts/jquery.validate.min.js"));

            bundles.Add(new Bundle("~/bundles/jquery_val_uno").Include("~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new Bundle("~/bundles/jquery_uno_ajax").Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));

            var bundle = new Bundle("~/bundles/jquery_val_plus");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle.Include(
                 "~/Scripts/jquery.validate.unobtrusive.min.js",
                 "~/Scripts/jquery.unobtrusive-ajax.min.js"
                );//"~/Scripts/jquery.validate.min.js" 加入后js出错
            bundles.Add(bundle);


            //加载上传插件 js
            bundles.Add(new ScriptBundle("~/bundles/filejs").Include(
                     "~/Scripts/fileinput/fileinput.js"));
            //加载树形插件 js
            bundles.Add(new ScriptBundle("~/bundles/ztreejs").Include(
           "~/Scripts/ztree/jquery.ztree.core-3.5.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/outlook").Include("~/Scripts/outlook.js"));
            #endregion


            #region CSS

            bundles.Add(new StyleBundle("~/Content/formcss/css").Include(
           "~/Content/formcss/form.css"));
            //加载easui css
            bundles.Add(new StyleBundle("~/Content/easyuicss").Include(
                      "~/Content/easyui/easyui.css"));
            //加载上传插件 css
            bundles.Add(new StyleBundle("~/Content/filecss").Include(
                   "~/Content/fileinput/fileinput.css"));
            //加载图标图图片 css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/icon/fontIcon.css"));
            //加载树形插件 css
            bundles.Add(new StyleBundle("~/Content/ztreecss").Include(
                "~/Scripts/ztree/zTreeStyle/zTreeStyle.css"));

            #endregion
        }
    }
    /// <summary>
    /// 按添加顺序加载文件，Bundle默认按文件名字母加载文件
    /// </summary>
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return files;
        }
    }
}