using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace TicketingTool.Reports
{
    public partial class Manifest : System.Web.UI.Page
    {
        ReportDocument rd = new ReportDocument();
        static String ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id_ref = Request.QueryString["id_ref"].ToString();
                LoadReports(id_ref);
            }


        }

        private void LoadReports(string id_ref)
        {
            SqlConnectionStringBuilder ObjSqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string sUserName = ObjSqlConnectionStringBuilder.UserID;
            string sPassword = ObjSqlConnectionStringBuilder.Password;

            string strSQL = "SELECT * FROM inbound_approved_manifest_head where id_ref='" + id_ref + "'";
            DataTable dtReport = Execute_Procedures_Select_ByQuery(strSQL);

            string strSQLVessel = "select *from [dbo].[inbound_approved_manifest] where id_ref='" + id_ref + "'";
            DataTable dtReportsVessel = Execute_Procedures_Select_ByQuery(strSQLVessel);

            rd.Load(Server.MapPath("rptManifestHeader.rpt"));

            rd.SetDataSource(dtReport);

         
            CrystalReportViewer1.ReportSource = rd;
            if (dtReportsVessel.Rows.Count > 0)
            {
                rd.Subreports["rptManifestCargo.rpt"].SetDataSource(dtReportsVessel);
            }
            string strSQLLogo = "select logoupload from master_CLIUI where is_active=1 AND is_deleted=0";
            DataTable dtReportLogo = Execute_Procedures_Select_ByQuery(strSQLLogo);
            string path = ConfigurationManager.AppSettings["LogoPath"].ToString();
            string logo = path+dtReportLogo.Rows[0]["logoupload"].ToString() +"";
            rd.SetParameterValue("Logo", logo);

            //select *from [dbo].[Inbound_Approved_Vessel] where id_ref=
            rd.SetDatabaseLogon(sUserName, sPassword);
            rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "uSERINFO");
        }

        public static DataTable Execute_Procedures_Select_ByQuery(string Query)
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter Adp = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand(Query, myConnection);
            Adp.SelectCommand = Command;
            Adp.Fill(RetValue, "Result");
            try
            {
                return RetValue.Tables[0];
            }
            catch { return null; }
        }

    }
}