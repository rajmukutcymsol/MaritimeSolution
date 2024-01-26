using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace TicketingTool.Utilities
{
    public class CommonUtility
    {
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            try
            {
                List<T> data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetObjectByRow<T>(row);
                    data.Add(item);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
            //return data;
        }
        public static T GetObjectByRow<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            try
            {
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            if (dr[column.ColumnName] != DBNull.Value)
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            else
                                pro.SetValue(obj, null, null);
                        }

                        else
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public static DataTable CreateDataTableFromList<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table class='table table-bordered table-responsive table-striped' id='tbl_Investement_summery'>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
        public static string ConvertDataTabletoString(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }
        //public async static void sendMail(string to, string cc, string subject, string emailBody)
        //{
        //    EmailService.EmailService emailService = new EmailService.EmailService();
        //    emailService.send_mail_msg_html_body("GDC ResolutionHub", to, cc, subject, emailBody);
        //}

        public async static void SaveOnSite(string Filepath, string UploadFolder, string newFolderName)
        {
            string _SharePointSite = ConfigurationManager.AppSettings["SharePointSite"];
            string _UserName = ConfigurationManager.AppSettings["SharepointFileUpladUserName"]; ;
            string _Password = ConfigurationManager.AppSettings["SharepointFileUpladPassword"]; ;

            ScriptSharePoint ssp = new ScriptSharePoint(_SharePointSite, _UserName, _Password);
            ssp.CreateFolderandUploadFile(Filepath, UploadFolder, newFolderName);
        }
        public async Task<bool> ValidateWebContent(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        // Check if the web content is present
                        if (!string.IsNullOrEmpty(content))
                        {
                            return true; // Web content is present
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return false; // Web content is not present or an error occurred
        }
    }

}
