using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class CliUiController : Controller
    {
        private readonly ICLIUIRepository _cliUiRepository;
        public CliUiController(ICLIUIRepository cliuiRepository)
        {
            this._cliUiRepository = cliuiRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _cliUiRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _cliUiRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_cli_ui request)
        {
            //var fileName = Path.GetFileName(request.upload.FileName);
            //var extension = Path.GetExtension(fileName);

            //// Generate a unique filename
            //var uniqueFileName = Guid.NewGuid().ToString() + extension;

            //// Specify the path to the upload folder
            //var uploadPath = Server.MapPath("~/Uploads/");

            //// Combine the path with the filename
            //var filePath = Path.Combine(uploadPath, uniqueFileName);

            //// Save the file to the server
            //request.upload.SaveAs(filePath);
            //request.logoupload = uniqueFileName;
           // List<Attachments> lp = new List<Attachments>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];
                string newFolderName = "uploads";

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/" + newFolderName + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/" + newFolderName + ""));
                }

                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);

                //string newFileName = "/uploads/ResolutionHUB/ChangeRequest/" + newFolderName + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/" + newFolderName + ""), fl);
                postedFile.SaveAs(path);

                //string UploadFolder = "Shared Documents/ResolutionHUB/ChangeRequest";
                //string Filepath = path;
                request.logoupload = fl;
            }
            var result = await _cliUiRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_cli_ui request)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase postedFile = Request.Files[i];
                string newFolderName = "uploads";

                bool isPathExist = Directory.Exists(Server.MapPath("~/uploads"));
                if (!isPathExist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads"));
                    Directory.CreateDirectory(Server.MapPath("~/uploads/" + newFolderName + ""));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/" + newFolderName + ""));
                }

                string extension = Path.GetExtension(postedFile.FileName);
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string fl = string.Concat(file, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);

                //string newFileName = "/uploads/ResolutionHUB/ChangeRequest/" + newFolderName + "/" + fl;
                string path = Path.Combine(Server.MapPath("~/uploads/" + newFolderName + ""), fl);
                postedFile.SaveAs(path);

                //string UploadFolder = "Shared Documents/ResolutionHUB/ChangeRequest";
                //string Filepath = path;
                request.logoupload = fl;
            }
            var result = await _cliUiRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _cliUiRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}