using PS1_MIC_090_Core.Repository;

namespace PS1_MIC_090_Core.Services
{
  
    public interface IApplicationService
    {
    }
    public class ApplicationService : BaseAppService, IApplicationService
    {
        private readonly IApplicationRepository applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }
    }
}
