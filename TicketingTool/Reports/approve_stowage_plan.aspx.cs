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
    public partial class approve_stowage_plan : System.Web.UI.Page
    {
        ReportDocument rd = new ReportDocument();
        static String ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id_ref = Request.QueryString["id_ref"].ToString();
                string strSQL = "select vessel_name from [dbo].[Inbound_requirement] where id='"+ id_ref + "'";
                DataTable dtvessel_name = Execute_Procedures_Select_ByQuery(strSQL);
                if (dtvessel_name != null)
                {
                    string strSQLs = "select hatchHolds from VesselData where id='"+ dtvessel_name.Rows[0][0].ToString() + "'";
                    DataTable hold = Execute_Procedures_Select_ByQuery(strSQLs);
                    if (hold.Rows[0][0].ToString() == "2")
                    {
                      LoadReports(id_ref);
                    }
                    else if (hold.Rows[0][0].ToString() == "4")
                    {
                        LoadReports_4Hold(id_ref);
                    }
                    //5 Holds
                    else if (hold.Rows[0][0].ToString() == "5")
                    {
                        LoadReports_5Hold(id_ref);
                    }

                }
            }
        }
        private void LoadReports(string id_ref)
        {
            SqlConnectionStringBuilder ObjSqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string sUserName = ObjSqlConnectionStringBuilder.UserID;
            string sPassword = ObjSqlConnectionStringBuilder.Password;

            string strSQL = "SELECT [vesselname] ,auto_id     ,[loabeamdeapth]      ,[capacities]      ,[deadweight]      ,FORMAT(arrival_date, 'd MMM yyyy') as arrival_date    ,FORMAT(sailedon_date, 'd MMM yyyy') as sailedon_date      ,[created_date]      ,[updated_by]      ,[updated_date]      ,[approved]      ,[approveddate]  FROM [dbo].[Stowage_Main_Info] where id_ref='" + id_ref + "' and approved=1";
            DataTable dtReport = Execute_Procedures_Select_ByQuery(strSQL);

            rd.Load(Server.MapPath("rptStowagePlanMainInfo.rpt"));
            rd.SetDataSource(dtReport);
            CrystalReportViewer1.ReportSource = rd;
            //Hold 1
            SqlParameter[] parameter_s = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 1"),
            };

            DataSet resultHoldInfo = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s);
            rd.Subreports["Hold1Info.rpt"].SetDataSource(resultHoldInfo.Tables[0]);

            if (resultHoldInfo.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold1Info.rpt"].SetDataSource(resultHoldInfo);
            }
            // hold 2
            SqlParameter[] parameter_s2 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 2"),
            };

            DataSet resultHoldInfo2 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s2);
            rd.Subreports["Hold2Info.rpt"].SetDataSource(resultHoldInfo2.Tables[0]);

            if (resultHoldInfo2.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold2Info.rpt"].SetDataSource(resultHoldInfo);
            }
            //Main
            string procedureName = "[dbo].[getTableData]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                new SqlParameter("@id_ref", id_ref),
            };
            DataSet resultTable = ds_ExecuteStoredProcedure(procedureName, parameters);
            rd.Subreports["rptSubStowagePlan_Table.rpt"].SetDataSource(resultTable.Tables[0]);

            if (resultTable.Tables[0].Rows.Count > 0)
            {
                rd.SetParameterValue("TotalHold 1", resultTable.Tables[1].Rows[0][0].ToString());
                rd.SetParameterValue("TotalHold 2", resultTable.Tables[1].Rows[1][0].ToString());
                rd.SetParameterValue("FinalTotal", resultTable.Tables[2].Rows[0][0].ToString());
            }

            rd.SetDatabaseLogon(sUserName, sPassword);
            rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "uSERINFO");
        }

        private void LoadReports_4Hold(string id_ref)
        {
            SqlConnectionStringBuilder ObjSqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string sUserName = ObjSqlConnectionStringBuilder.UserID;
            string sPassword = ObjSqlConnectionStringBuilder.Password;

            string strSQL = "SELECT [vesselname] ,auto_id     ,[loabeamdeapth]      ,[capacities]      ,[deadweight]      ,FORMAT(arrival_date, 'd MMM yyyy') as arrival_date    ,FORMAT(sailedon_date, 'd MMM yyyy') as sailedon_date      ,[created_date]      ,[updated_by]      ,[updated_date]      ,[approved]      ,[approveddate]  FROM [dbo].[Stowage_Main_Info] where id_ref='" + id_ref + "' and approved=1";
            DataTable dtReport = Execute_Procedures_Select_ByQuery(strSQL);

            rd.Load(Server.MapPath("rptStowagePlanMainInfo_4Hold.rpt"));
            rd.SetDataSource(dtReport);
            CrystalReportViewer1.ReportSource = rd;
            //**************Hold 1
            SqlParameter[] parameter_s = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 1"),
            };

            DataSet resultHoldInfo = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s);
            rd.Subreports["Hold1Info_4.rpt"].SetDataSource(resultHoldInfo.Tables[0]);

            if (resultHoldInfo.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold1Info_4.rpt"].SetDataSource(resultHoldInfo);
            }
            // *************hold 2
            SqlParameter[] parameter_s2 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 2"),
            };

            DataSet resultHoldInfo2 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s2);
            rd.Subreports["Hold2Info_4.rpt"].SetDataSource(resultHoldInfo2.Tables[0]);

            if (resultHoldInfo2.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold2Info_4.rpt"].SetDataSource(resultHoldInfo2);
            }

            // *************hold 2
            SqlParameter[] parameter_s3 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 3"),
            };

            DataSet resultHoldInfo3 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s3);
            rd.Subreports["Hold3Info_4.rpt"].SetDataSource(resultHoldInfo3.Tables[0]);

            if (resultHoldInfo3.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold3Info_4.rpt"].SetDataSource(resultHoldInfo3);
            }

            // *************hold 4
            SqlParameter[] parameter_s4 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 4"),
            };

            DataSet resultHoldInfo4 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s4);
            rd.Subreports["Hold4Info_4.rpt"].SetDataSource(resultHoldInfo4.Tables[0]);

            if (resultHoldInfo4.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold4Info_4.rpt"].SetDataSource(resultHoldInfo4);
            }

            //****************Main*********
            string procedureName = "[dbo].[getTableData]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                new SqlParameter("@id_ref", id_ref),
            };
            DataSet resultTable = ds_ExecuteStoredProcedure(procedureName, parameters);
            rd.Subreports["rptSubStowagePlan_Table_4.rpt"].SetDataSource(resultTable.Tables[0]);

            if (resultTable.Tables[0].Rows.Count > 0)
            {
                rd.SetParameterValue("TotalHold 1", resultTable.Tables[1].Rows[0][0].ToString());
                rd.SetParameterValue("TotalHold 2", resultTable.Tables[1].Rows[1][0].ToString());
                rd.SetParameterValue("TotalHold 3", resultTable.Tables[1].Rows[2][0].ToString());
                rd.SetParameterValue("TotalHold 4", resultTable.Tables[1].Rows[3][0].ToString());
                rd.SetParameterValue("FinalTotal", resultTable.Tables[2].Rows[0][0].ToString());
            }

            rd.SetDatabaseLogon(sUserName, sPassword);
            rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "uSERINFO");
        }

        private void LoadReports_5Hold(string id_ref)
        {
            SqlConnectionStringBuilder ObjSqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string sUserName = ObjSqlConnectionStringBuilder.UserID;
            string sPassword = ObjSqlConnectionStringBuilder.Password;

            string strSQL = "SELECT [vesselname] ,auto_id     ,[loabeamdeapth]      ,[capacities]      ,[deadweight]      ,FORMAT(arrival_date, 'd MMM yyyy') as arrival_date    ,FORMAT(sailedon_date, 'd MMM yyyy') as sailedon_date      ,[created_date]      ,[updated_by]      ,[updated_date]      ,[approved]      ,[approveddate]  FROM [dbo].[Stowage_Main_Info] where id_ref='" + id_ref + "' and approved=1";
            DataTable dtReport = Execute_Procedures_Select_ByQuery(strSQL);

            rd.Load(Server.MapPath("rptStowagePlanMainInfo_5Hold.rpt"));
            rd.SetDataSource(dtReport);
            CrystalReportViewer1.ReportSource = rd;
            //**************Hold 1
            SqlParameter[] parameter_s = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 1"),
            };

            DataSet resultHoldInfo = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s);
            rd.Subreports["Hold1Info_5.rpt"].SetDataSource(resultHoldInfo.Tables[0]);

            if (resultHoldInfo.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold1Info_5.rpt"].SetDataSource(resultHoldInfo);
            }
            // *************hold 2
            SqlParameter[] parameter_s2 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 2"),
            };

            DataSet resultHoldInfo2 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s2);
            rd.Subreports["Hold2Info_5.rpt"].SetDataSource(resultHoldInfo2.Tables[0]);

            if (resultHoldInfo2.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold2Info_5.rpt"].SetDataSource(resultHoldInfo2);
            }

            // *************hold 2
            SqlParameter[] parameter_s3 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 3"),
            };

            DataSet resultHoldInfo3 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s3);
            rd.Subreports["Hold3Info_5.rpt"].SetDataSource(resultHoldInfo3.Tables[0]);

            if (resultHoldInfo3.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold3Info_5.rpt"].SetDataSource(resultHoldInfo3);
            }

            // *************hold 4
            SqlParameter[] parameter_s4 = new SqlParameter[]
             {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 4"),
            };

            DataSet resultHoldInfo4 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s4);
            rd.Subreports["Hold4Info_5.rpt"].SetDataSource(resultHoldInfo4.Tables[0]);

            if (resultHoldInfo4.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold4Info_5.rpt"].SetDataSource(resultHoldInfo4);
            }
            // Hold 5            
            SqlParameter[] parameter_s5 = new SqlParameter[]
            {
                    new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                    new SqlParameter("@id_ref", id_ref),
                    new SqlParameter("@hold", "Hold 5"),
           };

            DataSet resultHoldInfo5 = ds_ExecuteStoredProcedure("getTableData_Hold_info", parameter_s5);
            rd.Subreports["Hold5Info_5.rpt"].SetDataSource(resultHoldInfo5.Tables[0]);

            if (resultHoldInfo5.Tables[0].Rows.Count > 0)
            {
                rd.Subreports["Hold5Info_5.rpt"].SetDataSource(resultHoldInfo5);
            }
            //****************Main*********
            string procedureName = "[dbo].[getTableData]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@auto_id", dtReport.Rows[0]["auto_id"].ToString()),
                new SqlParameter("@id_ref", id_ref),
            };
            DataSet resultTable = ds_ExecuteStoredProcedure(procedureName, parameters);
            rd.Subreports["rptSubStowagePlan_Table_5.rpt"].SetDataSource(resultTable.Tables[0]);

            if (resultTable.Tables[0].Rows.Count > 0)
            {
                rd.SetParameterValue("TotalHold 1", resultTable.Tables[1].Rows[0][0].ToString());
                rd.SetParameterValue("TotalHold 2", resultTable.Tables[1].Rows[1][0].ToString());
                rd.SetParameterValue("TotalHold 3", resultTable.Tables[1].Rows[2][0].ToString());
                rd.SetParameterValue("TotalHold 4", resultTable.Tables[1].Rows[3][0].ToString());
                rd.SetParameterValue("TotalHold 5", resultTable.Tables[1].Rows[4][0].ToString());

                rd.SetParameterValue("FinalTotal", resultTable.Tables[2].Rows[0][0].ToString());
            }

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
        public static DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            DataSet RetValue = new DataSet();
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                myConnection.Open();
                using (SqlCommand Command = new SqlCommand(procedureName, myConnection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if any
                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter Adp = new SqlDataAdapter(Command))
                    {
                        Adp.Fill(RetValue, "Result");
                        return RetValue.Tables[0];
                    }
                }
            }
        }
        public static DataSet ds_ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            DataSet RetValue = new DataSet();
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                myConnection.Open();
                using (SqlCommand Command = new SqlCommand(procedureName, myConnection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if any
                    if (parameters != null)
                    {
                        Command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter Adp = new SqlDataAdapter(Command))
                    {
                        Adp.Fill(RetValue);
                        return RetValue; // Return the entire DataSet
                    }
                }
            }
        }

    }
}