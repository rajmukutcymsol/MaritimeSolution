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
    public partial class Details : System.Web.UI.Page
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
            //Main 
            //from [dbo].[Inbound_requirement] where auto_id =@auto_id and id=@id

            string strSQLMain = "SELECT * FROM [dbo].[Inbound_requirement] where id='" + id_ref + "'";
            DataTable dtReportMain = Execute_Procedures_Select_ByQuery(strSQLMain);

            string strSQL = "SELECT * FROM [dbo].[Inbound_approved_requirement] where id_ref='"+ id_ref + "'";
            DataTable dtReport = Execute_Procedures_Select_ByQuery(strSQL);

            string strSQLVessel = "select *from [dbo].[Inbound_Approved_Vessel] where id_ref='" + id_ref + "'";
            DataTable dtReportsVessel = Execute_Procedures_Select_ByQuery(strSQLVessel);

            string strSQLOwners = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='"+ dtReportMain.Rows[0]["owners_name"] + "'";
            DataTable dtReportsOwners = Execute_Procedures_Select_ByQuery(strSQLOwners);

            string strSQLRecivers = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='" + dtReportMain.Rows[0]["receiver_name"] + "'";
            DataTable dtReportsRecivers = Execute_Procedures_Select_ByQuery(strSQLRecivers);

            string strSQLChecker = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='" + dtReportMain.Rows[0]["checker_name"] + "'";
            DataTable dtReportsChecker = Execute_Procedures_Select_ByQuery(strSQLChecker);
            
            string strSQLShipper = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='" + dtReportMain.Rows[0]["shipping_name"] + "'";
            DataTable dtReportsShipper = Execute_Procedures_Select_ByQuery(strSQLShipper);
            
            string strSQLSurveyor = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='" + dtReportMain.Rows[0]["surveyor_name"] + "'";
            DataTable dtReportsSurveyor = Execute_Procedures_Select_ByQuery(strSQLSurveyor);

            string strSQLLightering = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='" + dtReportMain.Rows[0]["stevedore_name"] + "'";
            DataTable dtReportsLightering = Execute_Procedures_Select_ByQuery(strSQLLightering);
           
            string strSQLStevedore_Name_FC = "select *from [dbo].[Inbound_approved_requirement_otherinfo_otherinfo_otherinfo] where id_ref='" + id_ref + "' and DetailsType='" + dtReportMain.Rows[0]["FC_Stevedore_Name"] + "'";
            DataTable dtReportsStevedore_Name_FC = Execute_Procedures_Select_ByQuery(strSQLStevedore_Name_FC);


            rd.Load(Server.MapPath("rptDetails.rpt"));
            rd.SetDataSource(dtReport);
            CrystalReportViewer1.ReportSource = rd;

            if (dtReportsVessel.Rows.Count > 0)
            {
                rd.Subreports["rptDetailsVessel.rpt"].SetDataSource(dtReportsVessel);
            }
            if (dtReportsOwners.Rows.Count > 0)
            {
                rd.Subreports["rptOwners.rpt"].SetDataSource(dtReportsOwners);
            }
            if (dtReportsRecivers.Rows.Count > 0)
            {
                rd.Subreports["rptRecivers.rpt"].SetDataSource(dtReportsRecivers);
            }
            if (dtReportsChecker.Rows.Count > 0)
            {
                rd.Subreports["rptCheckers.rpt"].SetDataSource(dtReportsChecker);
            }
            if (dtReportsShipper.Rows.Count > 0)
            {
                rd.Subreports["rptShippers.rpt"].SetDataSource(dtReportsShipper);
            }
            if (dtReportsSurveyor.Rows.Count > 0)
            {
                rd.Subreports["rptSurveyor.rpt"].SetDataSource(dtReportsSurveyor);
            }
            if (dtReportsLightering.Rows.Count > 0)
            {
                rd.Subreports["rptLightering.rpt"].SetDataSource(dtReportsLightering);
            }
            if (dtReportsStevedore_Name_FC.Rows.Count > 0)
            {
                rd.Subreports["FCStevedore.rpt"].SetDataSource(dtReportsStevedore_Name_FC);
            }

            string strSQLLogo = "select logoupload from master_CLIUI where is_active=1 AND is_deleted=0";
            DataTable dtReportLogo = Execute_Procedures_Select_ByQuery(strSQLLogo);
            string path = ConfigurationManager.AppSettings["LogoPath"].ToString();
            string logo = path + dtReportLogo.Rows[0]["logoupload"].ToString() + "";
            rd.SetParameterValue("Logo", logo);

            //rptRecivers.rpt select *from [dbo].[Inbound_Approved_Vessel] where id_ref=
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