using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using REST.Entity;
using REST.Interfaces;

namespace REST.WebClient
{
    class Rest
    {
        /// <summary>
        /// Downloads persons list from web source async, parse form JSON to Person entity
        /// </summary>
        /// <returns>Persons list entity</returns>
        public async Task< List<Person> > GetPersonsListAsync()
        {
            string url = "https://ucitel.sps-prosek.cz/~maly/PRG/json.php";
            var client = new RestClient(url);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("resource/{id}", Method.POST);
            // request.AddParameter("error", "1"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            request.AddHeader("header", "value");

            IRestResponse response = client.Execute(request);

            IParser parser = new JsonParser();
            return await parser.ParseStringAsync<List<Person>>(response.Content);

        }
    }
}
