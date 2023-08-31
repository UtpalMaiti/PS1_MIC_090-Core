using AutoMapper;

using PS1_MIC_090_Core.Controllers.Api;
using PS1_MIC_090_Core.Models;
using PS1_MIC_090_Core.Repository;
using PS1_MIC_090_Core.Repository.Domain;

namespace PS1_MIC_090_Core.Services
{

    public interface IApplicationService
    {
        Task<List<Application>> CreateApplications(List<CreateApplicationViewModel> model, int currentUserId);
    }
    public class ApplicationService : BaseAppService, IApplicationService
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly ILogger<ApplicationController> logger;
        private readonly IMapper mapper;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            ILogger<ApplicationController> logger,
            IMapper mapper
            )
        {
            this.applicationRepository = applicationRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<List<Application>> CreateApplications(List<CreateApplicationViewModel> model, int currentUserId)
        {
           var applications= mapper.Map<List<CreateApplicationViewModel>, IEnumerable< Application>>(model);

            applications= await this.applicationRepository.AddRange(applications.Distinct());

            return await Task.FromResult(applications.ToList());

        }
    }
}
