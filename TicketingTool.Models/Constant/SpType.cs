namespace TicketingTool.Models.Constant
{
    public enum MasterType
    {
        master_priority = 1,
        master_requirement=2,
        master_status=3,
        master_sdlc=4,
        master_technology=5,
        master_vendor=6,
        boolean_dropdown=7,
        master_tool = 8,
        master_function=9,
        master_function_level = 10,
        master_node_type =11,
        master_customer=12,
        master_region=13,
        master_domain=14,
        all=15
    }

    public enum ManageRequirement_Type
    {
        insert_requirement=1,
        getall=2,
        get_problems=3,
        get_change_request=4,
        get_requirement_by_id=5,
        update_requirement = 6,
        delete=7,
        getbytoolId = 8,
        getToolByProjectId=9,
        getVendorByProjectId=10,
        getTechnologyByProjectId = 11,
        getNodeTypeByProjectId=12,
        updatebyothers=13,
        updatebyothers_cr = 14,
        updatebyothers_is = 15,
        GetResolutionHr=16
    }

    public enum Generate_AutoId
    {
        NewRequest=1,
        ChangeRequest=2,
        IssuesRequest=3
    }

    public enum usp_ManageUser_Type
    {
        Insert_User=1,
        GetUserById=2,
        GetAll=3,
        GetUserByIdNew=4,
        UpdateUser=5,
        Delete=6,
       DevelopersList=7,
       DevelopersListfortask = 8,
       TesterList=9,
        getbyname = 10,
    }

    public enum usp_Dashbaord_Type
    {
        GetDataCount=1
    }

    public enum usp_ManageRole_Type
    {
        getAll=1,
        getRoleById=2,
        saveRole=3,
        updateRole=4,
        delete=5
    }

    public enum usp_ManageCustomer_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageDomain_Type
    {
        getAll=1,
        getById=2,
        save=3,
        update=4,
        delete=5
    }

    public enum usp_ManageFunction_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageFunctionLevel_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManagePriority_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageStatus_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageTechnology_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageVendor_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageProject_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageTool_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageSDLC_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }


    public enum usp_ManageRegion_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }


    public enum usp_ManageNodeType_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }


    public enum usp_Manageefficiency_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum usp_ManageRoleMenuMapping_Type
    {
        getRole=1,
        getMappedMenu=2,
        saveMapping=3
    }
    public enum usp_ManageAttachement_Type
    {
        newRequirement = 1,
        getAll=2,
        delete=3

    }
    public enum usp_ManageUseCase_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum ManageUserProjectMapping_Type
    {
        insert = 1,
        getall = 2,
        delete = 3
    }
    public enum ProjectToolsMapping_Type
    {
        insert = 1,
        getall = 2,
        delete = 3,
        getbyprojectwithTool=4

    }
    public enum ProjectCustomerMapping_Type
    {
        insert = 1,
        getall = 2,
        delete = 3
    }
    public enum ToolUseCaseMapping_Type
    {
        insert = 1,
        getall = 2,
        delete = 3,
       getbyprojectID=4
  
    }
    public enum usp_ManageCLIUI_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageDeveloper_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5,
        updatetask=6
    }
    public enum usp_ManageUpdateHistory_Type
    {
       getByAutoId=1,
       insert=2,
       getUpdatedHistory=3,
       getUpdatedHistoryByDate=4,
       getStatusHistory=5
    }
    public enum usp_ManageMessageUpdateStatus_Type
    {
        insert = 1,
        getbyauto_id=2

    }
    public enum usp_Solution_Architect_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_DeveloperTask_Type
    {
        save = 1,
        update = 2,
        delete = 3,
        getall=4,
        getbyId=5
    }
    public enum usp_Chat_Type
    {
        save = 1,
        getall = 2
       
    }
    public enum usp_ManageTester_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5,
        updatetask = 6
    }
    public enum usp_ManageResCat1_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageResCat2_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5,
        getbyCat1Id=6,
        getbyCat2Id = 7,


    }
    public enum usp_ManageResCat3_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum ResCatMapping_Type
    {
        insert = 1,
        getall = 2,
        delete = 3,
        getbyprojectwithTool = 4

    }
    public enum ProjectRegionMapping_Type
    {
        insert = 1,
        getall = 2,
        delete = 3,
        getbyprojectwithTool = 4

    }
    public enum usp_Dashbaord_Type_Info
    {
        GetDataCount_nr = 1,
        GetDataCount_cr = 2,
        GetDataCount_ir = 3,
        nrSum=4,
        crSum=5,
        irSum=6
    }
    public enum ProjectUser_Type
    {
        getbyuser = 1,
    }
    public enum usp_ManageGroupEmail_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_GroupEmailMapping_Type
    {
         
        getall = 1,
        getbyid=2,
        insert = 3,
        delete = 5

    }
    public enum usp_ManageCargoType_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageCategory_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageVessel
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageCommonMaster
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5,
        getstatebyid=6,
        getcitybyid=7
    }
    public enum usp_ManagePicInformation
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5,
        getstatebyid = 6,
        getcitybyid = 7
    }
    public enum usp_ManageMarkandNos_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_Quantity_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageState_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageCity_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }

    public enum ManageInboundRequirement_Type
    {
        getAll=1,
        update=4,
        insert_requirement = 3,
        updateall=6,
        approved=7

    }
    public enum ManageInboundManiFest_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5,
        updatetask = 6,
        updatemainefest=7,
        updateMeniFest_ByiD=8,
        GetApprovalData_VesselInfo=9,
        GetMeniFest_ById=10

    }
    public enum usp_ManageQtt_Type
    {
        getAll = 1,
        getById = 2,
        save = 3,
        update = 4,
        delete = 5
    }
    public enum usp_ManageRoleRight_Type
    {
        getAll = 1,
        getRoleById = 2,
        saveRole = 3,
        updateRole = 4,
        delete = 5
    }
    public enum ManageInbound_Reports_Type
    {
        getDetailsById = 1,

    }
    public enum usp_StowagePlan
    {
        getById = 1,
        PlanInfo=2,
        GetPlanInfo=3,
        GetRowByID=4,
        Update=5,
        Delete=6,
        save_Stowage_Plan_Info=7,
        GetPlainInfo=8,
        approved=9
    }
    public enum usp_Arrival_Condition
    {
        getById = 1,
        getInfoById=2,
        save=3,
        getInfo=4,
        approved=5,
        save_departure=6
        
    }
    public enum usp_CargoStausDay
    {
        getall = 1,
        getbyai = 2,
        save = 3,
        update = 4,
        delete = 5,
        getstatusdetailbyId=6,
        update_status=7,
        del_by_status_id=8
    }

    public enum usp_Cargo_Remarks
    {
        getall = 1,
        getById = 2,
        save = 3,
        update = 5,
        updateremarks=4,
        delete = 6,
        get_date_group=7,
        getremarks=8,
        getremarksbyid=9


    }
    public enum usp_DailyReport
    {
        getall = 1,
        getById = 2,
        save = 3,
        update = 5,
        delete = 6,
        get_date_group = 7,
        getremarks = 8,
        getforreport=9,
        getAllInfoForPri=10,
        getreportinfo_id=11,
        getupdateinfo=12,
        updatedaily=4,
        PreviousInfo=13,
        PreviousInfo_byautoid=14,
        deletedailyreport=15

    }
    public enum ManageSOF_Type
    {
        save = 1,
        getall=2
    }
}
