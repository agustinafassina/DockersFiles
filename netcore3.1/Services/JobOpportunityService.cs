using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobOpportunity.Models;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ApiJobOpportunity.Services
{
    public class JobOpportunityService : IJobOpportunityService
    {
        private readonly ApiDBContext _context;
        private const string pathExternalService = "http://localhost:8081/";
        public JobOpportunityService(ApiDBContext context)
        {
            _context = context;
        }

        public async Task Post(JobOpportunity request)
        {
            try{
                _context.Add(request);
                _context.SaveChangesAsync();
                validateUserSuscriptions(request.Name);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void validateUserSuscriptions(string name)
        {
            var users = _context.Users.Where(x => x.InterestPositionsName == name);

            if(users.Count() > 0)
            {
                SendEmailSuscription(users, name);
            }
        }

        private static void SendEmailSuscription(IEnumerable<User> users, string position)
        {
            var message = new MimeMessage ();

            foreach(var user in users)
            {
                message.From.Add (new MailboxAddress ("Jobs Subscriptions", "hotmailAccount"));
                message.To.Add (new MailboxAddress (user.UserName, user.Email));
                message.Subject = "NEW JOB SUBSCRIPTIONS OF " + position;

                message.Body = new TextPart ("html") {
                    Text = BodyEmail(user, position)
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient()) {
                    client.Connect("smtp.live.com", 25, SecureSocketOptions.StartTls);
                    client.Authenticate("hotmailAccount", "hotmailPassword");
                    client.Send (message);
                    client.Disconnect (true);
                }
            }
        }

        private static string BodyEmail(User user, string position)
        {
            return "<html><body><h2>Hi " + user.UserName + ".</h2><br><h3>New Job about: " + position + ". </h3><br>. <h3>To unsubscribe click here <a href=''>link</a> </h3></body></html>";
        }

        public List<JobOpportunity> JobSearch(JobOpportunity filters)
        {
            var getListInternalService = GetJobInternal(filters);
            var getListExternalService = GetJobExternal(filters);

            var listCompleted = new List<JobOpportunity>();
            listCompleted.AddRange(getListInternalService);
            listCompleted.AddRange(getListExternalService);

            return listCompleted;
        }

        private List<JobOpportunity> GetJobInternal(JobOpportunity filters)
        {
            var jobs = _context.JobOpportunities.ToList();

            if(filters.Name != null)
            {
                jobs = jobs.Where(x => x.Name == filters.Name).ToList();
            }

            if(filters.Salary != 0)
            {
                jobs = jobs.Where(x => x.Salary == filters.Salary).ToList();
            }

            if(filters.Skill != null)
            {
                jobs = jobs.Where(x => x.Skill == filters.Skill).ToList();
            }

            return jobs;
        }

        private List<JobOpportunity> GetJobExternal(JobOpportunity filter)
        {
            var sourceExternalService = "https://security.engbimsoftware.com/api/sirvey/63615c323a783c914172e2f8";
            string requestUri = pathExternalService + sourceExternalService;

            var client = new RestClient(sourceExternalService);
            var request = new RestRequest { Method = Method.GET };

            IRestResponse response = client.Execute(request);
            return GetListResponseExternalService(response.Content);
        }

        private List<JobOpportunity> GetListResponseExternalService(string jsonResponse)
        {
            var jsonArray = JArray.Parse(jsonResponse);
            List<JobOpportunity> responseList = new List<JobOpportunity>();

            for (int i = 0; i < jsonArray.LongCount(); i++)
            {
                var responseNew = new JobOpportunity
                {
                    Name = jsonArray[i][0].ToString(),
                    Salary = (int)jsonArray[i][1],
                    Country = jsonArray[i][2].ToString(),
                    Skill = jsonArray[i][3].ToString()
                };

                responseList.Add(responseNew);
            }

            return responseList;
        }

        public async Task PostSuscription(User request)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
        }

        private void SendSuscriptionJob()
        {

        }

        public List<User> GetSuscriptionUser()
        {
            return _context.Users.ToList();
        }
    }
}