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
    public partial class Daily_Report : System.Web.UI.Page
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

            string strSQLMain = "SELECT *, CONCAT(RIGHT('00' + CAST(from_HH_daytime AS VARCHAR(2)), 2) + RIGHT('00' + CAST(from_MM_daytime AS VARCHAR(2)), 2),'-',RIGHT('00' + CAST(to_HH_daytime AS VARCHAR(2)), 2) + RIGHT('00' + CAST(to_MM_daytime AS VARCHAR(2)), 2)  ) AS daytime,  CONCAT(RIGHT('00' + CAST(from_HH_first AS VARCHAR(2)), 2) + RIGHT('00' + CAST(from_MM_first AS VARCHAR(2)), 2),'-',RIGHT('00' + CAST(to_HH_first AS VARCHAR(2)), 2) + RIGHT('00' + CAST(to_MM_first AS VARCHAR(2)), 2)  ) AS first,  CONCAT(RIGHT('00' + CAST(from_HH_second AS VARCHAR(2)), 2) + RIGHT('00' + CAST(from_MM_second AS VARCHAR(2)), 2),'-',RIGHT('00' + CAST(to_HH_second AS VARCHAR(2)), 2) + RIGHT('00' + CAST(to_MM_second AS VARCHAR(2)), 2)  ) AS second,loading_text_date+ '@'+RIGHT('00' + CONVERT(NVARCHAR(2), [from_HH_loading_text]), 2)+''+RIGHT('00' + CONVERT(NVARCHAR(2), [to_MM_loading_text]), 2)+' '+loading_text_until  as loading,comp_loading+ '@'+RIGHT('00' + CONVERT(NVARCHAR(2), [from_HH_comp_loading]), 2)+''+RIGHT('00' + CONVERT(NVARCHAR(2), [from_MM_comp_loading]), 2)+'     '+	to_comp_loading+ '@'+RIGHT('00' + CONVERT(NVARCHAR(2), to_HH_comp_loading), 2)+''+RIGHT('00' + CONVERT(NVARCHAR(2), to_MM_comp_loading), 2)+'     ' as comp_loading, 			comm_loading+ '@'+RIGHT('00' + CONVERT(NVARCHAR(2), [from_HH_comm_loading]), 2)+''+RIGHT('00' + CONVERT(NVARCHAR(2), [from_MM_comm_loading]), 2)+'     '+	to_comm_loading+ '@'+RIGHT('00' + CONVERT(NVARCHAR(2), to_HH_comm_loading), 2)+''+RIGHT('00' + CONVERT(NVARCHAR(2), to_MM_comm_loading), 2)+'     ' as comm_loading, 'On ' +      CONVERT(NVARCHAR(50), from_daily_discharge_cargo, 107) +      ' @ ' +      RIGHT('00' + CONVERT(NVARCHAR(50), from_HH_daily_discharge_cargo), 2) +     RIGHT('00' + CONVERT(NVARCHAR(50), from_MM_daily_discharge_cargo), 2) + ' hrs. Up To  On ' +    CONVERT(NVARCHAR(50), to_daily_discharge_cargo, 107) +     ' @ ' + RIGHT('00' + CONVERT(NVARCHAR(50), to_HH_daily_discharge_cargo), 2) +  RIGHT('00' + CONVERT(NVARCHAR(50), to_MM_daily_discharge_cargo), 2) +    ' Hrs.' as formatted_result,FORMAT(report_date, 'MMMM dd, yyyy') as  report_date FROM     [dbo].[master_daily_report] WHERE      id ='" + id_ref + "'";

            DataTable dtReportMain = Execute_Procedures_Select_ByQuery(strSQLMain);


            string query_sum_hold= "SELECT    FORMAT(report_date, 'MMMM dd, yyyy') AS report_date,    SUM(hold_1_total + hold_2_total + hold_3_total + hold_4_total + hold_5_total) AS sum_all_holds FROM    [dbo].[master_daily_report] WHERE     id = '"+ id_ref + "' GROUP BY     FORMAT(report_date, 'MMMM dd, yyyy')";
            DataTable dtsum_hold = Execute_Procedures_Select_ByQuery(query_sum_hold);

            string sum_all_holds = dtsum_hold.Rows[0]["sum_all_holds"].ToString();


            string formatted_result = dtReportMain.Rows[0]["formatted_result"].ToString();
            string on_report_date = dtReportMain.Rows[0]["report_date1"].ToString();
            string comm_loading = dtReportMain.Rows[0]["comm_loading1"].ToString();
            string comp_loading = dtReportMain.Rows[0]["comp_loading1"].ToString();
            string loading = dtReportMain.Rows[0]["loading"].ToString();

            string daytime = dtReportMain.Rows[0]["daytime"].ToString();
            string first = dtReportMain.Rows[0]["first"].ToString();
            string second = dtReportMain.Rows[0]["second"].ToString();

            string strComanyMain = "SELECT * FROM [dbo].[master_CLIUI] where is_active=1";
            DataTable dtCompanyMain = Execute_Procedures_Select_ByQuery(strComanyMain);


            rd.Load(Server.MapPath("rptDailyReport.rpt"));
            rd.SetDataSource(dtReportMain);
            CrystalReportViewer1.ReportSource = rd;

            if (dtCompanyMain.Rows.Count > 0)
            {
                rd.Subreports["rptSubCompany.rpt"].SetDataSource(dtCompanyMain);
            }

            //string remarkQuery = "SELECT id, CONVERT(VARCHAR, date_of_action, 103) as date_of_action,'@ ' + RIGHT('00' + CONVERT(VARCHAR, HH_date_of_action), 2) + RIGHT('00' + CONVERT(VARCHAR, MM_date_of_action), 2) + '-' +    RIGHT('00' + CONVERT(VARCHAR, to_HH_date_of_action), 2) +    RIGHT('00' + CONVERT(VARCHAR, to_MM_date_of_action), 2) + ' Hrs:               :' + remarks_comments AS remarks_comments FROM      [dbo].[cargo_status_of_day_remarks] WHERE id_ref = '" + dtReportMain.Rows[0]["id"].ToString() + "'  AND report_date = '" + dtReportMain.Rows[0]["report_date"].ToString() + "' ORDER BY     date_of_action ASC;";
            // Assuming dtReportMain.Rows[0]["id"] is an integer and dtReportMain.Rows[0]["report_date"] is a DateTime
           //int idRef = Convert.ToInt32(dtReportMain.Rows[0]["id"]);
            DateTime reportDate = Convert.ToDateTime(dtReportMain.Rows[0]["report_date"]);
            string formattedReportDate = reportDate.ToString("yyyy-MM-dd HH:mm:ss");

            string remarkQuery = $"SELECT id, CONVERT(VARCHAR, date_of_action, 103) as date_of_action," +
                $" '@ ' + RIGHT('00' + CONVERT(VARCHAR, HH_date_of_action), 2) + " +
                $" RIGHT('00' + CONVERT(VARCHAR, MM_date_of_action), 2) + '-' + " +
                $" RIGHT('00' + CONVERT(VARCHAR, to_HH_date_of_action), 2) + " +
                $" RIGHT('00' + CONVERT(VARCHAR, to_MM_date_of_action), 2) + ' Hrs:               :' + remarks_comments AS remarks_comments " +
                $" FROM [dbo].[cargo_status_of_day_remarks] " +
                $" WHERE id_ref = '"+ dtReportMain.Rows[0]["id"].ToString() + "' AND report_date = '"+ formattedReportDate + "' " +
                $" ORDER BY date_of_action ASC;";

            // Now you can use remarkQuery in your SQL command


            DataTable dtRemarks = Execute_Procedures_Select_ByQuery(remarkQuery);
            if (dtRemarks.Rows.Count > 0)
            {
                rd.Subreports["rptRemarks.rpt"].SetDataSource(dtRemarks);
            }

            rd.SetParameterValue("formatted_result", formatted_result);
            rd.SetParameterValue("on_report_date", on_report_date);
            rd.SetParameterValue("comm_loading", comm_loading);
            rd.SetParameterValue("comp_loading", comp_loading);
            rd.SetParameterValue("loading", loading);

            rd.SetParameterValue("daytime", daytime);
            rd.SetParameterValue("first", first);
            rd.SetParameterValue("second", second);
            rd.SetParameterValue("sum_all_holds", sum_all_holds);

            // for remarkd 


            //string strSQLLogo = "select logoupload from master_CLIUI where is_active=1 AND is_deleted=0";
            //DataTable dtReportLogo = Execute_Procedures_Select_ByQuery(strSQLLogo);
            //string path = ConfigurationManager.AppSettings["LogoPath"].ToString();
            //string logo = path + dtReportLogo.Rows[0]["logoupload"].ToString() + "";
            //rd.SetParameterValue("Logo", logo);

            //rptRecivers.rpt select *from [dbo].[Inbound_Approved_Vessel] where id_ref=
            //rd.SetDatabaseLogon(sUserName, sPassword);
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