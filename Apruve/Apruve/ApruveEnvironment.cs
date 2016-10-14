using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apruve
{
    public class ApruveEnvironment
    {
        private string base_url;
        private const string js_path = "/js/apruve.js";
        private ApruveEnvironment(string URL){
            this.base_url = URL;
        }

        // Return stuff with public methods!

        public static ApruveEnvironment prodEnvironment()
        {
            ApruveEnvironment environmentObject = new ApruveEnvironment("https://app.apruve.com");
            return environmentObject;
        }

        public static ApruveEnvironment testEnvironment()
        {
            ApruveEnvironment environmentObject = new ApruveEnvironment("https://test.apruve.com");
            return environmentObject;
        }

        public string getBaseUrl()
        {
            return base_url;
        }

        public string getJsUrl()
        {
            return getBaseUrl() + js_path;
        }
        
        public string getJsTag(){
            return "<script src=\"" + getJsUrl() + "\" type=\"text/javascript\"></script>";
        }
        
    }
}