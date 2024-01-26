using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Utilities;


namespace TicketingTool.Services.Concrete.Master
{
    public class CommonMasterRepository: ICommonMasterRepository
    {
        public async Task<List<master_common_mstr>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCommonMaster.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameters);
            return CommonUtility.ConvertDataTableToList<master_common_mstr>(db_result.Tables[0]);
        }

        public async Task<master_common_mstr> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCommonMaster.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameters);
            return CommonUtility.GetObjectByRow<master_common_mstr>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(int spType, master_common_mstr dataObject)
        {
            try
            {
                SqlParameter[] parameter =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@CompanyName", dataObject.CompanyName),
                    new SqlParameter("@Address", dataObject.Address),
                    new SqlParameter("@CountryID", dataObject.CountryID),
                    new SqlParameter("@stateid", dataObject.stateid),
                    new SqlParameter("@cityid", dataObject.cityid),
                    new SqlParameter("@MobileNumber", dataObject.MobileNumber),
                    new SqlParameter("@Email", dataObject.Email),
                    new SqlParameter("@Fax", dataObject.Fax),
                    new SqlParameter("@personincharge", dataObject.personincharge),
                    new SqlParameter("@MastersCategoryID", dataObject.MastersCategoryID),
                    new SqlParameter("@is_active", dataObject.is_active),
                    new SqlParameter("@created_by", dataObject.created_by),
                    new SqlParameter("@updated_by", dataObject.created_by),
                    new SqlParameter("@id", dataObject.id),
                };

                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameter);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> Update(int spType, master_common_mstr dataObject)
        {
            SqlParameter[] parameter =
             {
               new SqlParameter("@spType",spType),
                    new SqlParameter("@CompanyName", dataObject.CompanyName),
                    new SqlParameter("@Address", dataObject.Address),
                    new SqlParameter("@CountryID", dataObject.CountryID),
                    new SqlParameter("@stateid", dataObject.stateid),
                    new SqlParameter("@cityid", dataObject.cityid),
                    new SqlParameter("@MobileNumber", dataObject.MobileNumber),
                    new SqlParameter("@Email", dataObject.Email),
                    new SqlParameter("@Fax", dataObject.Fax),
                    new SqlParameter("@CategoryName", dataObject.CategoryName),
                    new SqlParameter("@MastersCategoryID", dataObject.MastersCategoryID),
                    new SqlParameter("@is_active", dataObject.is_active),
                    new SqlParameter("@is_deleted", dataObject.is_deleted),
                    new SqlParameter("@created_by", dataObject.created_by),
                    new SqlParameter("@created_date", dataObject.created_date),
                    new SqlParameter("@updated_by", dataObject.updated_by),
                    new SqlParameter("@updated_date", dataObject.updated_date),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCommonMaster.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<List<master_State>> GetStateById<T>(int spType, master_common_mstr request)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCommonMaster.getstatebyid), new SqlParameter("@CountryID" , request.CountryID) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameters);
            return CommonUtility.ConvertDataTableToList<master_State>(db_result.Tables[0]);

        }

        public async Task<List<master_City>> GetCityByID<T>(int spType, master_common_mstr request)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCommonMaster.getcitybyid), new SqlParameter("@CountryID", request.CountryID), new SqlParameter("@StateId", request.stateid) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameters);
            return CommonUtility.ConvertDataTableToList<master_City>(db_result.Tables[0]);

        }

        Task<List<master_common_mstr>> ICommonMasterRepository.GetById(Guid? id)
        {
            throw new NotImplementedException();
        }
        public async Task<CommonDbResponse> SavePicInformation(int spType, master_pic_information dataObject)
        {
            try
            {
                SqlParameter[] parameter =
                {
                new SqlParameter("@spType", spType),
                new SqlParameter("@id", dataObject.id),
                new SqlParameter("@PicName", dataObject.PicName),
                new SqlParameter("@PicDesignation", dataObject.PicDesignation),
                new SqlParameter("@PicPhone", dataObject.PicPhone),
                new SqlParameter("@PicEmail", dataObject.PicEmail),
                new SqlParameter("@MastersCategoryID", dataObject.MastersCategoryID),
                new SqlParameter("@auto_id", dataObject.auto_id),
                new SqlParameter("@created_by", dataObject.Created_By),
            };

                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePicInformation, parameter);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately or log it.
                throw;
            }
        }

        
        public async Task<List<master_pic_information>> GetPicData(master_pic_information request )
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCommonMaster.getById), new SqlParameter("@auto_id", request.auto_id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePicInformation, parameters);
            return CommonUtility.ConvertDataTableToList<master_pic_information>(db_result.Tables[0]);
        }
        public async Task<T> GetRow<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCommonMaster, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<CommonDbResponse> DeletePic(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePicInformation.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePicInformation, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
