using System.Web;
using System.Web.Optimization;

namespace TMall.UI.ManageSite
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/libs/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/libs/jquery.unobtrusive*",
                        "~/Scripts/libs/jquery.validate*"));

            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/dwzjs").Include(
                     "~/Scripts/libs/dwz.min.js",
                     "~/Scripts/libs/dwz.regional.zh.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/themes/default/style.css",
                     "~/Content/themes/css/core.css"));

            bundles.Add(new ScriptBundle("~/bundles/dwzdebugjs").Include(
                      "~/Scripts/libs/dwz.core.js",
                      "~/Scripts/libs/dwz.util.date.js",
                      "~/Scripts/libs/dwz.validate.method.js",
                      "~/Scripts/libs/dwz.regional.zh.js",
                      "~/Scripts/libs/dwz.barDrag.js",
                      "~/Scripts/libs/dwz.drag.js",
                      "~/Scripts/libs/dwz.tree.js",
                      "~/Scripts/libs/dwz.accordion.js",
                      "~/Scripts/libs/dwz.ui.js",
                      "~/Scripts/libs/dwz.theme.js",
                      "~/Scripts/libs/dwz.switchEnv.js",
                      "~/Scripts/libs/dwz.alertMsg.js",
                      "~/Scripts/libs/dwz.contextmenu.js",
                      "~/Scripts/libs/dwz.navTab.js",
                      "~/Scripts/libs/dwz.tab.js",
                      "~/Scripts/libs/dwz.resize.js",
                      "~/Scripts/libs/dwz.dialog.js",
                      "~/Scripts/libs/dwz.dialogDrag.js",
                      "~/Scripts/libs/dwz.sortDrag.js",
                      "~/Scripts/libs/dwz.cssTable.js",
                      "~/Scripts/libs/dwz.stable.js",
                      "~/Scripts/libs/dwz.taskBar.js",
                      "~/Scripts/libs/dwz.ajax.js",
                      "~/Scripts/libs/dwz.pagination.js",
                      "~/Scripts/libs/dwz.database.js",
                      "~/Scripts/libs/dwz.datepicker.js",
                      "~/Scripts/libs/dwz.effects.js",
                      "~/Scripts/libs/dwz.panel.js",
                      "~/Scripts/libs/dwz.checkbox.js",
                      "~/Scripts/libs/dwz.history.js",
                      "~/Scripts/libs/dwz.combox.js",
                      "~/Scripts/libs/dwz.print.js"));

            bundles.Add(new ScriptBundle("~/bundles/seajs").Include(
                        "~/Scripts/sea-modules/seajs/2.1.1/sea.js",
                        "~/Scripts/seajs-config.js"
                        ));
        }
    }
}