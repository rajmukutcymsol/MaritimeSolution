using System.Web.Mvc;
using TicketingTool.Services.Abstract.Authentication;
using TicketingTool.Services.Abstract.Dashboard;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Services.Abstract.Requirement;
using TicketingTool.Services.Abstract.Role;
using TicketingTool.Services.Abstract.User;
using TicketingTool.Services.Concrete.Authentication;
using TicketingTool.Services.Concrete.Dashboard;
using TicketingTool.Services.Concrete.Mapping;
using TicketingTool.Services.Concrete.Master;
using TicketingTool.Services.Concrete.Requirement;
using TicketingTool.Services.Concrete.RoleRepository;
using TicketingTool.Services.Concrete.User;
using Unity;
using Unity.Mvc5;

namespace TicketingTool
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IMasterRepository, MasterRepository>();
            container.RegisterType<IRequirementRepository,RequirementRepository>();
            container.RegisterType<IUserAuthenticationRepository, UserAuthenticationRepository>();
            container.RegisterType<IDashboardRepository, DashboardRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IDomainRepository, DomainRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IVendorRepository, VendorRepository>();
            container.RegisterType<IStatusRepository, StatusRepository>();
            container.RegisterType<IFunctionRepository, FunctionRepository>();
            container.RegisterType<IFunctionLevelRepository, FunctionLevelRepository>();
            container.RegisterType<IPriorityRepository, PriorityRepository>();
            container.RegisterType<ITechnologyRepository, TechnologyRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<IEfficiencyRepository, EfficiencyRepository>();
            container.RegisterType<INodeTypeRepository, NodeTypeRepository>();
            container.RegisterType<IRegionRepository, RegionRepository>();
            container.RegisterType<ISDLCRepository, SDLCRepository>();
            container.RegisterType<ISolutionToolRepository, SolutionToolRepository>();
            container.RegisterType<IRoleMenuMapping, RoleMenuMapping>();
            container.RegisterType<IUseCaseRepository, UseCaseRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserProjectsMapping, UserProjectsMapping>();
            container.RegisterType<IProjectsToolsMapping, ProjectToolsMapping>();
            container.RegisterType<IProjectCustomersMapping, ProjectCustomersMapping>();
            container.RegisterType<IToolUseCasesMapping, ToolUseCasesMapping>();
            container.RegisterType<ICLIUIRepository, CLIUIRepository>();
            container.RegisterType<IDeveloperRepository, DeveloperRepository>();
            container.RegisterType<IProjectManagerRepository, ProjectManagerRepository>();
            container.RegisterType<ISolutionArchitectRepository, SolutionArchitectRepository>();
            container.RegisterType<IResoluionCategoryRepository, ResolutionCategoryRepository>();
            container.RegisterType<ITesterRepository, TesterRepository>();
            container.RegisterType<IResCat1Repository, ResCat1Repository>();
            container.RegisterType<IResCat2Repository, ResCat2Repository>();
            container.RegisterType<IResCat3Repository, ResCat3Repository>();
            container.RegisterType<IResCatMapping, ResCatMapping>();
            container.RegisterType<IProjectRegionMapping, ProjectRegionMapping>();
            container.RegisterType<IGroupEmailRepository, GroupEmailRepository>();
            container.RegisterType<IGroupEmailRepositoryMapping, GroupEmailMapping>();
            container.RegisterType<ICargoTypeRepository, CargoTypeRepository>();
            container.RegisterType<IDischargePortRipository, DischargePortRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IVesselRepository, VesselDataRepository>();
            container.RegisterType<ICommonMasterRepository, CommonMasterRepository>();
            container.RegisterType<IMarksAndNosRepository, MarksAndNosRepository>();
            container.RegisterType<IQuantityandKindofcargoname, QuantityandKindofcargoRepository>();
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<IStateRepository, StateRepository>();
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<IInboundRepository, InBoundRepository>();
            container.RegisterType<IQttRepository, QttRepository>();
            container.RegisterType<IRoleRightsRepository, RoleRights>();
            container.RegisterType<IInboundReportsRepository, InBoundReportsRepository>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}