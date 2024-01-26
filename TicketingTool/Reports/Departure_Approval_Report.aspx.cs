using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TicketingTool.Reports
{
    public partial class Departure_Approval_Report : System.Web.UI.Page
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

            string strSQLMain = @"
            SELECT
                [auto_id],
                [id],
                [id_ref],
                [vesselname],
                [flags],
                [type_of_cargo],
                [gross_weight_of_cargo],
                [qtt_name],
                [readiness_tendered],
                [readiness_accepted],
                [arrival_at],
                'FORE= '+fwd+' /'+' AFTER= '+ aft as draft_on_departure,
                'FO= '+fo+' DO= '+doo+' FW=' +fw as bunker_rob_on_departure, 
                [lat],
                [longi],
                [draft_on_departure],
                [bunker_rob_on_departure],
                [bunker_fuel_oil],
                [bunker_diesel_oil],
                [bunker_fresh_water],
                [bunker_eta_next_port],
                [other_watch_man],
                [other_police_man],
                [other_cash_advance],
                [EAT_HH_pilot_on_board_arrival],
                [EAT_MM_pilot_on_board_arrival],
                [EAT_HH_dropped_anchor],
                [EAT_MM_dropped_anchor],
                [EAT_HH_commenced_discharge_cargo],
                [EAT_MM_commenced_discharge_cargo],
                [EAT_HH_completed_discharge_cargo],
                [EAT_MM_completed_discharge_cargo],
                [EAT_HH_pilot_on_board_departure],
                [EAT_MM_pilot_on_board_departure],
                [EAT_HH_departure_from],
                [EAT_MM_departure_from],
                [created_by],
                [updated_by],
                [updated_date],
                [created_date],
                [approved],
                [is_active],
                [is_deleted],
                CONVERT(NVARCHAR(50),  CONCAT(
                    FORMAT(ir.pilot_on_board_arrival, 'MMMM dd, yyyy'),
                    ' @ ',
                    RIGHT('0' + CAST(ISNULL(ir.EAT_HH_pilot_on_board_arrival, 0) AS NVARCHAR(2)), 2),
                    RIGHT('0' + CAST(ISNULL(ir.EAT_MM_pilot_on_board_arrival, 0) AS NVARCHAR(2)), 2),
                    ' HRS'
                )) AS pilot_on_board_arrival,
                CONVERT(NVARCHAR(50),  CONCAT(
                    FORMAT(ir.dropped_anchor, 'MMMM dd, yyyy'),
                    ' @ ',
                    RIGHT('0' + CAST(ISNULL(ir.EAT_HH_dropped_anchor, 0) AS NVARCHAR(2)), 2),
                    RIGHT('0' + CAST(ISNULL(ir.EAT_MM_dropped_anchor, 0) AS NVARCHAR(2)), 2),
                    ' HRS'
                )) AS dropped_anchor,
                CONVERT(NVARCHAR(50),  CONCAT(
                    FORMAT(ir.commenced_discharge_cargo, 'MMMM dd, yyyy'),
                    ' @ ',
                    RIGHT('0' + CAST(ISNULL(ir.EAT_HH_commenced_discharge_cargo, 0) AS NVARCHAR(2)), 2),
                    RIGHT('0' + CAST(ISNULL(ir.EAT_MM_commenced_discharge_cargo, 0) AS NVARCHAR(2)), 2),
                    ' HRS'
                )) AS commenced_discharge_cargo,
                CONVERT(NVARCHAR(50),  CONCAT(
                    FORMAT(ir.completed_discharge_cargo, 'MMMM dd, yyyy'),
                    ' @ ',
                    RIGHT('0' + CAST(ISNULL(ir.EAT_HH_completed_discharge_cargo, 0) AS NVARCHAR(2)), 2),
                    RIGHT('0' + CAST(ISNULL(ir.EAT_MM_completed_discharge_cargo, 0) AS NVARCHAR(2)), 2),
                    ' HRS'
                )) AS completed_discharge_cargo,
                CONVERT(NVARCHAR(50),  CONCAT(
                    FORMAT(ir.pilot_on_board_departure, 'MMMM dd, yyyy'),
                    ' @ ',
                    RIGHT('0' + CAST(ISNULL(ir.EAT_HH_pilot_on_board_departure, 0) AS NVARCHAR(2)), 2),
                    RIGHT('0' + CAST(ISNULL(ir.EAT_MM_pilot_on_board_departure, 0) AS NVARCHAR(2)), 2),
                    ' HRS'
                )) AS pilot_on_board_departure,
                CONVERT(NVARCHAR(50),  CONCAT(
                    FORMAT(ir.departue_from, 'MMMM dd, yyyy'),
                    ' @ ',
                    RIGHT('0' + CAST(ISNULL(ir.EAT_HH_departure_from, 0) AS NVARCHAR(2)), 2),
                    RIGHT('0' + CAST(ISNULL(ir.EAT_MM_departure_from, 0) AS NVARCHAR(2)), 2),
                    ' HRS'
                )) AS departue_from
            FROM [dbo].[Departure_Sailing_Condition] ir
            WHERE ir.id_ref = '" + id_ref + "'";

            DataTable dtReportMain = Execute_Procedures_Select_ByQuery(strSQLMain);

            string strComanyMain = "SELECT * FROM [dbo].[master_CLIUI] where is_active=1";
            DataTable dtCompanyMain = Execute_Procedures_Select_ByQuery(strComanyMain);


            rd.Load(Server.MapPath("rptDepartureReport.rpt"));
            rd.SetDataSource(dtReportMain);
            CrystalReportViewer1.ReportSource = rd;

            if (dtCompanyMain.Rows.Count > 0)
            {
                rd.Subreports["rptSubCompany.rpt"].SetDataSource(dtCompanyMain);
            }

            //string strSQLLogo = "select logoupload from master_CLIUI where is_active=1 AND is_deleted=0";
            //DataTable dtReportLogo = Execute_Procedures_Select_ByQuery(strSQLLogo);
            //string path = ConfigurationManager.AppSettings["LogoPath"].ToString();
            //string logo = path + dtReportLogo.Rows[0]["logoupload"].ToString() + "";
            //rd.SetParameterValue("Logo", logo);

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