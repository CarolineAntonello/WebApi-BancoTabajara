using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BancoTabajara.API.Extensions
{
    public static class QueryExtensions
    {
        static HttpRequestMessage Request { get; set; }

        public static int GetQueryQuantidadeExtension(this HttpRequestMessage request)
        {
            var filters = request.GetQueryNameValuePairs();
            foreach (var sufix in filters)
            {
                if (sufix.Key.ToLower().Equals("quantidade"))
                {
                    return Convert.ToInt32(sufix.Value);
                }
            }
            return 0;
        }
    }
}