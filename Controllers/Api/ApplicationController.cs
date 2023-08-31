using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using PS1_MIC_090_Core.Models;
using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository;
using PS1_MIC_090_Core.Repository.Domain;
using PS1_MIC_090_Core.Services;

using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PS1_MIC_090_Core.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : BaseController
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IMapper mapper;
        private readonly IApplicationRepository applicationRepository;
        private readonly IApplicationService applicationService;

        public ApplicationController(
            ILogger<ApplicationController> logger,
            IMapper mapper,
            IApplicationRepository applicationRepository, 
            IApplicationService applicationService)
        {
            this._logger = logger;
            this.mapper = mapper;
            this.applicationRepository = applicationRepository;
            this.applicationService = applicationService;
        }

        // GET: api/<ApplicationController>
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            try
            {
                int currentUserId = GetCurrentUserId();

                var apps = (await this.applicationRepository.GetAll())
                    .Where(x => x.UserId == currentUserId);

                return Ok(apps);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(ApplicationController), nameof(Get));
                throw;
            }
           
           
        }

        // GET api/<ApplicationController>/5
        [HttpGet("{applicationId}")]
        public async Task<IActionResult> Get(int applicationId)
        {
            try
            {
                int currentUserId = GetCurrentUserId();

                var apps = (await this.applicationRepository.GetAll())
                    .Where(x => x.ApplicationId == applicationId);

                return Ok(apps);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(ApplicationController), nameof(Get));
                throw;
            }
        }

        // POST api/<ApplicationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<CreateApplicationViewModel> model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(modelState:ModelState);
                }

                int currentUserId = GetCurrentUserId();

                List<Application> response = (await this.applicationService.CreateApplications(model, currentUserId));

                //string uri = $"https://www.example.com/api/values/{createdResource.Id}?version={createdResource.Version}";

                return Created(new Uri("/"), response);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(ApplicationController), nameof(Get));
                throw;
            }
        }

        // PUT api/<ApplicationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApplicationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
