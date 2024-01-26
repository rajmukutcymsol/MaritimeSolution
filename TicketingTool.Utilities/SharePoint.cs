using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Security;
using System.IO;

namespace TicketingTool.Utilities
{
    public class ScriptSharePoint
    {
        private readonly string sharepointsite;
        private readonly string username;
        private readonly string password;
        private readonly SecureString securePassword = new SecureString();

        public ScriptSharePoint(string _sharepointsite, string _username, string _password)
        {
            this.sharepointsite = _sharepointsite;
            this.username = _username;
            this.password = _password;

            foreach (char c in password)
            {
                this.securePassword.AppendChar(c);
            }
        }

        public bool UploadFile(string filepath, string uploadfolder)
        {
            bool result = false;
            try
            {
                using (var clientContext = new ClientContext(sharepointsite))
                {
                    //SharePoint Authentication using username and password
                    clientContext.Credentials = new SharePointOnlineCredentials(username, securePassword);
                    Web web = clientContext.Web;
                    clientContext.Load(web, a => a.ServerRelativeUrl);
                    clientContext.ExecuteQuery();

                    // Get Sharepoint Folder relative URL
                    string uploadRootFolderRelativeUrl = uploadfolder;
                    Folder rootfolder = web.GetFolderByServerRelativeUrl(uploadRootFolderRelativeUrl);

                    //Create Upload File Information
                    var fileCreationInformation = new FileCreationInformation();
                    fileCreationInformation.Content = System.IO.File.ReadAllBytes(filepath);
                    fileCreationInformation.Url = Path.GetFileName(filepath);
                    fileCreationInformation.Overwrite = true;

                    //Add file to Upload Folder
                    rootfolder.Files.Add(fileCreationInformation);
                    rootfolder.Update();
                    clientContext.ExecuteQuery();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return result;
        }


        public bool CreateFolderandUploadFile(string filepath, string uploadfolder, string newFolderName)
        {
            bool result = false;
            try
            {
                using (var clientContext = new ClientContext(sharepointsite))
                {
                    //SharePoint Authentication using username and password
                    clientContext.Credentials = new SharePointOnlineCredentials(username, securePassword);
                    Web web = clientContext.Web;
                    clientContext.Load(web, a => a.ServerRelativeUrl);
                    clientContext.ExecuteQuery();

                    // Get Sharepoint Folder relative URL
                    string uploadRootFolderRelativeUrl = uploadfolder;
                    Folder rootfolder = web.GetFolderByServerRelativeUrl(uploadRootFolderRelativeUrl);

                    //Create Upload File Information
                    var fileCreationInformation = new FileCreationInformation();
                    fileCreationInformation.Content = System.IO.File.ReadAllBytes(filepath);
                    fileCreationInformation.Url = Path.GetFileName(filepath);
                    fileCreationInformation.Overwrite = true;

                    //Create new subFolder to load files into
                    rootfolder.Folders.Add(newFolderName);
                    rootfolder.Update();

                    //Add file to new Folder
                    Folder currentRunFolder = web.GetFolderByServerRelativeUrl(uploadRootFolderRelativeUrl + "/" + newFolderName);
                    currentRunFolder.Files.Add(fileCreationInformation);
                    currentRunFolder.Update();
                    clientContext.ExecuteQuery();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return result;
        }

        public void UploadFileToSharePoint(byte[] fileBytes, string fileName, string siteUrl, string documentLibraryName)
        {
            // Replace the placeholders with your SharePoint site URL and credentials
            //string username = "your_username";

            // Create a new SharePoint client context
            using (ClientContext context = new ClientContext(sharepointsite))
            {
                // Provide the credentials
                context.Credentials = new SharePointOnlineCredentials(username, securePassword);

                // Get the SharePoint web and document library
                Web web = context.Web;
                List documentLibrary = web.Lists.GetByTitle(documentLibraryName);

                // Prepare the file creation information
                FileCreationInformation fileCreationInfo = new FileCreationInformation
                {
                    Content = fileBytes,
                    Url = fileName,
                    Overwrite = true
                };

                // Upload the file to SharePoint
                Microsoft.SharePoint.Client.File uploadedFile = documentLibrary.RootFolder.Files.Add(fileCreationInfo);

                // Execute the upload operation
                context.ExecuteQuery();
            }
        }

    }
}
