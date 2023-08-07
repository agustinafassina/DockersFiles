using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiJobOpportunity.Services
{
    public interface IJobOpportunityService
    {
        List<JobOpportunity> JobSearch(JobOpportunity filters);
        Task Post (JobOpportunity request);
        Task PostSuscription (User request);
        List<User> GetSuscriptionUser();
    }
}