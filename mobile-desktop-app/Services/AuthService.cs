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

namespace mobile_desktop_app.Services
{
    public class AuthService
    {
        private RestClient client;
        private  UserInfo userInfo { get; set; }

        public AuthService()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();    
            client = new RestClient("https://localhost:51767/api/");
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

        public  async Task IsAuthenticatedAsync(string userName, string password)
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

          var response = await client.ExecutePostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<UserInfo>(response.Content!);

                userInfo.jwtBearer = "Bearer " + result.jwtBearer;
                userInfo.username = result.username;
                userInfo.userPhoto = result.userPhoto;
                userInfo.role = result.role;
                userInfo.roleId = result.roleId;              
            }
                           
        }
    }
}
