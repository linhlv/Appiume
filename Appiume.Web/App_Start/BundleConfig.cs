using System.Web.Optimization;

namespace Appiume.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/App/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/vendor/css")
                    .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/bootstrap-cosmo.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/toastr.min.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/sweetalert/sweet-alert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                );

            bundles.Add(
               new StyleBundle("~/Bundles/App/vendor/css2")
                   .Include("~/Content/material/vendors/bower_components/animate.css/animate.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/vendors/bower_components/material-design-iconic-font/dist/css/material-design-iconic-font.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/vendors/bower_components/bootstrap-sweetalert/lib/sweet-alert.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/vendors/bower_components/angular-loading-bar/src/loading-bar.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/vendors/bower_components/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/css/app.min.1.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/css/app.min.2.css", new CssRewriteUrlTransform())
                   .Include("~/Content/material/css/demo.css", new CssRewriteUrlTransform())

               );


            bundles.Add(new ScriptBundle("~/Bundles/App/vendor/js2")
                .Include(
                    "~/Apm/Framework/scripts/utils/ie10fix.js",
                    // <!-- Core -->
                    //"~/Content/material/vendors/bower_components/jquery/dist/jquery.min.js",

                    "~/Scripts/modernizr-2.8.3.js",
                    "~/Scripts/jquery-2.1.4.min.js",
                    "~/Scripts/jquery-ui-1.11.4.min.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/underscore.min.js",
                    "~/Scripts/moment-with-locales.min.js",
                    "~/Scripts/jquery.blockUI.js",
                    "~/Scripts/toastr.min.js",
                    "~/Scripts/sweetalert/sweet-alert.min.js",
                    "~/Scripts/others/spinjs/spin.js",
                    "~/Scripts/others/spinjs/jquery.spin.js",

                    "~/Scripts/angular.min.js",
                    "~/Scripts/angular-animate.min.js",
                    "~/Scripts/angular-sanitize.min.js",
                    "~/Scripts/angular-ui-router.min.js",
                    "~/Scripts/angular-ui/ui-bootstrap.min.js",
                    "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                    "~/Scripts/angular-ui/ui-utils.min.js",


                    // < !--Angular Modules-- >
                    "~/Content/material/bower_components/angular-ui-router/release/angular-ui-router.min.js",
                    "~/Content/material/bower_components/angular-loading-bar/src/loading-bar.js",
                    "~/Content/material/bower_components/oclazyload/dist/ocLazyLoad.min.js",
                    //"~/Content/material/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",

                    // < !--Common Vendors-- >
                    "~/Content/material/vendors/bower_components/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",
                    //"~/Content/material/vendors/bower_components/bootstrap-sweetalert/lib/sweet-alert.min.js",
                    "~/Content/material/vendors/bower_components/Waves/dist/waves.min.js",
                    "~/Content/material/vendors/bootstrap-growl/bootstrap-growl.min.js",
                    "~/Content/material/vendors/bower_components/ng-table/dist/ng-table.min.js",

                    //< !--Placeholder for IE9-- >
                    //< !--[if IE 9 ]>
                    //  < script src = "vendors/bower_components/jquery-placeholder/jquery.placeholder.min.js" ></ script >
                    //< ![endif]-- >

                    // < !--Using below vendors in order to avoid misloading on resolve -->
                    "~/Content/material/vendors/bower_components/flot/jquery.flot.js",
                    "~/Content/material/vendors/bower_components/flot.curvedlines/curvedLines.js",
                    "~/Content/material/vendors/bower_components/flot/jquery.flot.resize.js",
                    //"~/Content/material/vendors/bower_components/moment/min/moment.min.js",
                    "~/Content/material/vendors/bower_components/fullcalendar/dist/fullcalendar.min.js",
                    "~/Content/material/vendors/bower_components/flot-orderBars/js/jquery.flot.orderBars.js",
                    "~/Content/material/vendors/bower_components/flot/jquery.flot.pie.js",
                    "~/Content/material/vendors/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js",
                    "~/Content/material/vendors/bower_components/angular-nouislider/src/nouislider.min.js",

                    "~/Apm/Framework/scripts/apm.js",
                    "~/Apm/Framework/scripts/libs/apm.jquery.js",
                    "~/Apm/Framework/scripts/libs/apm.toastr.js",
                    "~/Apm/Framework/scripts/libs/apm.blockUI.js",
                    "~/Apm/Framework/scripts/libs/apm.spin.js",
                    "~/Apm/Framework/scripts/libs/apm.sweet-alert.js",
                    "~/Apm/Framework/scripts/libs/angularjs/apm.ng.js"
                    )
               );


            //~/Bundles/App/vendor/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/vendor/js")
                    .Include(
                        "~/Apm/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/json2.min.js",
                        "~/Scripts/modernizr-2.8.3.js",
                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/underscore.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-animate.min.js",
                        "~/Scripts/angular-sanitize.min.js",
                        "~/Scripts/angular-ui-router.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                        "~/Scripts/angular-ui/ui-utils.min.js",





                        "~/Apm/Framework/scripts/apm.js",
                        "~/Apm/Framework/scripts/libs/apm.jquery.js",
                        "~/Apm/Framework/scripts/libs/apm.toastr.js",
                        "~/Apm/Framework/scripts/libs/apm.blockUI.js",
                        "~/Apm/Framework/scripts/libs/apm.spin.js",
                        "~/Apm/Framework/scripts/libs/apm.sweet-alert.js",
                        "~/Apm/Framework/scripts/libs/angularjs/apm.ng.js"
                    )
                );

            //APPLICATION RESOURCES

            //~/Bundles/App/Main/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/Main/css")
                    .IncludeDirectory("~/App/Main", "*.css", true)
                );

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Main/js")
                    .IncludeDirectory("~/App/Main", "*.js", true)
                );
        }
    }
}