using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;
namespace TicketingTool.Services.Abstract.Master
{
    public interface ICommonMasterRepository
    {
        Task<List<master_pic_information>> GetPicData(master_pic_information request);
        Task<List<master_common_mstr>> GetAll();
        Task<List<master_common_mstr>> GetById(Guid? id);
        Task<CommonDbResponse> Save(int spType,master_common_mstr request);
        Task<CommonDbResponse> SavePicInformation(int spType, master_pic_information request);
        Task<CommonDbResponse> Update(int spType, master_common_mstr request);
        Task<CommonDbResponse> Delete(Guid? id);
        Task<CommonDbResponse> DeletePic(Guid? id);

        Task<List<master_State>> GetStateById<T>(int spType, master_common_mstr request);
        Task<List<master_City>> GetCityByID<T>(int spType, master_common_mstr request);
        Task<T> GetRow<T>(int spType, Guid id);
    }
}
