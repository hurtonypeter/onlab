﻿using System.Web;
using System.Web.Optimization;

namespace Perseus
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.10.4*"));

            bundles.Add(new ScriptBundle("~/bundles/knockoutjs").Include(
                        "~/Scripts/knockout-3.1.0.js",
                        "~/Scripts/knockout.mapping-latest.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're 
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatablestools").Include(
                        "~/Scripts/DataTables-1.9.4/extras/TableTools/media/js/jquery.dataTables.js"));


            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/DataTables-1.9.4/media/css/jquery.dataTables.css"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
