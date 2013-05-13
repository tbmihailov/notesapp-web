using Notesapp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notesapp.WP.Models
{
    public class NotesappDataService
    {
        const string PARAM_AUTH_TOKEN = "AUTH_TOKEN";
        string _accessToken;
        string _apiUrl;

        public NotesappDataService(string apiUrl, string accessToken)
        {
            _apiUrl = apiUrl;
            _accessToken = accessToken;
        }

        public void GetNotes(Action<IRestResponse<List<Note>>> callback)
        {
            RestClient client = new RestClient(_apiUrl);
            var request = new RestRequest(new Uri("Notes", UriKind.Relative), Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("AUTH_TOKEN", _accessToken);

            client.ExecuteAsync<List<Note>>(request, callback);
        }

        public void GetNote(int id, Action<IRestResponse<Note>> callback)
        {
            RestClient client = new RestClient(_apiUrl);
            var request = new RestRequest(string.Format("Notes/{id}", id), Method.GET);
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.AddHeader("AUTH_TOKEN", _accessToken);
            }

            client.ExecuteAsync<Note>(request, callback);
        }

        public void CreateNote(Note note, Action<IRestResponse<Note>> callback)
        {
            RestClient client = new RestClient(_apiUrl);
            var request = new RestRequest(new Uri("Notes", UriKind.Relative), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(note);
            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.AddHeader("AUTH_TOKEN", _accessToken);
            }

            client.ExecuteAsync<Note>(request, callback);
        }

        public void UpdateNote(Note note, Action<IRestResponse> callback)
        {
            RestClient client = new RestClient(_apiUrl);
            int id = note.Id;
            var request = new RestRequest(string.Format("Notes/{id}", id), Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(note);
            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.AddHeader("AUTH_TOKEN", _accessToken);
            }

            client.ExecuteAsync(request, callback);
        }

        public void DeleteNote(int id, Action<IRestResponse> callback)
        {
            RestClient client = new RestClient(_apiUrl);
            var request = new RestRequest(string.Format("Notes/{id}", id), Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.AddHeader("AUTH_TOKEN", _accessToken);
            }

            client.ExecuteAsync(request, callback);
        }
    }
}
