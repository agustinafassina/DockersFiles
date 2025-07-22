
using TemplateApi.Services.interfaces;

namespace TemplateApi.Services
{
    public class TestService : ITestService
    {
        public TestService()
        {
        }

        public async Task<string> GetModels()
        {
            var response = "ates";
            return response;
        }
    }
}