using Apruve.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Apruve
{
    public class ApruveClient
    {
        public ApruveEnvironment env { get; private set; }
        public string api_key { get; private set; }
        protected static ApruveClient client = null;

        // Constructor
        protected ApruveClient(string api_key, ApruveEnvironment env)
        {
            if (api_key == null)
            {
                throw new ApruveException("api_key cannot be null");
            }
            if (env == null)
            {
                throw new ApruveException("env cannot be null");
            }
            this.api_key = api_key;
            this.env = env;
        }

        /**
	     * Provides a single point of initialization for the ApruveClient library.
	     * 
	     * @param api_key
	     *            An API Key from your user account. Create on for your test
	     *            account on https://test.apruve.com/merchants, or create one
	     *            for live transactions at https://www.apruve.com/merchants. We
	     *            recommend that you create a unique API Key for each merchant
	     *            account.
	     * @param env
	     *            An environment object you must instantiate with an ApruveEnvironment 
         *            class method. Please use either ApruveEnvironment.prodEnvironment()
         *            or ApruveEnvironment.testEnvironment(), as appropriate.
	     *            
	     */
        public static void init(string api_key, ApruveEnvironment env)
        {
            client = new ApruveClient(api_key, env);
        }

        // Method for instantiating
        public static ApruveClient getInstance()
        {
            if (client == null)
            {
                throw new ApruveException("You must first initialize with ApruveClient.init()");
            }
            return client;
        }

        /**
         *  This method is invoked by the ApruveClient post(), put(), and get() methods. 
         *  It creates a RestClient object and passes it to the three-argument method,
         *  along with the request object and success status it received.
         */
        protected T responseHandler<T>(RestRequest request, System.Net.HttpStatusCode successStatus) where T : new()
        {
            return this.responseHandler<T>(new RestClient(client.env.getBaseUrl()), request, successStatus);
        }

        /**
         * This method executes and validates requests.
         */
        protected T responseHandler<T>(RestClient restClient, RestRequest request, System.Net.HttpStatusCode successStatus) where T:new()
        {

            // add headers
            request.AddHeader("Apruve-Api-Key", client.api_key);
            request.AddHeader("Content-Type", "application/json");

            // execute & validate request
            var response = restClient.Execute<T>(request);
            Console.WriteLine(response.Content);
            if (response.StatusCode != successStatus)
            {
                Console.WriteLine("RESPONSE CODE: " + response.StatusCode);
                throw new ApruveException(response.StatusCode.ToString());
            }
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            // write sent request object to debug console
            System.Diagnostics.Debug.WriteLine("Sent: " + request.Parameters.Where(p => p.Type == ParameterType.RequestBody).FirstOrDefault());

            // write Apruve server response to debug console
            System.Diagnostics.Debug.WriteLine("Response: " + (int)response.StatusCode + " " + response.StatusDescription + ":" + response.Content);

            // return response as data object;
            return response.Data;
        }

        public T post<T>(object requestBody, string uri, System.Net.HttpStatusCode successCode) where T : new()
        {
            // build request
            var request = new RestRequest(uri, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(requestBody);
            Console.WriteLine("REQUEST BODY: " + request.XmlSerializer.ToString());

            // pass request to responseHandler
            return this.responseHandler<T>(request, successCode);
        }

        public T put<T>(string uri, PaymentRequest arguments) where T : new()
        {
            // build request
            var request = new RestRequest(uri , Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(arguments);

            // pass request to responseHandler
            return this.responseHandler<T>(request, System.Net.HttpStatusCode.OK);
        }
        public T get<T>(string uri) where T : new()
        {
            // build request
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            // pass request to responseHandler
            return this.responseHandler<T>(request, System.Net.HttpStatusCode.OK);
        }
        public T delete<T>(string uri) where T : new()
        {
            // build request
            var request = new RestRequest(uri, Method.DELETE);
            request.RequestFormat = DataFormat.Json;

            // pass request to responseHandler
            return this.responseHandler<T>(request, System.Net.HttpStatusCode.OK);
        }

        // for unit testing
        public static void initToNull()
        {
            client = null;
        }
    }
}

