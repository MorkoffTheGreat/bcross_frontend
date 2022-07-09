using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using bcross_frontend.Models;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bcross_frontend.Services
{
    class RestService
    {
        RestClient client;
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        RestClientOptions restoptions = new RestClientOptions("https://bcross.tangenx.dev/")
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
            Encoding = Encoding.UTF8
        };

        public RestService()
        {
            client = new RestClient(restoptions);
        }

        public void AddUser(User user)
        {
            var request = new RestRequest("User/add", Method.Post);
            request.AddBody(user);
            var response = client.Execute(request);
        }

        public async Task<string> PutBook(int bookid, int senderid, string location, DateTime sendtime, string commentary)
        {
            var request = new RestRequest("bookrecord/put", Method.Post);
            request.AddQueryParameter("bookid", bookid)
                .AddQueryParameter("senderid", senderid)
                .AddQueryParameter("location", location)
                .AddQueryParameter("sendtime", sendtime)
                .AddQueryParameter("commentary", commentary);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async void AddBook(string name, string author, string year)
        {
            var request = new RestRequest("book/add", Method.Post);
            request.AddBody(
                new Book()
                {
                    Name = name,
                    Author = author,
                    Year = year
                });
            await client.ExecuteAsync(request);
        }

        public async void TakeBook(Guid guid, int receiver, DateTime receivetime)
        {
            var request = new RestRequest("bookrecord/take", Method.Post);
            request.AddQueryParameter("guid", guid);
            request.AddQueryParameter("receiver", receiver);
            request.AddQueryParameter("receivetime", receivetime);
            var response = await client.ExecuteAsync(request);
        }

        public async Task<List<Book>> GetSentBooks(int userid)
        {
            var request = new RestRequest("bookrecord/sentbooks", Method.Get);
            request.AddQueryParameter("userid", userid);
            var response = await client.ExecuteAsync(request);
            return JsonSerializer.Deserialize<List<Book>>(response.Content, options);
        }

        public async Task<List<Book>> GetReceivedBooks(int userid)
        {
            var request = new RestRequest("bookrecord/receivedbooks", Method.Get);
            request.AddQueryParameter("userid", userid);
            var response = await client.ExecuteAsync(request);
            return JsonSerializer.Deserialize<List<Book>>(response.Content, options);
        }

        public async Task<int> GetBookByGuid(Guid guid)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var request = new RestRequest("book/getbyguid", Method.Get);
            request.AddQueryParameter("guid", guid);
            var response = await client.ExecuteAsync(request);
            if (response.Content == "")
            {
                return 0;
            }
            Book book = JsonSerializer.Deserialize<Book>(response.Content, options);
            return book.Id;
        }

        public async Task<int> GetNextBookId()
        {
            var request = new RestRequest("book/lastid", Method.Get);
            var response = await client.ExecuteAsync(request);
            return int.Parse(response.Content) + 1;
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            var request = new RestRequest("user/idbyemail", Method.Get);
            request.AddQueryParameter("email", email);
            var response = await client.ExecuteAsync(request);
            return int.Parse(response.Content);
        }

        public List<Bookrecord> GetAllPutBooks()
        {
            var request = new RestRequest("bookrecord/GetAllPutBooksLocations", Method.Get);
            var response = client.Execute(request);
            return JsonSerializer.Deserialize<List<Bookrecord>>(response.Content, options);
        }

        public Book GetBookById(int id)
        {
            var request = new RestRequest($"book/{id}");
            var response = client.Execute(request);
            return JsonSerializer.Deserialize<Book>(response.Content, options);
        }

        public async Task<List<string>> GetBookTravel(Guid guid)
        {
            var request = new RestRequest("book/getbooktravel", Method.Get);
            request.AddQueryParameter("guid", guid);
            var response = await client.ExecuteAsync(request);
            var ret = JsonSerializer.Deserialize<List<string>>(response.Content, options);
            return ret;
        }

        public User GetUserById(int id)
        {
            var request = new RestRequest($"user/{id}", Method.Get);
            var response = client.Execute(request);
            var ret = JsonSerializer.Deserialize<User>(response.Content, options);
            return ret;
        }

        public  List<User> GetScoreboard()
        {
            var request = new RestRequest("user/scoreboard", Method.Get);
            var response = client.Execute(request);
            return JsonSerializer.Deserialize<List<User>>(response.Content, options);
        }

    }
}
