using AutoMapper;
using TemplateApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TemplateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetModels()
        {
            var models = await _testService.GetModels();
            var response = models;
            return Ok(response);
        }

        [HttpGet("version")]
        public async Task<IActionResult> GetVersion()
        {
            return Ok("v.1.0.0");
        }
    }
}