using System.Threading.Tasks;
using ApiJobOpportunity.Services;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ApiJobOpportunity.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobOpportunityController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            var sourceExternalService = "https://api.publicapis.org/entries";
            var request = new RestRequest(sourceExternalService);
            var client = new RestClient();
            var response = client.ExecuteAsync(request);

            return response.Result.Content;
        }
    }
}
