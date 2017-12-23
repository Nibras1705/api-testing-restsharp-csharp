using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;  
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;

namespace MyNamespace
{
    [TestFixture]
    public class MyClass
    {
        [Test]  //This Test using NunitFramework
        public void RestApiTestingPOST()   //Sample Test Name of REST API POST Method
        {
            string URL = "http://myurl/URL";  //Rest API URL
            string RequestBody = "{'Key1': 'Value1', 'Key2': 'Value2', 'Key3': 'Value3', 'Key4': 'Value4'}";  //Rest API POST Body

            var client = new RestClient(URL);
            var request = new RestRequest(Method.POST);    //Selecting Rest API Method Type
            request.AddHeader("cache-control", "no-cache");   //Adding required Header
            request.AddHeader("content-type", "application/json");  //Adding Json Header as request body and response type Json
            request.AddParameter("application/json", RequestBody, ParameterType.RequestBody);  
            IRestResponse response = client.Execute(request);

            string tokenresponse = response.Content;

            JToken user = JsonConvert.DeserializeObject<JToken>(tokenresponse);
            Console.WriteLine(user);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);   //Verifying Status Code to make sure response is as expected
        }


        [Test]
        public void RestApiTestingGET()
        {
            string URL = "http://myurl/URL";  //Rest API URL
            string RequestBody = null;  //GET Method doesn't contain any request Body
            string Username = "myUname";  //Variables for Authenticaton Header
            string Password = "myPass";

            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);  //Selecting Rest API Method Type
            request.AddHeader("cache-control", "no-cache");    //Adding required Header
            request.AddHeader("content-type", "application/json");  //Adding Json Header as response type Json
            client.Authenticator = new HttpBasicAuthenticator(Username, Password);  //Adding Authenticaton Header
            request.AddParameter("application/json", RequestBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            string tokenresponse = response.Content;

            JToken user = JsonConvert.DeserializeObject<JToken>(tokenresponse);
            Console.WriteLine(user);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);  //Verifying Status Code to make sure response is as expected
        }
    }
}