using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Requirement
{
    public interface IRequirementRepository
    {
        Task<T> Save<T>(int spType, project_requirement project_Requirement, string createdBy);

        Task<string> GenerateAutoId(int spType);
        Task<List<T>> GetAll<T>(int spType, Guid requirementType, vm_search search);
        Task<T> GetRow<T>(int spType, Guid id);
        Task<CommonDbResponse> Delete(Guid? id);
        Task<List<T>> GetAllAttachements<T>(int getAll, Attachments request);
        Task<CommonDbResponse> DeleteAttachement(Guid id);
        Task<List<T>> GetProjectCustomers<T>(int getAll, Guid project_name, string user_name);
        Task<List<T>> GetUseCasesByToolNameId<T>(int spType, Guid tool_id, Guid project_id);
        Task<List<T>> GetToolNameByProjectID<T>(int spType, Guid tool_id);
        Task<List<T>> GetProjectVendors<T>(int spType, Guid project_name, string user_name);
        Task<List<T>> GetProjectTechnology<T>(int spType, Guid project_name, Guid vendor, string user_name);
        Task<List<T>> GetProjectNodeType<T>(int spType, Guid project_name, Guid technology, Guid vendor, string user_name);
        Task<List<T>> GetUpdateHistory<T>(int spType, master_update_history master_update_History);
        Task<List<T>> GetHistoryByDate<T>(int spType, master_update_history master_update_History);
        Task<List<T>> GetStatusHistory<T>(int spType, master_update_history master_update_History);
        Task<CommonDbResponse> SaveUpdateMessageStatus(int spType,message_update_status request);
        //Task<string> GetUpdateMessage(int spType, string auto_id);
        Task <string> GetUpdateMessage(int getbyauto_id, string auto_id);

        Task<T> UpdateOthers<T>(int spType, project_requirement project_Requirement, string createdBy);
        //<T> UpdateOthers<T>(int spType, project_requirement project_Requirement, string createdBy);
        Task<T> UpdateOthers_CR<T>(int spType, vmProjectRequirement projectRequirement, string created_by);
        Task<T> UpdateOthers_IS<T>(int spType, vmProjectRequirement projectRequirement, string created_by);
        Task<T> SaveDevTask<T>(int spType, DeveloperTask developerTask);
        Task<List<T>> GetDeveloperTaskData<T>(int getAll, DeveloperTask request);
        Task<T> GetDevTaskByID<T>(int getById, string taskId);
        Task<CommonDbResponse> UpdateDevTask<T>(int getAll, DeveloperTask request);
        Task<CommonDbResponse> PullChat<T>(int save, ChatRecord chat);
        Task<List<T>> GetChatData<T>(int spType, ChatRecord chat);
        Task<CommonDbResponse> DeleteTask(string TaskId);
        Task<string> GetResolutionHr<T>(int getResolutionHr, Guid request_priority);
        Task<List<T>> GetAll_Filter<T>(int getall, Guid requirementId, List<Guid> request_status, vm_search request);
    }
}
