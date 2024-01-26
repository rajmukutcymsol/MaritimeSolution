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
    public partial class Statement_of_Fact : System.Web.UI.Page
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
            DataTable Maindt = new DataTable();
            Maindt = Execute_Procedures_Select_BySP("usp_Statementof_Fact",1,id_ref);

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("report_date", typeof(DateTime));
            resultTable.Columns.Add("formatted_date", typeof(string));
            resultTable.Columns.Add("time", typeof(string));

            resultTable.Columns.Add("Hrs:Min", typeof(int));
            resultTable.Columns.Add("Metric Tons", typeof(decimal));
            resultTable.Columns.Add("gang", typeof(int));
            resultTable.Columns.Add("HatchNo", typeof(string));
            int totalHrsMin = 0;
            decimal totalMetricTons = 0;

            for (int i=0; i<Maindt.Rows.Count;i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    DataRow originalRow = Maindt.Rows[i];

                    DateTime reportDate = (DateTime)originalRow["report_date"];
                    string formattedDate = (string)originalRow["formatted_date"];

                    string timeRangeDaytime = (string)originalRow["time_range_daytime"];
                    string time_range_first = (string)originalRow["time_range_first"];
                    string time_range_second = (string)originalRow["time_range_second"];
                    string gang_daytime = Convert.ToString(originalRow["gang_daytime"]);
                    string gang_first = Convert.ToString(originalRow["gang_first"]);
                    string gang_second = Convert.ToString(originalRow["gang_second"]);
                    
                    string from_HH_daytime = originalRow["from_HH_daytime"].ToString().PadLeft(2, '0');
                    string from_MM_daytime = originalRow["from_MM_daytime"].ToString().PadLeft(2, '0');
                    string to_HH_daytime = originalRow["to_HH_daytime"].ToString().PadLeft(2, '0');
                    string to_MM_daytime = originalRow["to_MM_daytime"].ToString().PadLeft(2, '0');

                    string from_HH_first = originalRow["from_HH_first"].ToString().PadLeft(2, '0');
                    string from_MM_first = originalRow["from_MM_first"].ToString().PadLeft(2, '0');
                    string to_HH_first = originalRow["to_HH_first"].ToString().PadLeft(2, '0');
                    string to_MM_first = originalRow["to_MM_first"].ToString().PadLeft(2, '0');

                    string from_HH_second = originalRow["from_HH_second"].ToString().PadLeft(2, '0');
                    string from_MM_second = originalRow["from_MM_second"].ToString().PadLeft(2, '0');
                    string to_HH_second = originalRow["to_HH_second"].ToString().PadLeft(2, '0');
                    string to_MM_second = originalRow["to_MM_second"].ToString().PadLeft(2, '0');

                    string daytime_total_out = Convert.ToString(originalRow["daytime_total_out"]);
                    string first_total_out = Convert.ToString(originalRow["first_total_out"]);
                    string second_total_out = Convert.ToString(originalRow["second_total_out"]);

                    string HatchNo = (string)originalRow["HatchNo"].ToString();

                    decimal amount = 1.2m; 

                    DataRow resultRow = resultTable.NewRow();
                    resultRow["report_date"] = reportDate;
                    resultRow["formatted_date"] = formattedDate;
                    if (j == 0)
                    {
                        resultRow["time"] = timeRangeDaytime;
                        resultRow["gang"] = gang_daytime;
                        TimeSpan timeDifference = CalculateTimeDifference(from_HH_daytime + from_MM_daytime, to_HH_daytime+ to_MM_daytime);
                        resultRow["Hrs:Min"] =Convert.ToString(timeDifference.Hours);
                        resultRow["Metric Tons"] = daytime_total_out;
                        resultRow["HatchNo"] = HatchNo;
                    }
                    else if (j == 1)
                    {
                        resultRow["time"] = time_range_first;
                        resultRow["gang"] = gang_first;
                        TimeSpan timeDifference = CalculateTimeDifference(from_HH_first + from_MM_first, to_HH_first + to_MM_first);
                        resultRow["Hrs:Min"] = Convert.ToString(timeDifference.Hours);
                        resultRow["Metric Tons"] = second_total_out;
                        resultRow["HatchNo"] = HatchNo;

                    }
                    else
                    {
                        resultRow["time"] = time_range_second;
                        resultRow["gang"] = gang_second;

                        TimeSpan timeDifference = CalculateTimeDifference(from_HH_second + from_MM_second, to_HH_second + to_MM_second);
                        resultRow["Hrs:Min"] = Convert.ToString(timeDifference.Hours);
                        resultRow["Metric Tons"] = first_total_out;
                        resultRow["HatchNo"] = HatchNo;


                    }
                    //resultRow["amount"] = amount;
                    totalHrsMin += Convert.ToInt32(resultRow["Hrs:Min"]);
                    totalMetricTons += Convert.ToDecimal(resultRow["Metric Tons"]);

                    resultTable.Rows.Add(resultRow);
                    
                }
            }
           

            //string strSQLMain = "select vd.vesselName as vessel_name,  ir.mastername as  master_name,  ia.flags, ia.CompanyName as agent_name, ia.LoadPort as load_port,  ia.DischargePort discharge_port ,cmd.CompanyName as surveyor_name,cmda.CompanyName as stevedore_name from  [dbo].[Inbound_requirement] ir inner join VesselData vd on ir.vessel_name=vd.id inner join [dbo].[inbound_approved_manifest_head] ia on ia.id_ref=ir.id  inner join [dbo].[CommonMastersData] cmd on cmd.id=ir.surveyor_name  inner join [dbo].[CommonMastersData] cmda on cmda.id=ir.stevedore_name where ir.id= '" + id_ref + "'";
            string strSQLMain = @"SELECT
                        CONVERT(NVARCHAR(50), CONCAT(FORMAT(ar.pilot_on_board_arrival, 'MMMM dd, yyyy'), ' @ ', 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_HH_pilot_on_board_arrival, 0) AS NVARCHAR(2)), 2), 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_MM_pilot_on_board_arrival, 0) AS NVARCHAR(2)), 2),
                        ' HRS')
                        ) AS pilot_on_board_arrival,
                        CONVERT(NVARCHAR(50), CONCAT(FORMAT(ar.dropped_anchor, 'MMMM dd, yyyy'), ' @ ', 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_HH_dropped_anchor, 0) AS NVARCHAR(2)), 2), 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_MM_dropped_anchor, 0) AS NVARCHAR(2)), 2),
                        ' HRS')
                        ) AS dropped_anchor,
                        CONVERT(NVARCHAR(50), CONCAT(FORMAT(ar.commenced_discharge_cargo, 'MMMM dd, yyyy'), ' @ ', 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_HH_commenced_discharge_cargo, 0) AS NVARCHAR(2)), 2), 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_MM_commenced_discharge_cargo, 0) AS NVARCHAR(2)), 2),
                        ' HRS')
                        ) AS commenced_discharge_cargo,
                        CONVERT(NVARCHAR(50), CONCAT(FORMAT(ar.completed_discharge_cargo, 'MMMM dd, yyyy'), ' @ ', 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_HH_completed_discharge_cargo, 0) AS NVARCHAR(2)), 2), 
                        RIGHT('0' + CAST(ISNULL(ar.EAT_MM_completed_discharge_cargo, 0) AS NVARCHAR(2)), 2),
                        ' HRS')
                        ) AS completed_discharge_cargo,
                        ar.readiness_tendered,
                        ar.readiness_accepted,
                        ar.arrival_at,
                        vd.vesselName AS vessel_name,
                        ir.mastername AS master_name,
                        ia.flags,
                        ia.CompanyName AS agent_name,
                        ia.LoadPort AS load_port,
                        ia.DischargePort AS discharge_port,
                        cmd.CompanyName AS surveyor_name,
                        cmda.CompanyName AS stevedore_name,
                        ar.fwd as drafton_arrived_fwd,
						ar.aft as drafton_arrived_aft,
						ar.fo as bunkeron_arrived_fo,
						ar.doo bunkeron_arrived_do,
						ar.fw bunkeron_arrived_fw,
                        ds.fwd as drafton_departure_fwd,
						ds.aft as drafton_departure_aft,
						ds.fo as bunkeron_departure_fo,
						ds.doo as bunkeron_departure_do,
						ds.fw as bunkeron_departure_fw,
						ios.departure_from_port,
						ios.ETA_Next_Port_Name,
						CONVERT(NVARCHAR(50), CONCAT(FORMAT(ios.departure_date, 'MMMM dd, yyyy'), ' @ ', 
                        RIGHT('0' + CAST(ISNULL(IOS.departure_date_HH, 0) AS NVARCHAR(2)), 2), 
                        RIGHT('0' + CAST(ISNULL(ios.departure_date_MM, 0) AS NVARCHAR(2)), 2),
                        ' HRS')
                        ) AS departure_date_from_date,
						 CONVERT(NVARCHAR(50), CONCAT(FORMAT(ios.ETA_Next_Port_Date, 'MMMM dd, yyyy'), ' @ ', 
        RIGHT('0' + CAST(ISNULL(IOS.ETA_Next_Port_HH, 0) AS NVARCHAR(2)), 2), 
        RIGHT('0' + CAST(ISNULL(ios.ETA_Next_Port_MM, 0) AS NVARCHAR(2)), 2),
        ' HRS',
        CASE WHEN ios.ETA_Next_Port_AMPM = 1 THEN ' AM' ELSE ' PM' END
    )) AS ETA_Next_Port_Date_time

                    FROM
                        [dbo].[Inbound_requirement] ir
                        INNER JOIN VesselData vd ON ir.vessel_name = vd.id
                        INNER JOIN [dbo].[inbound_approved_manifest_head] ia ON ia.id_ref = ir.id
                        INNER JOIN [dbo].[CommonMastersData] cmd ON cmd.id = ir.surveyor_name
                        INNER JOIN [dbo].[CommonMastersData] cmda ON cmda.id = ir.stevedore_name
                        INNER JOIN [dbo].[Arrival_Sailing_Condition] ar ON ir.id = ar.id_ref
                        INNER JOIN Departure_Sailing_Condition ds on ir.id=ds.id_ref
						inner join [dbo].[Inbound_SOF] ios on ir.id=ios.id_ref 
                    WHERE
                        ir.id = '" + id_ref + "'";
            DataTable dtReportMain = Execute_Procedures_Select_ByQuery(strSQLMain);

            string cargodes = "select distinct iam.auto_id,'B/L no '+iam.blno as blno, iam.cargotype_desc+iam.marks_and_nos_name as cargotype_desc,iam.gross_weight_of_cargo, mq.qtt_name from [dbo].[inbound_approved_manifest] iam  inner join inbound_manifest im on iam.auto_id=im.auto_id inner join master_qtt mq on mq.id=im.qtt_name where iam.id_ref='" + id_ref+"'";
            DataTable dtcargodes = Execute_Procedures_Select_ByQuery(cargodes);

            string strComanyMain = "SELECT * FROM [dbo].[master_CLIUI] where is_active=1";
            DataTable dtCompanyMain = Execute_Procedures_Select_ByQuery(strComanyMain);
            DataTable dtGrossTotal = Execute_Procedures_Select_ByQuery("select sum(quantity) as gross_total from inbound_manifest where auto_id='"+ dtcargodes.Rows[0]["auto_id"].ToString() + "'");
            DataTable dtQuantity_and_Kind_of_cargo_name = Execute_Procedures_Select_ByQuery("SELECT    STRING_AGG(mq.Quantity_and_Kind_of_cargo_name, ', ') AS ConcatenatedNames FROM    inbound_manifest im    INNER JOIN master_Quantity_and_Kind_of_cargo mq ON im.Quantity_and_Kind_of_cargo_name = mq.id WHERE    im.auto_id ='" + dtcargodes.Rows[0]["auto_id"].ToString() + "'");

            rd.Load(Server.MapPath("rptSOF.rpt"));
            rd.SetDataSource(dtReportMain);
            CrystalReportViewer1.ReportSource = rd;

            if (dtCompanyMain.Rows.Count > 0)
            {
                rd.Subreports["rptSubCompany.rpt"].SetDataSource(dtCompanyMain);
            }

            if (dtcargodes.Rows.Count > 0)
            {
                rd.Subreports["rptSubCargoDesc.rpt"].SetDataSource(dtcargodes);
            }
            if (resultTable.Rows.Count > 0)
            {
                rd.Subreports["rpt_SOF_Daily_Work_Time.rpt"].SetDataSource(resultTable);
            }

            rd.SetParameterValue("gross_total", dtGrossTotal.Rows[0][0].ToString());
            rd.SetParameterValue("Quantity_and_Kind_of_cargo_name", dtQuantity_and_Kind_of_cargo_name.Rows[0][0].ToString());
            rd.SetParameterValue("totalHrsMin", totalHrsMin);
            rd.SetParameterValue("totalMetricTons", totalMetricTons);
            rd.SetParameterValue("CompanyName",dtCompanyMain.Rows[0]["cli_ui_name"].ToString());
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
        public static DataTable Execute_Procedures_Select_BySP(string sp, int spType, string id_ref)
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter Adp = new SqlDataAdapter();

            SqlCommand Command = new SqlCommand(sp, myConnection);
            Command.CommandType = CommandType.StoredProcedure;

            // Add parameters to the SqlCommand
            Command.Parameters.AddWithValue("@spType", spType);
            Command.Parameters.AddWithValue("@id_ref", (object)id_ref ?? DBNull.Value);

            Adp.SelectCommand = Command;
            Adp.Fill(RetValue, "Result");

            try
            {
                return RetValue.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        static TimeSpan CalculateTimeDifference(string startTime, string endTime)
        {
            int startHours = int.Parse(startTime.Substring(0, 2));
            int startMinutes = int.Parse(startTime.Substring(2, 2));

            int endHours = int.Parse(endTime.Substring(0, 2));
            int endMinutes = int.Parse(endTime.Substring(2, 2));

            DateTime startDateTime = DateTime.Today.AddHours(startHours).AddMinutes(startMinutes);
            DateTime endDateTime = DateTime.Today.AddHours(endHours).AddMinutes(endMinutes);

            return endDateTime - startDateTime;
        }
    }
}