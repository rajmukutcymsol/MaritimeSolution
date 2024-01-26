using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Services.Abstract.Requirement;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Utilities;
using System.Configuration;
using TicketingTool.Services.Abstract.User;
using TicketingTool.Services.Concrete.Mapping;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class NewRequestController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IRequirementRepository _requirementRepository;
        private readonly IUserProjectsMapping _UserProjectsMappingRepository;
        private readonly IStatusRepository _statuRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRegionMapping projectRegionMapping;
        public NewRequestController(IMasterRepository masterRepository, IRequirementRepository requirementRepository, IUserProjectsMapping userProjectsMappingRepository, IStatusRepository statuRepository, IUserRepository userRepository, IProjectRegionMapping ProjectRegionMapping)
        {
            this._masterRepository = masterRepository;
            this._requirementRepository = requirementRepository;
            this._UserProjectsMappingRepository = userProjectsMappingRepository;
            this._statuRepository = statuRepository;
            this._userRepository = userRepository;
            this.projectRegionMapping = ProjectRegionMapping;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            bool admin_tru = false;
            string user_pass = Session["access_role"].ToString();
            if (user_pass == "admin" || user_pass == "Developer")
            {
                admin_tru = true;
            }
            ViewBag.AdminTru = admin_tru;

            ViewBag.priority = await _masterRepository.GetMasterInfo<master_priority>((int)MasterType.master_priority);
            ViewBag.status = await _masterRepository.GetMasterInfo<master_status>((int)MasterType.master_status);
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            allMaster.statuses.Insert(0, new master_status { id = Guid.Empty, status_name = "All" });
            ViewBag.statuses = allMaster.statuses;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetAll(vm_search request)
        {
            var requirementType = await _masterRepository.GetMasterInfo<master_requirement>((int)MasterType.master_requirement);
            var requirementId = requirementType.Where(x => x.requirement_type.Equals("New Requirement")).FirstOrDefault().id;
            var result = await _requirementRepository.GetAll<vm_new_request>((int)ManageRequirement_Type.getall, requirementId, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetAll_Filter(List<Guid> request_status)
        {
            vm_search search = new vm_search();
            search.user_name = Session["user_name"].ToString();
            search.access_role = Session["access_role"].ToString();

            var requirementType = await _masterRepository.GetMasterInfo<master_requirement>((int)MasterType.master_requirement);
            var requirementId = requirementType.Where(x => x.requirement_type.Equals("New Requirement")).FirstOrDefault().id;
            var result= await _requirementRepository.GetAll_Filter<vm_new_request>((int)ManageRequirement_Type.getall, requirementId, request_status, search);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var profile = JsonConvert.DeserializeObject<vm_employee>(Session["user_profile"].ToString());
            ViewBag.Requester = profile.Name;
            ViewBag.LM = profile.ManagerName;
            ViewBag.auto_id = await _requirementRepository.GenerateAutoId((int)Generate_AutoId.NewRequest);
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.requirementType = allMaster.requirements.Where(x => x.requirement_type.Equals("New Requirement")).FirstOrDefault().id;
            ViewBag.BooleanDropdown = await _masterRepository.GetMasterInfo<BooleanDropdown>((int)MasterType.boolean_dropdown);
            //allMaster.priorities.Insert(0, new master_priority { id = Guid.Empty, priority_name = "--Select--" });
            allMaster.regions.Insert(0, new master_region { id = null, region_name = "--Select--" });
            allMaster.functions.Insert(0, new master_function { id = null, function_name = "--Select--" });
            allMaster.function_levels.Insert(0, new master_function_level { id = null, function_level = "--Select--" });

            // for projects mappeed
            if (Session["access_role"].ToString() == "admin" || Session["access_role"].ToString() == "Developer")
            {
                // allMaster.projects.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = allMaster.projects;
                allMaster.priorities.Insert(0, new master_priority { id = Guid.Empty, priority_name = "--Select--" });
            }
            else
            {
                vw_user_projects_mapping request = new vw_user_projects_mapping { employee_id = Session["user_name"].ToString() };
                var result = await _UserProjectsMappingRepository.GetUserProjectMappingByEmployeeId<master_project>((int)ManageUserProjectMapping_Type.getall, request);
                //result.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = result;

                if (allMaster.priorities is List<master_priority> priorityList)// && priorityList.Count > 0)
                {
                    priorityList.RemoveAt(0);
                }
                allMaster.priorities.Insert(0, new master_priority { id = Guid.Empty, priority_name = "--Select--" });

            }

            return View(allMaster);
        }

        [HttpPost]
        public async Task<JsonResult> Save(vmProjectRequirement projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();

            List<Attachments> lp = new List<Attachments>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];
                string newFolderName = projectRequirement.auto_id;

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads/ResolutionHUB/NewRequest"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + newFolderName + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + newFolderName + ""));
                }
                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);

                string newFileName = "/uploads/ResolutionHUB/NewRequest/" + newFolderName + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + newFolderName + ""), fl);
                postedFile.SaveAs(path);

                string UploadFolder = "Shared Documents/ResolutionHUB/NewRequest";
                string Filepath = path;

                CommonUtility.SaveOnSite(Filepath, UploadFolder, newFolderName);

                lp.Add(new Attachments()
                {
                    AttachementName = newFileName,
                    AttachementType = "NewRequest"
                });
            }

            projectRequirement.attachment = lp;
            var result = await _requirementRepository.Save<CommonDbResponse>((int)ManageRequirement_Type.insert_requirement, projectRequirement, projectRequirement.created_by);
            if (result.STATUS == true)
            {
                var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                var NewRequestSubject = "New- " + projectRequirement.auto_id + " | " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];
                string to = result.to_email_user;
                string cc = TKTcc;
                if (result.AssigntoPersonEmail != null)
                {
                    cc = cc + ";" + result.AssigntoPersonEmail;
                }
                string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
                string emailBody = @"Dear User,<br/>" +
                                    "Thank you for reaching out to us. We are working on your New Request ID : " + projectRequirement.auto_id + " with following details:- <br/></br>" +
                                    "<b>Date of Request :</b> " + result.date_of_request + "<br/>" +
                                    "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                    "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                                    "<b>Requester Name :</b> " + result.Requester + "<br/></br>";
                                    if (result.AssigntoPersonEmail != null)
                                    {
                                        emailBody += "<b>Request for :</b> " + result.AssigntoPerson_Name + "<br/>";
                                    }
                                    emailBody +="<b>Best Regards<br/>" +
                                    "<b>GDC Resolution Hub.";
                
                //"Link: <a href=\"#\" class=\"visually-hidden focusable\">Open Details </a><br/>" +

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Id = id;
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.BooleanDropdown = await _masterRepository.GetMasterInfo<BooleanDropdown>((int)MasterType.boolean_dropdown);
            allMaster.statuses.Insert(0, new master_status { id = Guid.Empty, status_name = "--Select--" });
            //bool cost_value = true;
            //if (Session["access_role"].ToString() != "admin" && Session["access_role"].ToString() != "Developer")
            //{
            //    cost_value = false;
            //    ViewBag.ShowControl = cost_value;
            //}
            //else
            //{
            //    cost_value = true;
            //    ViewBag.ShowControl = cost_value;
            //}
            var result = await _userRepository.GetDeveloperUsers((int)usp_ManageUser_Type.DevelopersList);
            result.Insert(0, new vm_user_registration { employee_id = null, employee_name = "--Select--" });
            ViewBag.Developers = result;

            var devList = await _userRepository.GetDeveloperUsersforTask((int)usp_ManageUser_Type.DevelopersListfortask);
            ViewBag.DevelopersforTask = devList;

            var testerList = await _userRepository.GetTesterList((int)usp_ManageUser_Type.TesterList);
            testerList.Insert(0, new vm_user_registration { tester_name = "0", employee_name = "--Select--" });
            ViewBag.TesterList = testerList;

            return View(allMaster);
        }
        [HttpGet]
        public async Task<ActionResult> OthersEdit(Guid id)
        {
            ViewBag.Id = id;
            Session["OthersId"] = id;
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.BooleanDropdown = await _masterRepository.GetMasterInfo<BooleanDropdown>((int)MasterType.boolean_dropdown);
            allMaster.statuses.Insert(0, new master_status { id = Guid.Empty, status_name = "--Select--" });

            if (Session["access_role"].ToString() == "admin" || Session["access_role"].ToString() == "Developer")
            {
                // allMaster.projects.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = allMaster.projects;
            }
            else
            {
                vw_user_projects_mapping request = new vw_user_projects_mapping { employee_id = Session["user_name"].ToString() };
                var result = await _UserProjectsMappingRepository.GetUserProjectMappingByEmployeeId<master_project>((int)ManageUserProjectMapping_Type.getall, request);
                ViewBag.ProjectForUser = result;
            }
            return View(allMaster);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _requirementRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetRequirementDetail(Guid id)
        {
            var result = await _requirementRepository.GetRow<project_requirement>((int)ManageRequirement_Type.get_requirement_by_id, id);
            ViewBag.BooleanDropdown = result.is_resolution_offered;
            if (result.shairpoint_url != null)
            {
                Session["shairpoint_url"] = result.shairpoint_url;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetRequestedStatus(Guid id)
        {
            var result = await _requirementRepository.GetRow<project_requirement>((int)ManageRequirement_Type.get_requirement_by_id, id);
            var statusresult = await _statuRepository.GetById(result.request_status);
            //  Session["requestedstatus"] = statusresult.status_name;
            return Json(statusresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(vmProjectRequirement projectRequirement, List<string> FileName)
        {
            projectRequirement.created_by = Session["user_name"].ToString();

            List<Attachments> lp = new List<Attachments>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads/ResolutionHUB/NewRequest"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + ""));
                }
                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);


                string newFileName = "/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + ""), fl);

                postedFile.SaveAs(path);

                string UploadFolder = "Shared Documents/ResolutionHUB/NewRequest";
                string Filepath = path;
                CommonUtility.SaveOnSite(Filepath, UploadFolder, projectRequirement.auto_id);

                lp.Add(new Attachments()
                {
                    AttachementName = newFileName
                     ,
                    AttachementType = "NewRequest"
                });
            }

            projectRequirement.attachment = lp;
            var result = await _requirementRepository.Save<CommonDbResponse>((int)ManageRequirement_Type.update_requirement, projectRequirement, projectRequirement.created_by);
            if (result.STATUS == true)
            {
                if (result.progressResponce.ToString() != "0" || result.AssigntoPersonEmail != null)
                {
                    string dev_email = result.developers_group_email;
                    //string Dateofrequest = projectRequirement.date_of_request.Value.ToString();
                    var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                    var NewRequestSubject = "GDC Resolution Hub Update- " + projectRequirement.auto_id + " | " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];
                    string to = result.to_email_user;
                    string cc = TKTcc;
                    if (result.AssigntoPersonEmail != null)
                    {
                        cc = cc + ";" + result.AssigntoPersonEmail;
                    }
                    string task = "";
                    string subject = NewRequestSubject;
                    string emailBody = "";

                    emailBody = @"Dear User,<br/>" +
                                          "Your Request has been updated please find the details below:- <br/></br>" +
                                          "<b>Date of Request :</b> " + result.date_of_request + "<br/>" +
                                          "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                          "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                                          "<b>Requester Name :</b> " + result.Requester + "<br/>";
                                            if (result.AssigntoPersonEmail != null)
                                            {
                                                emailBody += "<b>Request for :</b> " + result.AssigntoPerson_Name + "<br/>";
                                            }
                                            if (result.progressResponce.ToString() != "0")
                                            {
                                                emailBody += "<b>Status :</b> " + result.progressResponce.ToString() + "<br/><br/>";
                                            }
                                            else {
                                                    emailBody += "<br/><br/>"; 
                                                }
                                            emailBody += "<b>Best Regards<br/>" +
                                          "<b>GDC Resolution Hub.";
                                            

                    //if (result.progressResponce.ToString() == "In-Progress")
                    //{
                    //    if (dev_email != "")
                    //    {
                    //        cc = cc + ";" + dev_email;
                    //        DeveloperTask dt = new DeveloperTask();
                    //        dt.auto_id = projectRequirement.auto_id;
                    //        var taslList = await _requirementRepository.GetDeveloperTaskData<DeveloperTask>((int)usp_DeveloperTask_Type.getall, dt);
                            
                    //        task += "<table style=\"border-collapse:collapse;\"><tr style=\"border: 1px solid #000;\"><th style=\"border: 1px solid #000;\"> Developer Name </th><th style=\"border: 1px solid #000;\"> Task </th></tr>";
                    //        foreach (var item in taslList)
                    //        {
                    //            task += "<tr style=\"border: 1px solid #000;\">";
                    //            task += "<td style=\"border: 1px solid #000;\">" + item.employee_name + "</td>";
                    //            task += "<td style=\"border: 1px solid #000;\">" + item.task_details + "</td>";
                    //            task += "</tr>";

                    //        }
                    //        task += "</table> </br></br>";
                    //        emailBody = @"Dear User,<br/>" +
                    //                  "Your Request has been updated please find the details below:- <br/></br>" +
                    //                  "<b>Date of Request :</b> " + Dateofrequest + "<br/>" +
                    //                  "<b>Project Name :</b> " + result.project_name + "<br/>" +
                    //                  "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                    //                  "<b>Requester Name :</b> " + result.Requester + "<br/>" +
                    //                  "<b>Status :</b> " + result.progressResponce.ToString() + "<br/>" +
                    //                  "<b>Task Details :</b> <br/></br>" +
                    //                    task +
                    //                  "<b>Best Regards<br/>" +
                    //                  "<b>GDC Resolution Hub.";
                    //    }
                    //}
                    //else
                    //{
                    //    emailBody = @"Dear User,<br/>" +
                    //                      "Your Request has been updated please find the details below:- <br/></br>" +
                    //                      "<b>Date of Request :</b> " + Dateofrequest + "<br/>" +
                    //                      "<b>Project Name :</b> " + result.project_name + "<br/>" +
                    //                      "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                    //                      "<b>Requester Name :</b> " + result.Requester + "<br/>" +
                    //                      "<b>Status :</b> " + result.progressResponce.ToString() + "<br/>" +

                    //                      "<b>Best Regards<br/>" +
                    //                      "<b>GDC Resolution Hub.";
                    //}
                   // 
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> GetAllAttachements(Attachments request)
        {
            var result = await _requirementRepository.GetAllAttachements<Attachments>((int)usp_ManageAttachement_Type.getAll, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DeleteAttachement(Guid id)
        {
            var result = await _requirementRepository.DeleteAttachement(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ProjectCustomers(Guid project_name)
        {
            string user_name = Session["user_name"].ToString();
            var result = await _requirementRepository.GetProjectCustomers<master_customer>((int)usp_ManageAttachement_Type.getAll, project_name, user_name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static string EncodeToBase64(string zipPath)
        {
            using (FileStream fs = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
            {
                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                return Convert.ToBase64String(filebytes);
            }
        }
        //public async static void sendMailNewRequest(vmProjectRequirement projectRequirement var result)
        //{
        //    string Dateofrequest = projectRequirement.date_of_request.Value.ToString("dd/MM/yyyy");
        //    var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
        //    var NewRequestSubject = ConfigurationManager.AppSettings["NewRequestSubject"];
        //    string to = "raj.mukut@nokia.com";
        //    string cc = TKTcc;
        //    string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
        //    string emailBody = @"Dear User,<br/>" +
        //                        "Thank you for reaching out to us. We are working on your issue  Request ID : "+ projectRequirement.auto_id + " with following details <br/></br>"+
        //                        "Date of Request: " + Dateofrequest + "<br/>" +
        //                        "Project Name: " + projectRequirement.project_name.Value + "<br/>" +
        //                        "Title: " + projectRequirement.request_title+ "<br/><br/>" +
        //                        "Best Regards<br/>" +
        //                        "GDC Resolution Hub.";
        //    CommonUtility.sendMailNewRequest(to,cc,subject,emailBody);
        //}

        public async Task<ActionResult> ProjectVendors(Guid project_name)
        {
            string user_name = Session["user_name"].ToString();
            var result = await _requirementRepository.GetProjectVendors<vw_tool_usecases_mapping>((int)ManageRequirement_Type.getVendorByProjectId, project_name, user_name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ProjectTechnology(Guid project_name, Guid vendor)
        {
            string user_name = Session["user_name"].ToString();
            var result = await _requirementRepository.GetProjectTechnology<vw_tool_usecases_mapping>((int)ManageRequirement_Type.getTechnologyByProjectId, project_name, vendor, user_name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ProjectNodeType(Guid project_name, Guid vendor, Guid technology)
        {
            string user_name = Session["user_name"].ToString();
            var result = await _requirementRepository.GetProjectNodeType<vw_tool_usecases_mapping>((int)ManageRequirement_Type.getNodeTypeByProjectId, project_name, technology, vendor, user_name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetHistoryByDate(master_update_history master_update_History)
        {
            string htmlcode = "";
            List<master_update_history> lp = new List<master_update_history>();
            lp = await _requirementRepository.GetHistoryByDate<master_update_history>((int)usp_ManageUpdateHistory_Type.getUpdatedHistoryByDate, master_update_History);
            foreach (var item in lp)
            {
                List<master_update_history> itemLists = new List<master_update_history>();
                master_update_History.auto_id = item.auto_id;
                master_update_History.DateofUpdate = item.update_date;
                htmlcode += "<ul class=\"timeline\">";
                htmlcode += "<li class=\"time-label\">";
                htmlcode += "<span class=\"bg-red\">";
                htmlcode += item.DateofUpdate;
                htmlcode += "</span>";
                htmlcode += "</li>";

                itemLists = await _requirementRepository.GetUpdateHistory<master_update_history>((int)usp_ManageUpdateHistory_Type.getUpdatedHistory, master_update_History);
                var mergedRows = itemLists.GroupBy(r => new { r.employee_id, r.times, r.employee_name })
                     .Select(g => new master_update_history
                     {
                         employee_id = g.Key.employee_id,
                         employee_name = g.Key.employee_name,
                         times = g.Key.times,
                         descc = string.Join("| ", g.Select(r => r.descc))
                     })
                     .ToList();
                foreach (var items in mergedRows)
                {
                    var d = items.descc.Split('|');

                    htmlcode += "<li>";
                    htmlcode += "<i class=\"fa fa-history bg-blue\"></i>";
                    htmlcode += "<div class=\"timeline-item\">";
                    htmlcode += "<span class=\"time\"><i class=\"fa fa-clock-o\"></i>" + items.times + "</span>";
                    htmlcode += "<h3 class=\"timeline-header\"><a href = \"#\" > " + items.employee_name + "</a>...</h3>";
                    htmlcode += "<div class=\"timeline-body\">";
                    foreach (var d1 in d)
                    {
                        htmlcode += d1 + "</br>";
                    }
                    htmlcode += "</div>";
                    htmlcode += "</div>";
                    htmlcode += "</li>";
                }
                htmlcode += "</ul>";
            }
            var result = new { html = htmlcode };
            return Json(result, JsonRequestBehavior.AllowGet);
        }//GetHistoryByDate
        public async Task<ActionResult> GetUpdatedHistoryByAutoId(master_update_history master_update_History)
        {
            var result = await _requirementRepository.GetUpdateHistory<master_update_history>((int)usp_ManageUpdateHistory_Type.getUpdatedHistory, master_update_History);
            return Json(result, JsonRequestBehavior.AllowGet);
        }//
        public async Task<ActionResult> GetStatusHistory(master_update_history master_update_History)
        {
            string htmlcode = "";
            List<master_update_history> lp = new List<master_update_history>();
            lp = await _requirementRepository.GetStatusHistory<master_update_history>((int)usp_ManageUpdateHistory_Type.getStatusHistory, master_update_History);
            foreach (var item in lp)
            {
                htmlcode += "<p><b> Date: </b>" + item.DateofUpdate + " <b>Time:</b> " + item.times + " <b> Employee Name: </b>" + item.employee_name + " <b>Status: </b>" + item.FieldValue + " " + "</br>";
            }
            var result = new { html = htmlcode };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateMessageStatus(message_update_status request)
        {
            string user_name = Session["user_name"].ToString();
            request.created_by = user_name;
            var result = await _requirementRepository.SaveUpdateMessageStatus((int)usp_ManageMessageUpdateStatus_Type.insert, request);
            if (result.STATUS == true)
            {
                var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                var NewRequestSubject = "GDC Resolution Hub- Update- " + request.auto_id + " | " + request.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];
                string to = result.to_email_user;
                string cc = TKTcc;
                if (result.AssigntoPersonEmail != null)
                {
                    cc = cc + ";" + result.AssigntoPersonEmail;
                }
                string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
                string emailBody = @"Dear User,<br/>" +
                                  "Your Request has been updated please find the details below:- <br/></br>" +
                                  "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                  "<b>Title :</b> " + request.request_title + "<br/>" +
                                  "<b>Requester Name :</b> " + result.Requester + "<br/></br>";
                                    if (result.AssigntoPersonEmail != null)
                                    {
                                        emailBody += "<b>Request for :</b> " + result.AssigntoPerson_Name + "<br/>";
                                    }
                                    emailBody +="<b>New State :</b> " + result.progressResponce.ToString() + "<br/>" +
                                  "<b>Comment added :</b> " + request.message.ToString() + "</br></br>" +
                                  "<b>Best Regards<br/>" +
                                  "<b>GDC Resolution Hub.";
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetUpdateMessage(string auto_id)
        {
            string user_name = Session["user_name"].ToString();
            var result = await _requirementRepository.GetUpdateMessage((int)usp_ManageMessageUpdateStatus_Type.getbyauto_id, auto_id);
            var obj = new { messages = result };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateOthers(vmProjectRequirement projectRequirement, List<string> FileName)
        {
            projectRequirement.id = Guid.Parse(Session["OthersId"].ToString());
            projectRequirement.created_by = Session["user_name"].ToString();
            List<Attachments> lp = new List<Attachments>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];
                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads/ResolutionHUB/NewRequest"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + ""));
                }
                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);

                string newFileName = "/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/ResolutionHUB/NewRequest/" + projectRequirement.auto_id + ""), fl);

                postedFile.SaveAs(path);

                string UploadFolder = "Shared Documents/ResolutionHUB/NewRequest";
                string Filepath = path;
                CommonUtility.SaveOnSite(Filepath, UploadFolder, projectRequirement.auto_id);

                lp.Add(new Attachments()
                {
                    AttachementName = newFileName
                     ,
                    AttachementType = "NewRequest"
                });
            }

            projectRequirement.attachment = lp;
            var result = await _requirementRepository.UpdateOthers<CommonDbResponse>((int)ManageRequirement_Type.updatebyothers, projectRequirement, projectRequirement.created_by);
            if (result.STATUS == true)
            {
                if (result.progressResponce.ToString() != "0")
                {
                    //string Dateofrequest = projectRequirement.date_of_request.Value.ToString("dd/MM/yyyy");
                    var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                    var NewRequestSubject = "GDC Resolution Hub- Update- " + projectRequirement.auto_id + " | " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];
                    string to = result.to_email_user;
                    string cc = TKTcc;
                    string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
                    string emailBody = @"Dear User,<br/>" +
                                      "Your Request has been updated please find the details below:- <br/></br>" +
                                      "<b>Date of Request :</b> " + result.date_of_request + "<br/>" +
                                      "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                      "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                                      "<b>Requester Name :</b> " + result.Requester + "<br/>" +
                                      "<b>Status :</b> " + result.progressResponce.ToString() + "<br/></br>" +
                                      "<b>Best Regards<br/>" +
                                      "<b>GDC Resolution Hub.";
                    
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SaveDevTask(DeveloperTask _developerTask)
        {
            var result = await _requirementRepository.SaveDevTask<CommonDbResponse>((int)usp_DeveloperTask_Type.save, _developerTask);
            string dev_email, cc;
            if (result.STATUS==true)
            {
                dev_email = result.developers_group_email;
                cc = ConfigurationManager.AppSettings["TKTcc"];
                var NewRequestSubject = "GDC Resolution Hub- Update- " + _developerTask.auto_id + " | " + _developerTask.task_details + "";
                string emailBody = "";
                string task = "";
                //if (result.progressResponce.ToString() == "In-Progress")
                //{
                    if (dev_email != "")
                    {
                        //cc = cc + ";" + dev_email;
                        //DeveloperTask dt = new DeveloperTask();
                        //dt.auto_id = _developerTask.auto_id;
                        //var taslList = await _requirementRepository.GetDeveloperTaskData<DeveloperTask>((int)usp_DeveloperTask_Type.getall, dt);

                        //task += "<table style=\"border-collapse:collapse;\"><tr style=\"border: 1px solid #000;\"><th style=\"border: 1px solid #000;\"> Developer Name </th><th style=\"border: 1px solid #000;\"> Task </th></tr>";
                        //foreach (var item in taslList)
                        //{
                        //    task += "<tr style=\"border: 1px solid #000;\">";
                        //    task += "<td style=\"border: 1px solid #000;\">" + item.employee_name + "</td>";
                        //    task += "<td style=\"border: 1px solid #000;\">" + item.task_details + "</td>";
                        //    task += "</tr>";

                        //}
                        //task += "</table> </br></br>";
                        emailBody = @"Dear "+result.employee_name+",<br/>" +
                                  "Your Request has been updated please find the details below:- <br/></br>" +
                                  "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                  "<b>Requester Name :</b> " + result.Requester + "<br/>" +
                                  "<b>Request Id :</b> " + _developerTask.auto_id + "<br/>" +
                                  "<b>Task Title :</b> " + _developerTask .task_details+ "<br/>" +
                                  "<b>Status :</b> " + result.progressResponce.ToString() + "<br/></br>" +
                                  //"<b>Task Details :</b> <br/></br>" +
                                  //  task +
                                  "<b>Best Regards<br/>" +
                                  "<b>GDC Resolution Hub.";
                    }
                //}
               // CommonUtility.sendMail(dev_email, cc, NewRequestSubject, emailBody);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetDeveloperTaskData(DeveloperTask request)
        {
            var result = await _requirementRepository.GetDeveloperTaskData<DeveloperTask>((int)usp_DeveloperTask_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetDevTaskByID(string TaskId)
        {
            var result = await _requirementRepository.GetDevTaskByID<vm_user_registration>((int)usp_ManageDeveloper_Type.getById, TaskId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> UpdateDevTask(DeveloperTask _DeveloperTask)
        {
            var result = await _requirementRepository.UpdateDevTask<CommonDbResponse>((int)usp_ManageDeveloper_Type.updatetask, _DeveloperTask);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> PullChat(ChatRecord _chat)
        {
            string user_name = Session["user_name"].ToString();
            _chat.ChatCreatedBy = user_name;
            var result = await _requirementRepository.PullChat<CommonDbResponse>((int)usp_Chat_Type.save,_chat);              
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetChatData(ChatRecord chat)
        {
            string htmlcode = "";
            List<ChatRecord> lp = new List<ChatRecord>();
            lp = await _requirementRepository.GetChatData<ChatRecord>((int)usp_Chat_Type.getall, chat);
            if (lp.Count>0)
            {
                htmlcode += "<div class=\"direct-chat-messages\" style=\"margin-right: 9px;\">";
                int count = 0;
                foreach (var item in lp)
                {
                    if (count % 2 == 0)
                    {
                        htmlcode += "<div class=\"direct-chat-msg\">";
                        htmlcode += "<div class=\"direct-chat-infos clearfix\">";
                        htmlcode += "<span class=\"direct-chat-name float-left\">" + item.employee_name + "</span>";
                        htmlcode += "<span class=\"direct-chat-timestamp float-right\">" +"  "+ item.ChatTime + "</span>";
                        htmlcode += "</div>";
                        htmlcode += "<div style=\"background-color: antiquewhite;\">";
                        htmlcode += item.ChatMessage;
                        htmlcode += "</div>";
                        htmlcode += "</div>";
                        count++;
                    }
                    else
                    {
                        htmlcode += "<div class=\"direct-chat-msg right\">";
                        htmlcode += "<div class=\"direct-chat-infos clearfix\">";
                        htmlcode += "<span class=\"direct-chat-name float-right\">" + item.employee_name + "</span>";
                        htmlcode += "<span class=\"direct-chat-timestamp float-left\">" + "  " + item.ChatTime + "</span>";
                        htmlcode += "</div>";
                        htmlcode += "<div style=\"background-color: #f4f0eb;\">";
                        htmlcode += item.ChatMessage;
                        htmlcode += "</div>";
                        htmlcode += "</div>";
                        count++;
                    }
                }
                htmlcode += "</div>";
            }
            else
            {
                htmlcode = "";
            }
           var result = new { html = htmlcode };
            return Json(result, JsonRequestBehavior.AllowGet);
        }//GetHistoryByDate
        public async Task<ActionResult> DeleteTask(string TaskId)
        {
            var result = await _requirementRepository.DeleteTask(TaskId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetRegionByProjectID(vm_project_region_mapping request)
        {
            var result = await projectRegionMapping.GetById<vm_project_region_mapping>((int)ProjectCustomerMapping_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetUserList(string prefix)
        {
            //var testerList = await _userRepository.GetTesterList((int)usp_ManageUser_Type.TesterList);

            var customers = await _userRepository.GetUsersByName<vm_user_registration>((int)usp_ManageUser_Type.getbyname, prefix);
            return Json(customers);
        }
        public async Task<JsonResult> GetUserById(string employeeId)
        {
            var result = await _userRepository.GetUserById((int)usp_ManageUser_Type.GetUserByIdNew, employeeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetUserByName(string employee_name)
        {
            var result = await _userRepository.GetUserByName((int)usp_ManageUser_Type.getbyname, employee_name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}