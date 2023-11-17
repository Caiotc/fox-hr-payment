using RestSharp;
using SharedUI.Data;
using SharedUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedUI.Services
{
    public class ApiHelper
    {
        private RestClient client;
        private LogedUser logedUser { get; set; }

        public ApiHelper(LogedUser logedU)
        {
            logedUser = logedU;
            client = new RestClient("https://localhost:61760/api");
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
                {"Authorization",logedUser.jwtBearer },
                                {"Access-Control-Allow-Origin","*"}

            });

            var response = await client.ExecutePostAsync(request);

            return response;
        }
        public async Task<RestResponse> ChangePasswordAsync(string userName, string password, string newPassword)
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
                {"Authorization",logedUser.jwtBearer },
                                {"Access-Control-Allow-Origin","*"}

            });

            var response = await client.ExecutePutAsync(request);

            return response;

        }

        public async Task<RestResponse> IsAuthenticatedAsync(string userName, string password)
        {
            try
            {
                var request = new RestRequest("User/login", Method.Post);

                request.AddBody(new
                {
                    userName,
                    password
                });

                request.AddHeaders(new Dictionary<string, string>()
            {
                { "content-type","application/json" },
                {"accept", "text/plain" },
                {"Access-Control-Allow-Origin","*"}
            });

                return await client.ExecutePostAsync(request);

            }
            catch (Exception ex)
            {
                throw ex;

            }

  


        }

        public async Task<RestResponse> GetAllEmployees()
        {
            try
            {
                var request = new RestRequest("Employee", Method.Get);


                request.AddHeaders(new Dictionary<string, string>()
            {
                { "content-type","application/json" },
                {"accept", "text/plain" },
                {"Access-Control-Allow-Origin","*"}
            });

                 return await client.GetAsync(request);

            }
            catch (Exception ex)
            {
                throw ex;

            }




        }
        public async Task<RestResponse> GetALlPayments(string cpf)
        {
            try
            {
                var request = new RestRequest($"Payment/{cpf}", Method.Get);


                request.AddHeaders(new Dictionary<string, string>()
            {
                { "content-type","application/json" },
                {"accept", "text/plain" },
                {"Access-Control-Allow-Origin","*"}
            });

                return await client.GetAsync(request);

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public async Task<RestResponse> PerformPayments(string cpf)
        {
            try
            {
                var request = new RestRequest($"Payment/{cpf}", Method.Post);


                request.AddHeaders(new Dictionary<string, string>()
                {
                    { "content-type","application/json" },
                    {"accept", "text/plain" },
                    {"Access-Control-Allow-Origin","*"}
                });

              
                return await client.PostAsync(request);

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


    }
}
