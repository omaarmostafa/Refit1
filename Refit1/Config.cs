using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Refit1
{
    public static class Config
    {
        //public static string ApiUrl = "http://makeup-api.herokuapp.com";
        public static string ApiUrl = "http://asgatech-sampleapi.azurewebsites.net";


        public static string ApiHostName
        {
            get
            {
                var apiHostName = Regex.Replace(ApiUrl, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase)
                                   .Replace("/", string.Empty);
                return apiHostName;
            }
        }
    }
}
