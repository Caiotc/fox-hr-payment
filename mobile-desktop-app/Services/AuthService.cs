using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;
using mobile_desktop_app.Models;
using mobile_desktop_app.Helpers;
using System.Text.RegularExpressions;

namespace mobile_desktop_app.Services
{
    public class AuthService
    {
        private RestClient client;
        private  UserInfo userInfo { get; set; }

        public AuthService()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();    
            client = new RestClient("https://localhost:56508/api/");
        }
        public async Task<RestResponse> AddEmployee(Employee employee)
        {
            var request = new RestRequest("Employee", Method.Post);
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            request.AddBody(employee);

            request.AddHeaders(new Dictionary<string, string>()
            {
                { "content-type","application/json" },
                {"accept", "text/plain" },
                {"Authorization",userInfo.jwtBearer }
            });

            var response = await client.ExecutePostAsync(request);

            return response;
        }
        public async Task<RestResponse> ChangePasswordAsync(string userName,string password,string newPassword)
        {
            var request = new RestRequest("User/changePassword", Method.Put);

            request.AddBody(new
            {
                userName,
                password,
                newPassword
            });

            request.AddHeaders(new Dictionary<string, string>()
            {
                { "content-type","application/json" },
                {"accept", "text/plain" },
                {"Authorization",userInfo.jwtBearer }
            });

            var response = await client.ExecutePutAsync(request);

            return response;

        }

        public  async Task<RestResponse> IsAuthenticatedAsync(string userName, string password)
        {
        
           
            var request = new RestRequest("User/login",Method.Post);

            request.AddBody(new
            {
                userName,
                password
            });

            request.AddHeaders(new Dictionary<string, string>()
            {
                { "content-type","application/json" },
                {"accept", "text/plain" }
            });

          return await client.ExecutePostAsync(request);

            
                           
        }
    }
}
