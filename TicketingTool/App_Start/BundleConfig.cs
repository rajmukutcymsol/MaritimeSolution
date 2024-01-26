using System.Web.Optimization;

namespace TicketingTool
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                    "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                    "~/Content/bower_components/jvectormap/jquery-jvectormap.css",
                    "~/Content/dist/css/AdminLTE.min.css",
                    "~/Content/dist/css/skins/_all-skins.min.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include("~/Content/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Content/bower_components/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ThemeChanges").Include(
                     "~/Content/dist/js/adminlte.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/NewRequests").Include(
                     "~/Content/custom_scripts/project_requirement/new_request/manage_requirement.js"));

            bundles.Add(new ScriptBundle("~/bundles/EditNewRequests").Include(
                     "~/Content/custom_scripts/project_requirement/new_request/project_requirement_edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/NewRequestList").Include(
                     "~/Content/custom_scripts/project_requirement/new_request/project_requirement_list.js"));

            bundles.Add(new ScriptBundle("~/bundles/ChangeRequest").Include(
            "~/Content/custom_scripts/project_requirement/change_request/change_request_requirement.js"));

            bundles.Add(new ScriptBundle("~/bundles/EditChangeRequest").Include(
                     "~/Content/custom_scripts/project_requirement/change_request/change_request_edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/ChangeRequestList").Include(
                     "~/Content/custom_scripts/project_requirement/change_request/change_request_requirementlist.js"));

            bundles.Add(new ScriptBundle("~/bundles/IssuesRequest").Include(
           "~/Content/custom_scripts/project_requirement/issues/problem_request.js"));

            bundles.Add(new ScriptBundle("~/bundles/IssuesEditRequest").Include(
                     "~/Content/custom_scripts/project_requirement/issues/EditIssues.js"));

            bundles.Add(new ScriptBundle("~/bundles/IssuesList").Include(
                     "~/Content/custom_scripts/project_requirement/issues/problem_request_list.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                    "~/Content/custom_scripts/common/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include("~/Content/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/OthersEditNewRequests").Include(
                    "~/Content/custom_scripts/project_requirement/new_request/manage_others_edit.js"));
            bundles.Add(new ScriptBundle("~/bundles/OthersEditChangeRequest").Include(
                  "~/Content/custom_scripts/project_requirement/change_request/manage_others_edit.js"));
            bundles.Add(new ScriptBundle("~/bundles/OthersEditIssues").Include(
                 "~/Content/custom_scripts/project_requirement/issues/manage_others_edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard").Include(
                 "~/Content/custom_scripts/settings/Dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/EditCommonMaster").Include(
                "~/Content/custom_scripts/settings/CommonMaster_Edit.js"));
            bundles.Add(new ScriptBundle("~/bundles/InBound_Edit").Include(
              "~/Content/custom_scripts/settings/InBound_Edit.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
