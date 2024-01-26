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
using System.Configuration;
using TicketingTool.Utilities;
using TicketingTool.Services.Abstract.User;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class IssueRequestController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IRequirementRepository _requirementRepository;
        private readonly IUserProjectsMapping _UserProjectsMappingRepository;
        private readonly IStatusRepository _statuRepository;
        private readonly IUserRepository _userRepository;

        private readonly IResCat1Repository resCat1Repository;
        private readonly IResCat2Repository resCat2Repository;
        private readonly IResCat3Repository resCat3Repository;
        public IssueRequestController(IMasterRepository masterRepository, IRequirementRepository requirementRepository, IUserProjectsMapping userProjectsMappingRepository, IStatusRepository statuRepository ,IUserRepository userRepository, IResCat1Repository ResCat1Repository, IResCat2Repository ResCat2Repository, IResCat3Repository ResCat3Repository)
        {
            this._masterRepository = masterRepository;
            this._requirementRepository = requirementRepository;
            this._UserProjectsMappingRepository = userProjectsMappingRepository;
            this._statuRepository = statuRepository;
            this._userRepository = userRepository;

            this.resCat1Repository = ResCat1Repository;
            this.resCat2Repository = ResCat2Repository;
            this.resCat3Repository = ResCat3Repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            bool admin_tru = false;
            string user_pass = Session["access_role"].ToString();
            if (user_pass == "admin" || user_pass == "Developer" ||user_pass== "SupportTeam")
            {
                admin_tru = true;
            }
            ViewBag.AdminTru = admin_tru;

            ViewBag.priority = await _masterRepository.GetMasterInfo<master_priority>((int)MasterType.master_priority);
            //ViewBag.priority.Insert(0, new master_priority { id = Guid.Empty, priority_name = "--Select--" });
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
            var requirementId= requirementType.Where(x => x.requirement_type.Equals("Issue / Problem")).FirstOrDefault().id;
            var result = await _requirementRepository.GetAll<vm_issue_list>((int)ManageRequirement_Type.get_problems, requirementId, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetAll_Filter(List<Guid> request_status)
        {
            vm_search search = new vm_search();
            search.user_name = Session["user_name"].ToString();
            search.access_role = Session["access_role"].ToString();

            var requirementType = await _masterRepository.GetMasterInfo<master_requirement>((int)MasterType.master_requirement);
            var requirementId = requirementType.Where(x => x.requirement_type.Equals("Issue / Problem")).FirstOrDefault().id;
            var result = await _requirementRepository.GetAll_Filter<vm_new_request>((int)ManageRequirement_Type.getall, requirementId, request_status, search);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var profile = JsonConvert.DeserializeObject<vm_employee>(Session["user_profile"].ToString());
            ViewBag.Requester = profile.Name;
            ViewBag.LM = profile.ManagerName;
            ViewBag.auto_id = await _requirementRepository.GenerateAutoId((int)Generate_AutoId.IssuesRequest);
            var requirementType = await _masterRepository.GetMasterInfo<master_requirement>((int)MasterType.master_requirement);
            ViewBag.master_tool=await _masterRepository.GetMasterInfo<master_tool>((int)MasterType.master_tool);
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.priority = await _masterRepository.GetMasterInfo<master_priority>((int)MasterType.master_priority);
            ViewBag.masterresolutioncategory = allMaster.masterresolutioncategory;

            ViewBag.requirementType = requirementType.Where(x => x.requirement_type.Equals("Issue / Problem")).FirstOrDefault().id;
            ViewBag.BooleanDropdown = await _masterRepository.GetMasterInfo<BooleanDropdown>((int)MasterType.boolean_dropdown);
            //ViewBag.priority.Insert(0, new master_priority { id = null, priority_name = "--Select--" });

            // for projects mappeed
            if (Session["access_role"].ToString() == "admin" || Session["access_role"].ToString() == "Developer")
            {
                allMaster.projects.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = allMaster.projects;
                ViewBag.priority.Insert(0, new master_priority { id = null, priority_name = "--Select--" });
            }
            else
            {
                vw_user_projects_mapping request = new vw_user_projects_mapping { employee_id = Session["user_name"].ToString() };
                var result = await _UserProjectsMappingRepository.GetUserProjectMappingByEmployeeId<master_project>((int)ManageUserProjectMapping_Type.getall, request);
               // result.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = result;

                if (ViewBag.priority is List<master_priority> priorityList)// && priorityList.Count > 0)
                {
                    priorityList.RemoveAt(0);
                }
                ViewBag.priority.Insert(0, new master_priority { id = null, priority_name = "--Select--" });
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

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads/ResolutionHUB/issues"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues/" + newFolderName + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues/" + newFolderName + ""));
                }
                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);

                string newFileName = "/uploads/ResolutionHUB/issues/" + newFolderName + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/ResolutionHUB/issues/" + newFolderName + ""), fl);
                postedFile.SaveAs(path);

                string UploadFolder = "Shared Documents/ResolutionHUB/issues";
                string Filepath = path;

                CommonUtility.SaveOnSite(Filepath, UploadFolder, newFolderName);
                lp.Add(new Attachments()
                {
                    AttachementName = newFileName
                    ,
                    AttachementType = "IssueRequest"
                });
            }

            projectRequirement.attachment = lp;
            var result = await _requirementRepository.Save<CommonDbResponse>((int)ManageRequirement_Type.insert_requirement, projectRequirement, projectRequirement.created_by);
            if (result.STATUS == true)
            {
                var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                var NewRequestSubject = "GDC Resolution Hub- New- " + projectRequirement.auto_id + " | " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];

                string to = result.to_email_user;
                string cc = TKTcc;
                if (result.AssigntoPersonEmail != null)
                {
                    cc = cc + ";" + result.AssigntoPersonEmail;
                }
                string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
                string emailBody = @"Dear User,<br/>" +
                                    "Thank you for reaching out to us. We are working on your issue  Request ID : " + projectRequirement.auto_id + " with following details:- <br/></br>" +
                                    "<b>Date of Request :</b> " + result.date_of_request + "<br/>" +
                                    "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                    "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                                    "<b>Requester Name :</b> " + result.Requester + "<br/>";
                                    // Assign to Person
                                    if (result.AssigntoPersonEmail != null)
                                    {
                                        emailBody += "<b>Request for :</b> " + result.AssigntoPerson_Name + "<br/>";
                                    }
                                    emailBody +="<b>Tool Name :</b> " + result.tool_name + "<br/>" +
                                    "<b>Use Case Name :</b> " + result.use_case_name + "<br/></br>" +
                                    //"Link: <a href=\"#\" class=\"visually-hidden focusable\">Open Details </a><br/>" +
                                    "<b>Best Regards<br/>" +
                                    "<b>GDC Resolution Hub.";
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _requirementRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
           
            ViewBag.Id = id;
            ViewBag.master_tool = await _masterRepository.GetMasterInfo<master_tool>((int)MasterType.master_tool);
            ViewBag.priority = await _masterRepository.GetMasterInfo<master_priority>((int)MasterType.master_priority);
            ViewBag.status = await _masterRepository.GetMasterInfo<master_status>((int)MasterType.master_status);
            ViewBag.BooleanDropdown = await _masterRepository.GetMasterInfo<BooleanDropdown>((int)MasterType.boolean_dropdown);
            ViewBag.function = await _masterRepository.GetMasterInfo<master_function>((int)MasterType.master_function);
            ViewBag.function_level = await _masterRepository.GetMasterInfo<master_function_level>((int)MasterType.master_function_level);
            ViewBag.region = await _masterRepository.GetMasterInfo<master_region>((int)MasterType.master_region);
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            allMaster.statuses.Insert(0, new master_status { id = Guid.Empty, status_name = "--Select--" });

            var result = await _userRepository.GetDeveloperUsers((int)usp_ManageUser_Type.DevelopersList);
            result.Insert(0, new vm_user_registration { employee_id = null, employee_name = "--Select--" });
            ViewBag.Developers = result;

            allMaster.priorities.Insert(0, new master_priority { id = Guid.Empty, priority_name = "--Select--" });

            var cat1 = await resCat1Repository.GetAll();
            cat1.Insert(0, new master_res_cat_1 { id = Guid.Empty, res_cat1_name = "--Select--" });
            ViewBag.ResCat1 = cat1;

            //var cat2 = await resCat2Repository.GetAll();
            //cat2.Insert(0, new master_res_cat_2 { id = Guid.Empty, res_cat2_name = "--Select--" });
            //ViewBag.ResCat2 = cat2;

            //var cat3 = await resCat3Repository.GetAll();
            //cat3.Insert(0, new master_res_cat_3 { id = Guid.Empty, res_cat3_name = "--Select--" });
            //ViewBag.ResCat3 = cat3;

            return View(allMaster);
        }

        public async Task<JsonResult> GetRequirementDetail(Guid id)
        {
            var result = await _requirementRepository.GetRow<project_requirement>((int)ManageRequirement_Type.get_requirement_by_id, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(vmProjectRequirement projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();

            List<Attachments> lp = new List<Attachments>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads/ResolutionHUB/issues"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues/" + projectRequirement.auto_id + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues/" + projectRequirement.auto_id + ""));
                }
                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);


                string newFileName = "/uploads/ResolutionHUB/issues/" + projectRequirement.auto_id + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/ResolutionHUB/issues/" + projectRequirement.auto_id + ""), fl);

                postedFile.SaveAs(path);

                string UploadFolder = "Shared Documents/ResolutionHUB/issues";
                string Filepath = path;
                CommonUtility.SaveOnSite(Filepath, UploadFolder, projectRequirement.auto_id);
                lp.Add(new Attachments()
                {
                    AttachementName = newFileName
                     ,
                    AttachementType = "IssueRequest"
                });
            }

            projectRequirement.attachment = lp;
            var result = await _requirementRepository.Save<CommonDbResponse>((int)ManageRequirement_Type.update_requirement, projectRequirement, projectRequirement.created_by);
            string dev_email, cc;
            if (result.STATUS == true)
            {
                string Dateofrequest = "";
                dev_email = result.developers_group_email;
                if ((result.progressResponce.ToString() != "0" && result.progressResponce.ToString() != "In-Progress") || result.AssigntoPersonEmail != null)
                {
                    var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                    var NewRequestSubject = "GDC Resolution Hub- Update- " + projectRequirement.auto_id + "| " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];
                    string to = result.to_email_user;
                    cc = TKTcc;
                    if (result.AssigntoPersonEmail != null)
                    {
                        cc = cc + ";" + result.AssigntoPersonEmail;
                    }
                    string subject = NewRequestSubject;
                    string emailBody = @"Dear User,<br/>" +
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
                                        emailBody += "<b>Tool Name :</b> " + result.tool_name + "<br/>" +
                                        "<b>Use Case Name :</b> " + result.use_case_name + "<br/></br>" +
                                        //"<b>Current Status :</b> " + result.progressResponce + "<br/></br>" +
                                        "Best Regards<br/>" +
                                        "GDC Resolution Hub.";
                    
                }
                if (result.progressResponce.ToString() == "In-Progress")
                {
                    //Dateofrequest = projectRequirement.date_of_request.Value.ToString("dd/MM/yyyy");
                    var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                    var NewRequestSubject = "GDC Resolution Hub- Update- " + projectRequirement.auto_id + "| " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];
                    string to = dev_email;
                    cc = TKTcc + "; " + result.to_email_user;
                    string subject = NewRequestSubject;
                    string emailBody = @"Dear User,<br/>" +
                                        "Your Request has been updated please find the details below:- <br/></br>" +
                                        "<b>Date of Request :</b> " + result.date_of_request + "<br/>" +
                                        "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                        "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                                        "<b>Requester Name :</b> " + result.Requester + "<br/>" +
                                        "<b>Tool Name :</b> " + result.tool_name + "<br/>" +
                                        "<b>Use Case Name :</b> " + result.use_case_name + "<br/>" +
                                        "<b>Current Status :</b> " + result.progressResponce + "<br/></br>" +
                                        "Best Regards<br/>" +
                                        "GDC Resolution Hub.";
                    
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
        public async static void sendMailIsseRequest()
        {
            var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
            var NewRequestSubject = ConfigurationManager.AppSettings["IssueRequestSubject"];
            string to = "raj.mukut@nokia.com";
            string cc = TKTcc;
            string subject = NewRequestSubject + DateTime.Now.ToString("yyyy_MM_dd-HH");
            string emailBody = @"
                                Hi,<br/><br/>
                                Thank you for reaching out to us. We are working on your issue  Request ID : REQ_202305181703001 with following details <br/><br/>
                                Best Regards<br/>
                                TKT";
            //EmailService.EmailService emailService = new EmailService.EmailService();
            //emailService.send_mail_msg_html_body("GDC ResolutionHub", to, cc, subject, emailBody);
        }
        public async Task<ActionResult> GetToolNameByProjectID(Guid project_name)
        {
            var result = await _requirementRepository.GetToolNameByProjectID<master_tool>((int)ManageRequirement_Type.getToolByProjectId, project_name);
            ViewBag.tools = result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetUseCasesByToolNameId(Guid tool_id, Guid project_id)
        {
            var result = await _requirementRepository.GetUseCasesByToolNameId<master_usecase>((int)ManageRequirement_Type.getbytoolId, tool_id, project_id);
            result.Insert(0, new master_usecase { id = Guid.Empty, use_case_name = "Others" });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
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
        public async Task<ActionResult> GetUpdateMessage(string auto_id)
        {
            string user_name = Session["user_name"].ToString();
            var result = await _requirementRepository.GetUpdateMessage((int)usp_ManageMessageUpdateStatus_Type.getbyauto_id, auto_id);
            var obj = new { messages = result };
            return Json(obj, JsonRequestBehavior.AllowGet);
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
                                  "<b>Requester Name :</b> " + result.Requester + "<br/>";
                                  if (result.AssigntoPersonEmail != null)
                                    {
                                        emailBody += "<b>Request for :</b> " + result.AssigntoPerson_Name + "<br/>";
                                    }
                                    emailBody += "<b>New State :</b> " + result.progressResponce.ToString() + "<br/>" +
                                  "<b>Comment added :</b> " + request.message.ToString() + "</br></br>" +
                                  "<b>Best Regards<br/>" +
                                  "<b>GDC Resolution Hub.";
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> OthersEdit(Guid id)
        {
            ViewBag.Id = id;
            Session["OthersId"] = id;
            ViewBag.Id = id;
            ViewBag.master_tool = await _masterRepository.GetMasterInfo<master_tool>((int)MasterType.master_tool);
            ViewBag.priority = await _masterRepository.GetMasterInfo<master_priority>((int)MasterType.master_priority);
            ViewBag.status = await _masterRepository.GetMasterInfo<master_status>((int)MasterType.master_status);
            ViewBag.BooleanDropdown = await _masterRepository.GetMasterInfo<BooleanDropdown>((int)MasterType.boolean_dropdown);
            ViewBag.function = await _masterRepository.GetMasterInfo<master_function>((int)MasterType.master_function);
            ViewBag.function_level = await _masterRepository.GetMasterInfo<master_function_level>((int)MasterType.master_function_level);
            ViewBag.region = await _masterRepository.GetMasterInfo<master_region>((int)MasterType.master_region);
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            allMaster.statuses.Insert(0, new master_status { id = Guid.Empty, status_name = "--Select--" });

            if (Session["access_role"].ToString() == "admin" || Session["access_role"].ToString() == "Developer")
            {
                allMaster.projects.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = allMaster.projects;
            }
            else
            {
                vw_user_projects_mapping request = new vw_user_projects_mapping { employee_id = Session["user_name"].ToString() };
                var result = await _UserProjectsMappingRepository.GetUserProjectMappingByEmployeeId<master_project>((int)ManageUserProjectMapping_Type.getall, request);
                //result.Insert(0, new master_project { id = null, project_name = "--Select--" });
                ViewBag.ProjectForUser = result;
            }
            return View(allMaster);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateOthers(vmProjectRequirement projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();

            List<Attachments> lp = new List<Attachments>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];

                string newFolderName = projectRequirement.auto_id;

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads/ResolutionHUB/issues"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues/" + newFolderName + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/ResolutionHUB/issues/" + newFolderName + ""));
                }
                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);

                string newFileName = "/uploads/ResolutionHUB/issues/" + newFolderName + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/ResolutionHUB/issues/" + newFolderName + ""), fl);
                postedFile.SaveAs(path);

                string UploadFolder = "Shared Documents/ResolutionHUB/issues";
                string Filepath = path;

                CommonUtility.SaveOnSite(Filepath, UploadFolder, newFolderName);
                lp.Add(new Attachments()
                {
                    AttachementName = newFileName
                    ,
                    AttachementType = "IssueRequest"
                });
            }

            projectRequirement.attachment = lp;
            var result = await _requirementRepository.UpdateOthers_IS<CommonDbResponse>((int)ManageRequirement_Type.updatebyothers_is, projectRequirement, projectRequirement.created_by);
            if (result.STATUS == true)
            {
                //string Dateofrequest = projectRequirement.date_of_request.Value.ToString("dd/MM/yyyy");
                var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                //var NewRequestSubject = ConfigurationManager.AppSettings["IssueRequestSubject"];
                var NewRequestSubject = "GDC Resolution Hub- Update- " + projectRequirement.auto_id + " | " + projectRequirement.request_title + "";// ConfigurationManager.AppSettings["ChangeRequestSubject"];

                string to = result.to_email_user;
                string cc = TKTcc;
                string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
                string emailBody = @"Dear User,<br/>" +
                                    "Thank you for reaching out to us. We are working on your issue  Request ID : " + projectRequirement.auto_id + " with following details:- <br/></br>" +
                                    "<b>Date of Request :</b> " + result.date_of_request + "<br/>" +
                                    "<b>Project Name :</b> " + result.project_name + "<br/>" +
                                    "<b>Title :</b> " + projectRequirement.request_title + "<br/>" +
                                    "<b>Requester Name :</b> " + result.Requester + "<br/>" +
                                    "<b>Tool Name :</b> " + result.tool_name + "<br/>" +
                                    "<b>Use Case Name :</b> " + result.use_case_name + "<br/></br>" +
                                    //"Link: <a href=\"#\" class=\"visually-hidden focusable\">Open Details </a><br/>" +
                                    "<b>Best Regards<br/>" +
                                    "<b>GDC Resolution Hub.";
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetRequestedStatus(Guid id)
        {
            var result = await _requirementRepository.GetRow<project_requirement>((int)ManageRequirement_Type.get_requirement_by_id, id);
            var statusresult = await _statuRepository.GetById(result.request_status);
            return Json(statusresult, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetResolutionHr(Guid request_priority)
        {
            var result = await _requirementRepository.GetResolutionHr<string>((int)ManageRequirement_Type.GetResolutionHr, request_priority);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetResCat2(Guid res_cat1)
        {
            var statusresult = await resCat2Repository.GetByCat1Id<master_res_cat_2>(res_cat1);
            return Json(statusresult, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetResCat3(Guid res_cat1, Guid res_cat2)
        {
            var statusresult = await resCat2Repository.GetByCat3Id<master_res_cat_3>(res_cat1, res_cat2);
            return Json(statusresult, JsonRequestBehavior.AllowGet);
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