using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notesapp.WP.Models
{
    public class AccountDataService
    {
        string _apiUrl;

        public AccountDataService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public void LoginAsync(string username, string password, Action<IRestResponse<UserProfile>> callback)
        {
            var request = new RestRequest("Account/Login", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            RestClient client = new RestClient(_apiUrl);
            client.ExecuteAsync<UserProfile>(request, callback);
        }

    }
}
